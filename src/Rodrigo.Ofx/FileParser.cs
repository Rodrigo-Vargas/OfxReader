using System.Text.RegularExpressions;

namespace Rodrigo.Ofx
{
    public class FileParser
    {
        public static string GetOfxBody(string fileContent)
        {
            var result = Regex.Match(fileContent, "(<OFX>.+</OFX>)", RegexOptions.Singleline);

            return result.Groups[0].Value.Replace("\n", "").Replace("\r", "");
        }
    }
}
