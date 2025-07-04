using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.AddHostedService<ScraperWorker>();
    }).Build();

await host.RunAsync();

public class ScraperWorker : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: "scrape",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var url = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Scraping: {url}");
            // TODO: use HtmlAgilityPack to scrape and save to DB
        };
        channel.BasicConsume(queue: "scrape",
                             autoAck: true,
                             consumer: consumer);

        Console.WriteLine("ScraperWorker started.");
        return Task.CompletedTask;
    }
}
