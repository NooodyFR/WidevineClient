using System;
using System.Collections.Generic;
using System.Text;

namespace WidevineClient
{
    class Logger
    {
        public static void Cyan(object text, bool time = true)
        {
            if (time)
                Console.Write(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss.fff]") + " - ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (time)
                Console.Write(text);
            else
                Console.Write("                            " + text);
            Console.ResetColor();
            Console.WriteLine();
        }

        public static void Print(object text, bool enter = true)
        {
            Console.Write(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss.fff]") + " - " + text);
            if (enter) Console.WriteLine();
        }
    }
}
