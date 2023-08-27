namespace Infrastructure.EventBus.RabbitMQ
{
    public class RabbitMQEventBusOptions
    {
        public const string SectionName = "EventBus:RabbitMQ";

        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RetryCount { get; set; }
        public string QueueName { get; set; }
    }
}