using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Bot.Attribute.Bot
{
    public class BotCallback
    {
        public string Command { get; private set; }
        public string AllText { get; private set; }
        public string NoCommand { get; private set; }
        public List<string> Arguments { get; private set; }
        public List<string> TextList { get; private set; }

        public void Parse(string text)
        {
            if (string.IsNullOrEmpty(text)) return;

            var splits = text.Split(new char[]{ ':', ' '});

            this.AllText = text;
            this.Command = splits?.FirstOrDefault();
            this.NoCommand = string.Join(":", splits.Skip(1));
            this.Arguments = splits.Skip(1).Take(splits.Count()).ToList();
            this.TextList = splits.ToList();
        }
    }
}
