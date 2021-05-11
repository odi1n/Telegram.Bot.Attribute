using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Bot.Attribute
{
    public static class CallbackData
    {
        public static string SetParams(this string command, params object[] param)
        {
            return command + ":" + string.Join(":", param);
        }
    }
}
