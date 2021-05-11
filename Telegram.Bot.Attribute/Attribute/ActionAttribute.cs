using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Bot.Attribute
{
    public class ActionAttribute : System.Attribute
    {
        /// <summary>
        /// Работа - имя
        /// </summary>
        public string WorkName { get; private set; }

        public ActionAttribute(string workName)
        {
            this.WorkName = workName;
        }

        public override string ToString()
        {
            return WorkName;
        }
    }
}
