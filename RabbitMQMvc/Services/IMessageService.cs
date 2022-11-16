using RabbitMQMvc.Models;
using RabitMQMvc.Entity;

namespace RabitMqMessageAPI.Services
{
    public interface IMessageService
    {
        public IEnumerable<Message> MessageList();
        public Message CreateMessage(Message message);
        public Message UpdateMessage(Message message);
        public Message findMessageById(int Id);
        public bool DeleteMessage(int Id);

    }
}
