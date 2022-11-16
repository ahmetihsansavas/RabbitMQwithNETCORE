using RabitMQMvc.Entity;

namespace RabbitMQMvc.Models
{
    public class MessageModel
    {
        public string Data { get; set; }
        public IList<String> MessageList { get; set; }
    }
}
