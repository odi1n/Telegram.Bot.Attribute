using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Bot.Attribute.Models
{
    public class AccauntModels
    {
        public string Login { get; private set; }
        public string Password { get; private set; }

        public AccauntModels(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }
    }
}
