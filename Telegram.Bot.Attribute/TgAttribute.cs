using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.SystemConsole.Themes;
using Telegram.Bot.Args;
using Telegram.Bot.Attribute.Bot;
using Telegram.Bot.Attribute.Params;
using Telegram.Bot.Types.Enums;
using BotCommand = Telegram.Bot.Attribute.Bot.BotCommand;

namespace Telegram.Bot.Attribute
{
    public class TgAttribute
    {
        private Logger Log { get; set; }

        public TgAttribute()
        {
            Log = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:dd.MM HH:mm:ss}] [{Level:u3}] - {Message} {NewLine} {Exception}",
                    theme: SystemConsoleTheme.Literate
                ).Enrich.WithProperty("Version", "1.0.0")
                .CreateLogger();

            Log.Warning("Set Log");
        }

        public void SetCallback(CallbackQueryEventArgs callbackQueryEventArgs, int access)
        {
            if (callbackQueryEventArgs.CallbackQuery.Message.Type != MessageType.Text) return;
            var methods = GetMethod<CallbackAttribute>();

            foreach (var method in methods) // iterate through all found methods
            {
                var attributes = method.GetCustomAttributes(false);

                var attributesAll = attributes.Select(CheckTypeCallback).ToList();
                attributesAll.RemoveAll(x => x == null);

                var attribute = attributesAll.FirstOrDefault();
                if (attribute == null)
                    return;

                var botCallback = new BotCallback();
                botCallback.Parse(callbackQueryEventArgs.CallbackQuery.Data);

                if (attribute.Access > access) continue;

                if (!attribute.Commands.All(x => String.Equals(x, botCallback.AllText, StringComparison.CurrentCultureIgnoreCase) ||
                                                 String.Equals(x, botCallback.Command, StringComparison.CurrentCultureIgnoreCase) ||
                                                 String.Equals(x, botCallback.NoCommand, StringComparison.CurrentCultureIgnoreCase)))
                    continue;

                Log.Information(string.Join("", new List<string>()
                    {
                        $"Message Id: {callbackQueryEventArgs.CallbackQuery.Message.MessageId}, ",
                        $"Callback: {callbackQueryEventArgs.CallbackQuery.Data}, ",
                        $"Chat: {callbackQueryEventArgs.CallbackQuery.Message.Chat.Id}, ",
                        $"From: {callbackQueryEventArgs.CallbackQuery.From}, ",
                        $"Type: {callbackQueryEventArgs.CallbackQuery.Message.Type}, ",
                        $"Date: {callbackQueryEventArgs.CallbackQuery.Message.Type}, ",
                    }));

                var obj = Activator.CreateInstance(method.DeclaringType); // Instantiate the class
                method.Invoke(obj, parameters: new object[]
                    {
                        new TextParams(callback:botCallback, callbackQuery:callbackQueryEventArgs.CallbackQuery),
                    }
                ); // invoke the method

                break;
            }
        }

        public void SetCommand(MessageEventArgs messageEventArgs, int access)
        {
            if (messageEventArgs.Message.Type != MessageType.Text) return;
            var methods = GetMethod<CommandAttribute>();

            foreach (var method in methods) // iterate through all found methods
            {
                var attributes = method.GetCustomAttributes(false);

                var attributesAll = attributes.Select(CheckTypeCommand).ToList();
                attributesAll.RemoveAll(x => x == null);

                var attribute = attributesAll.FirstOrDefault();
                if (attribute == null)
                    return;

                var botCommand = new BotCommand();
                botCommand.Parse(messageEventArgs.Message.Text);

                if (attribute.Access > access) continue;
                if (!attribute.Commands.All(x => String.Equals(x, botCommand.AllText, StringComparison.CurrentCultureIgnoreCase) ||
                                                 String.Equals(x, botCommand.Command, StringComparison.CurrentCultureIgnoreCase) ||
                                                 String.Equals(x, botCommand.NoCommand, StringComparison.CurrentCultureIgnoreCase)))
                    continue;

                Log.Information(string.Join("", new List<string>()
                    {
                        $"Message Id: {messageEventArgs.Message.MessageId}, ",
                        $"Text: {messageEventArgs.Message.Text}, ",
                        $"Chat: {messageEventArgs.Message.Chat.Id}, ",
                        $"From: {messageEventArgs.Message.From}, ",
                        $"Type: {messageEventArgs.Message.Type}, ",
                        $"Date: {messageEventArgs.Message.Date}, ",
                    }));

                var obj = Activator.CreateInstance(method.DeclaringType); // Instantiate the class
                method.Invoke(obj, parameters: new object[]
                    {
                        new TextParams(botCommand, message:messageEventArgs.Message),
                    }
                ); // invoke the method

                break;
            }
        }
       
        public void SetAction(MessageEventArgs messageEventArgs, string workName)
        {
            if (messageEventArgs.Message.Type != MessageType.Text) return;
            var methods = GetMethod<ActionAttribute>();

            foreach (var method in methods) // iterate through all found methods
            {
                var attributes = method.GetCustomAttributes(false);

                var attributesAll = attributes.Select(CheckWork).ToList();
                attributesAll.RemoveAll(x => x == null);

                var attribute = attributesAll.FirstOrDefault();
                if (attribute == null)
                    return;

                if (attribute.WorkName != workName) continue;

                var botAction = new BotAction(messageEventArgs.Message.Text);

                Log.Information(string.Join("", new List<string>()
                    {
                        $"Message Id: {messageEventArgs.Message.MessageId}, ",
                        $"Text: {messageEventArgs.Message.Text}, ",
                        $"Chat: {messageEventArgs.Message.Chat.Id}, ",
                        $"From: {messageEventArgs.Message.From}, ",
                        $"Type: {messageEventArgs.Message.Type}, ",
                        $"Date: {messageEventArgs.Message.Date}, ",
                        $"Work: {workName}, ",
                    }));

                var obj = Activator.CreateInstance(method.DeclaringType); // Instantiate the class
                method.Invoke(obj, parameters: new object[]
                    {
                        new TextParams(action:botAction, message:messageEventArgs.Message),
                    }
                ); // invoke the method
                break;
            }
        }


        private IEnumerable<MethodInfo> GetMethod<T>()
        {
            return AppDomain.CurrentDomain.GetAssemblies() // Returns all currenlty loaded assemblies
                .SelectMany(x => x.GetTypes()) // returns all types defined in this assemblies
                .Where(x => x.IsClass) // only yields classes
                .SelectMany(x => x.GetMethods()) // returns all methods defined in those classes
                .Where(x => x.GetCustomAttributes(typeof(T), false).FirstOrDefault() !=
                            null); // returns only methods that have the CommandAttribute
        }

        private CallbackAttribute CheckTypeCallback(object attribute)
        {
            CallbackAttribute callbackAttribute = null;
            try
            {
                callbackAttribute = (CallbackAttribute)attribute;
                return callbackAttribute;
            }
            catch
            {
                return null;
            }
        }
       
        private CommandAttribute CheckTypeCommand(object attribute)
        {
            CommandAttribute commandAttribute = null;
            try
            {
                commandAttribute = (CommandAttribute)attribute;
                return commandAttribute;
            }
            catch
            {
                return null;
            }
        }

        private ActionAttribute CheckWork(object attribute)
        {
            ActionAttribute actionAttribute = null;
            try
            {
                actionAttribute = (ActionAttribute)attribute;
                return actionAttribute;
            }
            catch
            {
                return null;
            }
        }
    }
}
