using System.ComponentModel;

namespace RabbitMQMvc.Models
{
    public class LoginModel
    {
        [DisplayName("Kullanıcı Adı")]
        public string name { get; set; }
    }
}
