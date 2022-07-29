
namespace ZamodsPacker
{
    public static class XMLSpecialCharacterConverter
    {
        public static string UnicodeToXMLCharacter(char unicode)
        {
            switch (unicode)
            {
                case '"':
                    return "&quote;";
                case '&':
                    return "&amp;";
                case '\'':
                    return "&apos;";
                case '<':
                    return "&lt;";
                case '>':
                    return "&gt;";
                default:
                    return $"{unicode}";
            }
        }

        public static string UnicodeToXMLHandler(string rawString)
        {
            var outputString = "";
            foreach(var character in rawString)
            {
                outputString += UnicodeToXMLCharacter(character);
            }
            return outputString;
        }
    }
}
