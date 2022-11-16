namespace RabbitMQMvc.RabitMQ
{
    public interface IRabitMQProducer
    {
        public void SendMessage<T>(T message);
        public IList<string> GetMessages();
    }
}
