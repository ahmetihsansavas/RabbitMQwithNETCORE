// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

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
};
channel.BasicConsume(queue: "message", autoAck: true, consumer: consumer);
Console.ReadKey();
