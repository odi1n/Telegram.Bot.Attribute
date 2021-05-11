using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Attribute.KeyBoards
{
    public class InlineKeyboard
    {
        public static List<List<InlineKeyboardButton>> InlineKeyBoards { get; set; }

        public InlineKeyboard()
        {
            InlineKeyBoards = new List<List<InlineKeyboardButton>>();
        }

        public InlineKeyboardMarkup Get()
        {
            return new InlineKeyboardMarkup(InlineKeyBoards);
        }

        public List<List<InlineKeyboardButton>> GetReply()
        {
            return InlineKeyBoards;
        }

        public int AddRows(InlineKeyboardButton button)
        {
            InlineKeyBoards.Add(new List<InlineKeyboardButton>()
            {
                button
            });
            return InlineKeyBoards.Count;
        }

        public int AddRows(List<InlineKeyboardButton> button)
        {
            InlineKeyBoards.Add(button);
            return InlineKeyBoards.Count;
        }

        public int AddCollumns(int rows, InlineKeyboardButton button)
        {
            InlineKeyBoards[rows].Add(button);
            return InlineKeyBoards[rows].Count;
        }


        public int AddCollumnButton = 0;

        public List<InlineKeyboardButton> AddCollumns(InlineKeyboardButton button, int maxColumn = 2)
        {
            if ( AddCollumnButton == 0 )
                InlineKeyBoards.Add(new List<InlineKeyboardButton>());

            AddCollumnButton++;

            int rows = InlineKeyBoards.Count - 1;
            InlineKeyBoards[rows].Add(button);

            if ( AddCollumnButton == maxColumn )
                AddCollumnButton = 0;

            var data = InlineKeyBoards[rows];
            return data;
        }

    }
}
