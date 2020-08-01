using System.Text.RegularExpressions;

namespace Timesheets.Services
{
    public static class StringExtensions
    {
        private static readonly Regex RedundantSpacesRegex = new Regex(@"(?<=\s)\s+");

        public static string RemoveDoubleSpaces(this string input)
        {
            return RedundantSpacesRegex.Replace(input, string.Empty);
        }
    }
}