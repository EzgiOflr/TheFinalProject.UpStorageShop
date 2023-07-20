using Application.Common.Models;
using Application.Features.OrderEvents.Commands.Add;
using Application.Features.Orders.Commands.Add;
using Application.Features.Products.Commands.Add;
using Domain.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Application.Features.Exports.Queries.GetProductsByOrderIdExport;
using Application.Features.Exports.Queries.GetOrderEventsByOrderIdExport;

namespace BackgroundWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private Timer _timer;
        private readonly string _logHub = "https://localhost:7172/Hubs/SeleniumLogHub";
        private readonly HubConnection _logHubConnection;

        private readonly string _listenerHub = "https://localhost:7172/Hubs/TriggerHub";
        private readonly HubConnection _listenerHubConnection;


        HttpClient httpClient = new HttpClient();

        IWebDriver driver = null;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;


            new DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();

            _logHubConnection = new HubConnectionBuilder()
                 .WithUrl(_logHub)
                 .WithAutomaticReconnect()
                 .Build();

            _listenerHubConnection = new HubConnectionBuilder()
                   .WithUrl(_listenerHub)
                   .WithAutomaticReconnect()
                   .Build();

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _listenerHubConnection.On<CrawlerTriggerDto>("ReactDataReceived", (crawlerTriggerDto) =>
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", crawlerTriggerDto.Token);

                Crawler(crawlerTriggerDto.ProductCount, crawlerTriggerDto.ProductType);
            });

            await _listenerHubConnection.StartAsync(stoppingToken);
            await _logHubConnection.StartAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }

        async Task Crawler(int productCount, string productType)
        {
            var isDone = false;
            Order order = null;
            string log = "";

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


                    driver.Navigate().GoToUrl("https://4teker.net/?currentPage=" + currentPage);

                    if (currentPage == 1)
                    {
                        log = "Websiteye giriş yapıldı.";
                        await _logHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(log));
                        Console.WriteLine(log);

                        var pages = driver.FindElements(By.ClassName("page-number")).LastOrDefault().Text;

                        log = "Toplam " + pages + " sayfa ürün bulunduğu tespit edildi.";
                        await _logHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(log));
                        Console.WriteLine(log);
                    }

                    log = currentPage + ". sayfaya girildi.";
                    await _logHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(log));
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
                    await _logHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(log));
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

                await httpClient.PostAsJsonAsync("https://localhost:7172/api/Export/GetProductsByOrderIdExport", new GetProductsByOrderIdExportQuery(order.Id));
                await httpClient.PostAsJsonAsync("https://localhost:7172/api/Export/GetOrderEventsByOrderIdExport", new GetOrderEventsByOrderIdExportQuery(order.Id));

                log = "Toplamda " + productList.Count + " Ürün kazındı.";
                await _logHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(log));
                Console.WriteLine(log);

            }
            catch (Exception exception)
            {
                await httpClient.PostAsJsonAsync("https://localhost:7172/api/OrderEvent/CreateOrderEvent", new OrderEventAddCommand()
                {
                    OrderId = order.Id,
                    Status = Domain.Enums.OrderStatusEnum.CrawlingFailed
                });

                log = "Kazıma esnasında hata oluştu.";
                await _logHubConnection.InvokeAsync("SendLogNotificationAsync", CreateLog(log));
                Console.WriteLine(log);
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
        SeleniumLogDto CreateLog(string message) => new SeleniumLogDto(message);
    }
}