# EventDrivenWebScraper

## 📄 Description
A microservice-based web scraper using RabbitMQ to queue and process scraping tasks.

## 🛠 Tech Stack
- .NET Worker Service
- RabbitMQ
- HtmlAgilityPack
- NHibernate
- PostgreSQL

## 🚀 Features
- Queue-based scraping requests
- Scrapes product/news info from sample websites
- Stores data into PostgreSQL using NHibernate
- Supports API trigger and cron-based scheduling

## 📦 How to Run
1. Clone this repo
2. Set up the DB connection string in `appsettings.json`
3. Run using Visual Studio or `dotnet run`

