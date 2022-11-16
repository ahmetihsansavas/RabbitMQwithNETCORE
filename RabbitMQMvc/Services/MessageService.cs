using RabbitMQMvc.Data;
using RabitMQMvc.Entity;

namespace RabitMqMessageAPI.Services
{
    public class MessageService : IMessageService
    {
        public DbContextClass _dbContext;
        public MessageService(DbContextClass dbContext) {
            _dbContext = dbContext;
        }
        public Message CreateMessage(Message message)
        {
            var result = _dbContext.Messages.Add(message);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public bool DeleteMessage(int Id)
        {
            var filteredData = _dbContext.Messages.Where(m=>m.MessageId==Id).FirstOrDefault();
            var result = _dbContext.Messages.Remove(filteredData);
            _dbContext.SaveChanges();
            return result != null ? true : false;
        }

        public Message findMessageById(int Id)
        {
            return _dbContext.Messages.Where(m => m.MessageId == Id).FirstOrDefault();
        }

        public IEnumerable<Message> MessageList()
        {
            return _dbContext.Messages.ToList();
        }

        public Message UpdateMessage(Message message)
        {
           var result = _dbContext.Messages.Update(message);
            _dbContext.SaveChanges();
            return result.Entity;
        }
    }
}
