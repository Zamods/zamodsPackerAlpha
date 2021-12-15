
namespace ZamodsPacker
{
    /// <summary>
    /// Data structure of DIM package Name.
    /// </summary>
    public struct PackageNameModel
    {
        string SourcePrefix { get; }
        string ProductSKU { get; }
        string PackageID { get; }
        string ProductName { get; }

        public PackageNameModel(string sourcePrefix, string productSKU, string packageID, string productName)
        {
            SourcePrefix = sourcePrefix;
            ProductSKU = productSKU;
            PackageID = packageID;
            ProductName = productName;
        }

        public override string ToString()
        {
            return $"{SourcePrefix}{ProductSKU}-{PackageID}_{ProductName}.zip";
        }
    }
}
