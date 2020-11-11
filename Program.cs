using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using System;
using System.IO;
using System.Text;

namespace WidevineClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Tests.Test();
            Console.WriteLine();
            Tests.Test2();
            Console.WriteLine();
            Tests.Test3();
            Console.WriteLine();
            Tests.Test4();
        }
    }
}
