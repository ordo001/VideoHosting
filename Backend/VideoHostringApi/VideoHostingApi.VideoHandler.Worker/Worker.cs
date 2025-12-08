using VideoHostingApi.VideoHandler.Services.Contracts.Models;
using VideoHostringApi.Common.Messaging.Contracts;

namespace VideoHostingApi.VideoHandler.Worker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> logger;
    private readonly IMessageConsumer<VideoProcessingMessage> consumer;

    public Worker(ILogger<Worker> logger, IMessageConsumer<VideoProcessingMessage> consumer)
    {
        this.logger = logger;
        this.consumer = consumer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }
            await consumer.StartConsuming("video-processing");

            await Task.Delay(1000, stoppingToken);
        }
    }
}