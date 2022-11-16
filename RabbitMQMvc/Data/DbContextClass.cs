using Microsoft.EntityFrameworkCore;
using RabitMQMvc.Entity;

namespace RabbitMQMvc.Data
{
    public class DbContextClass:DbContext
    {
        protected IConfiguration Configuration;
        public DbContextClass(IConfiguration configuration) {
        Configuration=configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));       
        }
        public DbSet<Message> Messages { get; set; }
    }
}
