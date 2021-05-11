using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Attribute.Bot;
using Telegram.Bot.Types;
using BotCommand = Telegram.Bot.Attribute.Bot.BotCommand;

namespace Telegram.Bot.Attribute.Params
{
    public class TextParams
    {
        public Message Message { get; private set; }
        public BotCommand Command { get; private set; }

        public CallbackQuery CallbackQuery { get; private set; }
        public BotCallback Callback { get; private set; }

        public BotAction Action { get; private set; }


        public TextParams(BotCommand command, Message message = null)
        {
            this.Command = command;
            this.Message = message;
        }

        public TextParams(BotCallback callback, CallbackQuery callbackQuery)
        {
            this.Callback = callback;
            this.CallbackQuery = callbackQuery;
        }

        public TextParams(BotAction action, Message message)
        {
            this.Action = action;
            this.Message = message;
        }
    }
}
