
namespace ZamodsPacker
{
    public readonly struct FileDataModel
    {
        string Target { get; }
        string Action { get; }
        string Value { get; }

        public FileDataModel(string value, string target = "Content", string action = "Install")
        {
            Target = target;
            Action = action;
            Value = value;
        }

        public override string ToString()
        {
            return $"<File TARGET=\"{Target}\" ACTION=\"{Action}\" VALUE=\"{Value}\"/>";
        }
    }
}
