using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ZamodsPacker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pass the file path to content directory!");
            string path = Console.ReadLine();
            Console.WriteLine("Pass GLOBALID! Check it in meta data of your product!");
            string globalID = Console.ReadLine();
            var manifesto = new Manifesto();
            manifesto.FinalizeAndWriteToFile(path, globalID).Wait();
            Console.Read();
        }
    }

}
