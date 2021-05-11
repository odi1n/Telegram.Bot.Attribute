using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Bot.Attribute
{
    public class CommandAttribute : System.Attribute
    {
        /// <summary>
        /// Команда
        /// </summary>
        public string[] Commands { get; private set; }
        /// <summary>
        /// Доступ
        /// </summary>
        public int Access { get; private set; }

        public CommandAttribute(string[] commands, int access)
        {
            this.Commands = commands;
            this.Access = access;
        }

        public override string ToString()
        {
            return Commands.FirstOrDefault();
        }
    }
}
