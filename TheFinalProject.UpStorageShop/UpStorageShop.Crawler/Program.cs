using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using OpenQA.Selenium.Chrome;
using System.Globalization;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;
using Domain.Entities;
using Newtonsoft.Json;
using Domain.Dtos;
using Application.Features.Orders.Commands.Add;
using Application.Features.Products.Commands.Add;
using Application.Features.OrderEvents.Commands.Add;

Console.WriteLine("UpSchool Crawler");

string log = "";

new DriverManager().SetUpDriver(new ChromeConfig());

IWebDriver driver = new ChromeDriver();

var hubConnection = new HubConnectionBuilder()
    .WithUrl($"https://localhost:7172/Hubs/SeleniumLogHub")
    .WithAutomaticReconnect()
    .Build();

await hubConnection.StartAsync();

var httpClient = new HttpClient();

while (true)
{
    var isDone = false;
    var productCount = 0;

    while (productCount == 0)
    {
        Console.WriteLine("How many products do you want to crawl?");
        var productCountString = Console.ReadLine();
        int.TryParse(productCountString, out productCount);

        if (productCount == 0)
        {
            Console.WriteLine("Please enter a number.");
        }
    }

    var productType = "";

    while (!(productType == "A" || productType == "B" || productType == "C"))
    {
        Console.WriteLine("What type of products you want to crawl? (A - All, B - On Sale, C - Not On Sale)");
        productType = Console.ReadLine();

        if (!(productType == "A" || productType == "B" || productType == "C"))
        {
            Console.WriteLine("Please enter A or B or C.");
        }
    }

    Order order = null;

    try
    {
        var productList = new List<ProductDto>();

        var currentPage = 0;



        while (productList.Count < productCount && isDone == false)
        {
            currentPage++;

            if (currentPage == 1)
            {
                var orderResponse = await httpClient.PostAsJsonAsync("https://localhost:7172/api/Order/CreateOrder", new OrderAddCommand()
                {
                    ProductCrawlType = productType == "A" ? Domain.Enums.ProductCrawlType.All : productType == "B" ? Domain.Enums.ProductCrawlType.OnDiscount : Domain.Enums.ProductCrawlType.NonDiscount,
                    RequestedAmount = productCount
                });

                var asd = await orderResponse.Content.ReadAsStringAsync();

                var orderId = JsonConvert.DeserializeObject<dynamic>(await orderResponse.Content.ReadAsStringAsync()).data;

                order = new Order()
                {
                    Id = orderId
                };

                await httpClient.PostAsJsonAsync("https://localhost:7172/api/OrderEvent/CreateOrderEvent", new OrderEventAddCommand()
                {
                    OrderId = order.Id,
                    Status = Domain.Enums.OrderStatusEnum.BotStarted
                });
            }


            driver.Navigate().GoToUrl("https://finalproject.dotnet.gg/?currentPage=" + currentPage);

            if (currentPage == 1)
            {
                log = "Websiteye giriş yapıldı.";
                await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(log));
                Console.WriteLine(log);

                var pages = driver.FindElements(By.ClassName("page-number")).LastOrDefault().Text;

                log = "Toplam " + pages + " sayfa ürün bulunduğu tespit edildi.";
                await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(log));
                Console.WriteLine(log);
            }

            log = currentPage + ". sayfaya girildi.";
            await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(log));
            Console.WriteLine(log);

            await httpClient.PostAsJsonAsync("https://localhost:7172/api/OrderEvent/CreateOrderEvent", new OrderEventAddCommand()
            {
                OrderId = order.Id,
                Status = Domain.Enums.OrderStatusEnum.CrawlingStarted
            });

            var divs = driver.FindElements(By.ClassName("mb-5"));

            if (divs.Count == 0)
            {
                isDone = true;
                continue;
            }

            var pageProductCount = 0;

            foreach (var item in divs)
            {
                var productName = item.FindElement(By.ClassName("product-name")).Text;
                var isOnSale = item.FindElements(By.ClassName("onsale")).Count > 0;
                var picture = item.FindElement(By.ClassName("card-img-top")).GetAttribute("src");
                decimal salePrice = 0;

                if (item.FindElements(By.ClassName("sale-price")).Count > 0)
                {
                    salePrice = Convert.ToDecimal(item.FindElement(By.ClassName("sale-price")).Text.Replace("$", ""), CultureInfo.GetCultureInfo("tr-TR"));
                }

                var price = Convert.ToDecimal(item.FindElement(By.ClassName("price")).Text.Replace("$", ""), CultureInfo.GetCultureInfo("tr-TR"));

                if (productType == "B" && isOnSale == false)
                {
                    continue;
                }

                if (productType == "C" && isOnSale == true)
                {
                    continue;
                }

                if (productList.Count >= productCount)
                {
                    continue;
                }

                productList.Add(new ProductDto()
                {
                    OrderId = order.Id,
                    IsOnSale = isOnSale,
                    Name = productName,
                    Picture = picture,
                    SalePrice = salePrice,
                    Price = price
                });

                pageProductCount++;
            }


            log = currentPage + ". sayfa tarandı, " + pageProductCount + " ürün bulundu.";
            await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(log));
            Console.WriteLine(log);

            await httpClient.PostAsJsonAsync("https://localhost:7172/api/OrderEvent/CreateOrderEvent", new OrderEventAddCommand()
            {
                OrderId = order.Id,
                Status = Domain.Enums.OrderStatusEnum.CrawlingCompleted
            });
        }

        await httpClient.PostAsJsonAsync("https://localhost:7172/api/Product/CreateProducts", new ProductAddCommand()
        {
            Products = productList,
            OrderId = order.Id
        });

        await httpClient.PostAsJsonAsync("https://localhost:7172/api/OrderEvent/CreateOrderEvent", new OrderEventAddCommand()
        {
            OrderId = order.Id,
            Status = Domain.Enums.OrderStatusEnum.OrderCompleted
        });

        log = "Toplamda " + productList.Count + " Ürün kazındı.";
        await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(log));
        Console.WriteLine(log);

        //Console.WriteLine(productList.Count + " Products crawled.");
    }
    catch (Exception exception)
    {
        await httpClient.PostAsJsonAsync("https://localhost:7172/api/OrderEvent/CreateOrderEvent", new OrderEventAddCommand()
        {
            OrderId = order.Id,
            Status = Domain.Enums.OrderStatusEnum.CrawlingFailed
        });

        log = "Kazıma esnasında hata oluştu.";
        await hubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(log));
        Console.WriteLine(log);
        //driver.Quit();
    }
}

SeleniumLogDto CreateLog(string message) => new SeleniumLogDto(message);