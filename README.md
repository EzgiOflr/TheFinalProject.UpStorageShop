# TheFinalProject.UpStorageShop


# Project Description

This project involves a bot application developed at the request of an e-commerce website administrator. 
The bot is designed to scrape the competitor's products, track their prices, and perform discount or price matching actions according to the administrator's defined strategy. 
Additionally, a web application with a console screen is provided for monitoring the bot's operational status.

**Features**

+ Ability to scrape competitor's products and track their prices.
+ Option to scrape all products or a specified number of products.
+ Ability to select and scrape only discounted products, non-discounted products, or all products.
+ Tracking the bot's operational status and performed actions through a console screen in the web application.
+ Viewing the scraped products in a web-based panel and saving them as an Excel file.
+ Sending the scraped products via email.

# Crawler App (.NET - Console Application)

This application is designed to perform data scraping operations for us.
For data scraping, I have used the Selenium framework. 
When the application starts, it prompts the user to specify the products and the quantity of products they want to scrape.


# Web API (Clean Architecture - ASP.NET Core 7 Application)


This application serves as the intermediary for inter-application communication and handles our database operations.
It follows the Clean Architecture principles and implements the CQRS (Command Query Responsibility Segregation) design pattern for handling API requests.
Additionally, it utilizes SignalR for real-time communication. 

# React Client Application

This application consists of two sections on the screen. In the first section, users can view their orders, order details, and associated products in tables. The second section listens to the logs sent by the Hub via SignalR. It receives all the data (texts, messages) from the logs and displays them in the formatted section of the terminal view on the page.
Additionally, there is a "Send Mail" button. When clicked, it sends the selected product, order event, and order sections from the table as an Excel file via email. Furthermore, based on the customer's preference displayed in the table, it allows saving the file as an Excel file to a specified file path.


Please note that this description provides an overview of the application's functionality. If you have any specific requirements or need further details, feel free to let me know.


![Ekran Alıntısı](https://github.com/EzgiOflr/TheFinalProject.UpStorageShop/assets/97016507/1a933dd0-6acc-485d-908c-ef4eb8539a4b)



![Ekran Alıntısı3](https://github.com/EzgiOflr/TheFinalProject.UpStorageShop/assets/97016507/997f207a-e140-4b22-a193-a9e8251b490d)










