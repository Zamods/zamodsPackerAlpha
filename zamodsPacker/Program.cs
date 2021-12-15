using System;
using System.IO;
using System.Threading.Tasks;

namespace ZamodsPacker
{
    class Program
    {
        static void Main(string[] args)
        {
            int result = 1;

            PackerHeading();
            Console.WriteLine("Do you want to generate package name?");
            Console.WriteLine("Type \"1\" for yes or \"0\" for no!");
            result = CheckUserResponse();
            
            if(result == 1)
            {
                var packageNamer = new PackageNamer();
                packageNamer.GeneratePackageName();
            }
            Console.Clear();

            PackerHeading();
            Console.WriteLine("Do you want to create manifest & supplement files for more than one product?");
            Console.WriteLine("Type \"1\" for yes or \"0\" for no!");
            result = CheckUserResponse();

            if(result == 1)
            {
                PackerHeading();
                Console.WriteLine("Do you want to input content paths, globalID's and products names via text file?");
                Console.WriteLine("Type \"1\" for yes or \"0\" for no!");
                result = CheckUserResponse();
                if (result == 1)
                {
                    ParseBatchAndExecuteFile().Wait();
                }
                else
                {
                    do
                    {
                        OneTimeExecution().Wait();
                        Console.WriteLine("Do you want to continue?");
                        Console.WriteLine("Type \"1\" for yes or \"0\" for no!");
                        result = CheckUserResponse();
                    }
                    while (result != 0);
                    PackerHeading();
                }
            }
            else
            {
                OneTimeExecution().Wait();
            }
            Console.WriteLine("\n");
            Console.WriteLine("Done processing!\nThank you!");
            Console.ReadKey();
        }

        private static async Task OneTimeExecution()
        {
            // User input
            PackerHeading();
            Console.WriteLine("Pass the file path to content directory!\nTip: C:\\Whatever\\Content");
            string path = Console.ReadLine();
            Console.Clear();

            PackerHeading();
            Console.WriteLine("Pass GLOBALID! \nTip: Check it in metadata of your product!");
            Console.WriteLine("Content\\Runtime\\Support\\yourproduct.dsx");
            string globalID = Console.ReadLine();
            Console.Clear();

            PackerHeading();
            Console.WriteLine("Pass Product Name to generate \"Supplement.dsx\"! \nTip: Check it in metadata of your product!");
            Console.WriteLine("Content\\Runtime\\Support\\yourproduct.dsx");
            string productName = Console.ReadLine();
            Console.Clear();

            PackerHeading();
            DisplayAddedInput(path, globalID, productName);

            Console.WriteLine("\n");
            // Data processing for "Manifest.dsx"
            var manifesto = new Manifesto();
            await manifesto.FinalizeAndWriteToFile(path, globalID);

            Console.WriteLine("\n");
            // Data processing for "Supplement.dsx"
            var supplemento = new Supplemento();
            await supplemento.FinalizeAndWriteToFile(path, productName);
        }
        
        private static async Task ParseBatchAndExecuteFile()
        {
            try
            {
                PackerHeading();
                Console.WriteLine("Tip: File should be formatted as of such...");
                Console.WriteLine("path\"\"globalID\"\"productName\n");
                Console.WriteLine("Now pass the file path of products in text file!");
                string path = Console.ReadLine();
                var fileContent = await File.ReadAllLinesAsync(path);
                foreach (var dataChunk in fileContent)
                {
                    var split = dataChunk.Split("\"\"");
                    Console.WriteLine("\n");
                    // Data processing for "Manifest.dsx"
                    var manifesto = new Manifesto();
                    await manifesto.FinalizeAndWriteToFile(split[0], split[1]);
                    
                    Console.WriteLine("\n");
                    // Data processing for "Supplement.dsx"
                    var supplemento = new Supplemento();
                    await supplemento.FinalizeAndWriteToFile(split[0], split[2]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid file type or structure!");
                Console.WriteLine(ex.Message);
            }
        }

        private static int CheckUserResponse()
        {
            int result;
            bool response = int.TryParse(Console.ReadLine(), out result);
            while (!response)
            {
                Console.WriteLine("Invalid input!\nType \"1\" for yes or \"0\" for no!");
                response = int.TryParse(Console.ReadLine(), out result);
            }
            Console.Clear();
            return result;
        }

        private static void DisplayAddedInput(string path, string globalID, string productName)
        {
            Console.WriteLine("Your input data is as of such!");
            Console.WriteLine($"Content Directory Path: {path}\n");
            Console.WriteLine($"Product Global ID: {globalID}\n");
            Console.WriteLine($"Product Name: {productName}\n");
        }

        private static void PackerHeading()
        {
            Console.WriteLine("zamodsPacker Alpha v.0.0.2\n");
            Console.WriteLine("Manifest and Supplement generator for Dad Studio aKA Daz Studio.\n");
            Console.WriteLine("Links: https://shorturl.at/jquBG");
            Console.WriteLine("Github: https://github.com/Zamods/zamodsPackerAlpha");
            Console.WriteLine("********************************************************************\n");
        }
    }

}
