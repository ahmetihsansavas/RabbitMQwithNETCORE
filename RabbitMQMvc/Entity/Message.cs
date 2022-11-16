namespace RabitMQMvc.Entity
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Data { get; set; }
        public DateTime dateTime { get; set; } = DateTime.Now;
        public string UserName { get; set; }

    }
}
