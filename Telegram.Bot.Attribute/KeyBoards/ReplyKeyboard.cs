using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Attribute.KeyBoards
{
    public class ReplyKeyboard
    {
        public static List<List<KeyboardButton>> ReplyKeyBoards { get; set; }

        public ReplyKeyboard()
        {
            ReplyKeyBoards = new List<List<KeyboardButton>>();
        }

        public ReplyKeyboardMarkup Get()
        {
            return new ReplyKeyboardMarkup(ReplyKeyBoards, true);
        }

        public List<List<KeyboardButton>> GetReply()
        {
            return ReplyKeyBoards;
        }

        public int AddRows(KeyboardButton button)
        {
            ReplyKeyBoards.Add(new List<KeyboardButton>()
            {
                button
            });
            return ReplyKeyBoards.Count;
        }

        public int AddCollumns(int rows, KeyboardButton button)
        {
            rows = rows - 1;
            ReplyKeyBoards[rows].Add(button);
            return ReplyKeyBoards[rows].Count;
        }


        public int AddCollumnButton = 0;

        public List<KeyboardButton> AddCollumns(KeyboardButton button, int maxColumn = 2)
        {
            if ( AddCollumnButton == 0 )
                ReplyKeyBoards.Add(new List<KeyboardButton>());

            AddCollumnButton++;

            int rows = ReplyKeyBoards.Count - 1;
            ReplyKeyBoards[rows].Add(button);

            if ( AddCollumnButton == maxColumn )
                AddCollumnButton = 0;

            var data = ReplyKeyBoards[rows];
            return data;
        }
    }
}
