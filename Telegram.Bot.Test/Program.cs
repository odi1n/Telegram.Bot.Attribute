using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Attribute;

namespace Telegram.Bot.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TgAttribute class1 = new TgAttribute();
            Console.WriteLine();
            Console.ReadKey();
        }

        public class TestClass1
        {
            public void Method1()
            {
                Console.WriteLine("TestClass1->Method1");
            }

            [Command(new []{"Привет"}, 1)]
            public void Method2(string[] command, int access)
            {
                Console.WriteLine("TestClass1->Method2");
            }
        }
    }
}
