using System;
using System.IO;
using System.Threading.Tasks;
using System.Text;

namespace ZamodsPacker
{
    /// <summary>
    /// Class intented to generate product supplement.dsx file.
    /// </summary>
    public class Supplemento
    {
        const string SUPPLEMENTHEADER = "<ProductSupplement VERSION=\"0.1\">";
        const string SUPPLEMENTFOOTER = "</ProductSupplement>";
        const string PRODUCTTAGS = "<ProductTags VALUE=\"DAZStudio4_5\"/>";

        /// <summary>
        /// Writes the product supplement file for given product.
        /// </summary>
        /// <param name="path">Path of content folder where generated file will be saved.</param>
        /// <param name="productName">Full name of product for which product supplement file is intented.</param>
        /// <returns>Completed Task upon either failure or success.</returns>
        public async Task FinalizeAndWriteToFile(string path, string productName= "REPLACE THIS WITH PRODUCT NAME")
        {
            try
            {
                string PRODUCTNAME = $"<ProductName VALUE=\"{productName}\"/>";
                string finalText = SUPPLEMENTHEADER;

                finalText += $"\n {PRODUCTNAME}";
                finalText += $"\n {PRODUCTTAGS}";
                finalText += $"\n{SUPPLEMENTFOOTER}";

                string finalFilePath = $"{path}\\Supplement.dsx";

                Encoding utf8 = Encoding.GetEncoding("ISO-8859-1");
                await File.WriteAllTextAsync(finalFilePath, finalText, encoding: utf8);
                Console.WriteLine($"Wrote supplement file successfully to path: {finalFilePath}");
            }
            catch (Exception ex)
            {
                string messageBoxText = $"Failed to write information to supplement file.\n" +
                              $"{ex.Message}";
                string caption = "Process Failed!";
                Console.WriteLine($"{messageBoxText}{caption}");
            }

            await Task.CompletedTask;
        }
    }
}
