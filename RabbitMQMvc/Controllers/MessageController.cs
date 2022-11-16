﻿using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using RabbitMQMvc.Models;
using RabbitMQMvc.RabitMQ;
using RabitMqMessageAPI.Services;
using RabitMQMvc.Entity;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace RabbitMQMvc.Controllers
{
    
    public class MessageController : Controller
    {
        public IMessageService _messageService { get; set; }
        public IRabitMQProducer _rabitMQProducer { get; set; }
        public MessageController(IMessageService messageService,IRabitMQProducer rabitMQProducer)
        {
            _messageService = messageService;
            _rabitMQProducer= rabitMQProducer;
            GetMessageList();
        }
        static List<string> Messagelist = new List<string>();
        [HttpGet]

        public IActionResult CreateMessage()
        {
            return View(new MessageModel() { MessageList= Messagelist});
        }
        [HttpPost]
        public IActionResult CreateMessage(string Data) {
            var userName = HttpContext.Session.GetString("UserName");
            var message = new Message() { Data= Data ,UserName= userName };
            var messageEntity = _messageService.CreateMessage(message);
            _rabitMQProducer.SendMessage(messageEntity);
            return View(new MessageModel() { MessageList = Messagelist,Data=Data});
        }

        public void GetMessageList()
        {
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
                Messagelist.Add(message);
            };
            channel.BasicConsume(queue: "message", autoAck: true, consumer: consumer);
        }

        public IActionResult MessageList() {
            return View(Messagelist);
        }
    }
}
