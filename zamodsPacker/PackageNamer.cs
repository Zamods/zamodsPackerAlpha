using System;

namespace ZamodsPacker
{
    /// <summary>
    /// Class intented to generate product package name that is compatible with DIM.
    /// </summary>
    public class PackageNamer
    {
        public void GeneratePackageName()
        {
            string sourcePrefix = FormatSourcePrefix();
            string productSKU = FormatProductSKU();
            string packageID = FormatPackageID();
            string productName = FormatProductName();

            var packageNameModel = new PackageNameModel(sourcePrefix, productSKU, packageID, productName);
            Console.WriteLine(packageNameModel);
            Console.ReadKey();
            //File.WriteAllText(path, packageNameModel.ToString());
        }
        private string FormatProductName()
        {
            string userInput;

            Console.WriteLine("Type \"Product Name\" for your package.");
            userInput = Console.ReadLine();
            PurgeSpacesAndSpecialCharacters(ref userInput);
            Console.Clear();

            return userInput;
        }
        private string FormatPackageID()
        {
            string userInput;
            bool isCheckOk;
            do
            {
                Console.WriteLine("Type \"Product ID\" for your package.\nLike: \"01\", \"22\"...");
                userInput = Console.ReadLine();
                isCheckOk = IsStringDigits(userInput);
                if (!isCheckOk)
                {
                    Console.WriteLine($"Provided Product ID: \"{userInput}\" is not digits!");
                }
                Console.Clear();
            } while (!isCheckOk);

            LeftStringPadding(ref userInput, "0", 2);

            return userInput;
        }

        private string FormatProductSKU()
        {
            string userInput;
            bool isCheckOk;
            do
            {
                Console.WriteLine("Type \"Product SKU\" for your package.\nLike: \"46535\"...");
                userInput = Console.ReadLine();
                isCheckOk = IsProductSKUCorrect(userInput);
                Console.Clear();
            } while (!isCheckOk);

            LeftStringPadding(ref userInput, "0", 8);

            return userInput;
        }

        private string FormatSourcePrefix()
        {
            bool isCheckOk;
            string userInput;
            do
            {
                Console.WriteLine("Type \"Source Prefix\" for your package.\nLike: \"IM\", \"DAZ3D\"...");
                userInput = Console.ReadLine();
                isCheckOk = IsSourcePrefixCorrect(userInput);
                Console.Clear();
            } while (!isCheckOk);

            return userInput.ToUpper();
        }

        private bool IsProductSKUCorrect(string productSKU)
        {
            if (!IsStringDigits(productSKU))
            {
                Console.WriteLine($"Provided Product SKU: \"{productSKU}\" is not digits!");
                return false;
            }

            if (productSKU.Length > 8 || productSKU.Length < 1)
            {
                Console.WriteLine("Total digits of \"Product SKU\" should be not be greater than 8 and lesser than 1!");
                Console.WriteLine($"Your input: {productSKU.Length}");
                return false;
            }

            return true;
        }

        private bool IsSourcePrefixCorrect(string sourcePrefix)
        {
            if (!Char.IsLetter(sourcePrefix[0]))
            {
                Console.WriteLine("First character of \"Source Prefix\" should be capital letter only.");
                Console.WriteLine($"Your input: {sourcePrefix[0]}");
                return false;
            }

            if(sourcePrefix.Length > 6 || sourcePrefix.Length < 1)
            {
                Console.WriteLine("Total characters of \"Source Prefix\" should be not be greater than 7 and lesser than 1!");
                Console.WriteLine($"Your input: {sourcePrefix.Length}");
                return false;
            }

            return true;
        }

        private bool IsStringDigits(string str)
        {
            foreach (var c in str)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void LeftStringPadding(ref string source, string padding, int outputLength)
        {
            if (source.Length < outputLength)
            {
                int count = outputLength - source.Length;
                for (int i = 0; i < count; i++)
                {
                    source = source.Insert(0, padding);
                }
            }
        }

        private void PurgeSpacesAndSpecialCharacters(ref string source)
        {
            string processedStr = "";
            foreach(var c in source)
            {
                if(Char.IsLetter(c))
                {
                    processedStr += c;
                }
            }
            source = processedStr;
        }
    }
}
