using System.Globalization;
using System.Text.RegularExpressions;

namespace Converter
{
    public static class StringExtentions
    {
        private static NumberFormatInfo nfi = NumberFormatInfo.CurrentInfo;
        private static string pattern = @"^\s*[" + Regex.Escape(nfi.PositiveSign + nfi.NegativeSign) + @"]?\s?" 
                                        + @"((\d?\s?)*" + Regex.Escape(Properties.Resources.DecimalSeperator) + @"?\d?\d?){1}$";
        private static readonly Regex rgx = new Regex(pattern);
        public static bool IsDecimalNumber(this string str) => !string.IsNullOrWhiteSpace(str) && rgx.IsMatch(str);
    }
}
