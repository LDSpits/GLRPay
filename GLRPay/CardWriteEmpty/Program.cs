using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFC7;

namespace CardWriteEmpty
{
    class Program
    {

        static void Main(string[] args)
        {
            NFC7handler handler = new NFC7handler();
            handler.CardInsertEvent += ((sender, eventarg) => {
                handler.EraseCard();
            });

            Console.WriteLine("insert card to erase");
            Console.ReadKey();
        }
    }
}
