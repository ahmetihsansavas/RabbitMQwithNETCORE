using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQMvc.RabitMQ;
using System.Text;

namespace RabitMqMessageAPI.RabitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {

        public IList<string> GetMessages()
        {
            var messages = new List<string>();
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("message", exclusive: false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
                messages.Add(message);
            };
            channel.BasicConsume(queue: "message", autoAck: true, consumer: consumer);
            return messages;
        }

        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory { HostName="localhost"};
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("message",exclusive:false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange:"",routingKey:"message",body:body);
        }
    }
}
