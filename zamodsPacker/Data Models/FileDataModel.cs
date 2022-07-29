
namespace ZamodsPacker
{
    /// <summary>
    /// Data structure of entries in manifest.dsx file.
    /// </summary>
    public readonly struct FileDataModel
    {
        string Target { get; }
        string Action { get; }
        string Value { get; }

        /// <summary>
        /// Creates the Data representation of an entry in manifest.dsx file.
        /// </summary>
        /// <param name="value">Mostly likely a path to from content directory to intended file. Like: Content/..../something.jpg</param>
        /// <param name="target">Type of item mostly set to "Content".</param>
        /// <param name="action">What is intent to do with given value. Mostly set to "Install"</param>
        public FileDataModel(string value, string target = "Content", string action = "Install")
        {
            Target = target;
            Action = action;
            Value = XMLSpecialCharacterConverter.UnicodeToXMLHandler(value);
        }

        public override string ToString()
        {
            return $" <File TARGET=\"{Target}\" ACTION=\"{Action}\" VALUE=\"{Value}\"/>";
        }
    }
}
