using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Attribute.Models;

namespace Telegram.Bot.Attribute.Bot
{
    public class BotAction
    {
        public List<AccauntModels> Accaunt { get; private set; }
        public List<ulong> ApplicationUniqId { get; private set; }

        public BotAction(string message)
        {
            var splitList = message.Split(new[] { "\n" }, StringSplitOptions.None);

            foreach (var accStr in splitList)
            {
                var accaunt = accStr.Split(new[] { ":", ";" }, StringSplitOptions.None);
                if (accaunt.Count() != 2)
                    continue;

                if (Accaunt == null)
                    Accaunt = new List<AccauntModels>();

                Accaunt.Add(new AccauntModels(accaunt[0], accaunt[1]));
            }

            foreach (var s in splitList)
            {
                try
                {
                    ulong number = Convert.ToUInt64(s);
                    if (ApplicationUniqId ==null)
                        ApplicationUniqId = new List<ulong>();

                    ApplicationUniqId.Add(number);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
