using Contracts;
using DotNetCore.CAP;

namespace WebApi.Producer
{
    public class LogConsumerService : ICapSubscribe
    {
        private readonly ILogger<LogConsumerService> _logger;

        public LogConsumerService(ILogger<LogConsumerService> logger)
        {
            _logger = logger;
        }

        [CapSubscribe("queue.cap.log")]
        public async Task Handle(LogMessage message)
        {
            await Task.CompletedTask;
            _logger.LogInformation("log consumer executed...");
            Console.WriteLine($"{message.Id}'li log ---> {message.Message}");
        }
    }
}
