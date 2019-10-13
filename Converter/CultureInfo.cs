using System;
using System.Collections.Generic;
using System.Text;

namespace Converter
{
    public class CultureInfo
    {
        public string Union { get; set; }

        public string CurrencyName { get; set; }
        public string CurrencyNamePlural { get; set; }
        public string HundredthOfCurrencyName { get; set; }
        public string HundredthOfCurrencyNamePlural { get; set; }

        public Dictionary<string, string> DigitToWordMap { get; set; }
        public Dictionary<string, string> TensNumToWordMap { get; set; }
        public Dictionary<string, string> SpecialTensDigitToWordMap { get; set; }

        public Dictionary<int, string> LargeNumberNames { get; set; }

        public Func<string, string> DigitToWordRule { get; set; }
        public Func<string, string> TensToWordRule { get; set; }
        public Func<string, string> HundredsToWordRule { get; set; }
    }

    /// <summary>
    /// In DefaultCultureInfo there is no checking that the input string is number. Assuming that the input string is always like "0" or "1" or "12" or "123".  
    /// </summary>
    public sealed class DefaultCultureInfo : CultureInfo
    {
        public DefaultCultureInfo()
        {
            Union = "and";
            CurrencyName = "dollar";
            HundredthOfCurrencyName = "cent";
            CurrencyNamePlural = "dollars";
            HundredthOfCurrencyNamePlural = "cents";

            DigitToWordMap = new Dictionary<string, string>
            {
                { "0", "zero" },
                { "1", "one" },
                { "2", "two" },
                { "3", "three" },
                { "4", "four" },
                { "5", "five" },
                { "6", "six" },
                { "7", "seven" },
                { "8", "eight" },
                { "9", "nine" },
            };

            TensNumToWordMap = new Dictionary<string, string>
            {
                { "10", "ten" },
                { "11", "eleven" },
                { "12", "twelve" },
                { "13", "thirteen" },
            };

            SpecialTensDigitToWordMap = new Dictionary<string, string>
            {
                { "2", "twen" },
                { "3", "thir" },
                { "4", "for" },
                { "5", "fif" },
                { "8", "eigh" },
            };

            LargeNumberNames = new Dictionary<int, string>
            {
                {2, "hundred" },
                {3, "thousand" },
                {6, "million" },
                {9, "billion" },
                {12, "trillion" }
            };

            DigitToWordRule = d => DigitToWordMap.ContainsKey(d) ? DigitToWordMap[d] : d;

            TensToWordRule = num =>
            {
                var temp = num.Trim();
                if (string.IsNullOrWhiteSpace(temp)) temp = "00";
                if (temp.Length < 2) temp = "0" + temp;

                var digit = temp[temp.Length - 2].ToString();
                var result = new StringBuilder();
                string lastDigit = temp[temp.Length - 1].ToString();
                switch (digit)
                {
                    case "0":
                        result.Append(DigitToWordRule(lastDigit));
                        break;
                    case "1":
                        temp = digit + lastDigit;
                        if (TensNumToWordMap.ContainsKey(temp))
                            result.Append(TensNumToWordMap[temp]);
                        else
                            result.Append(SpecialTensDigitToWordMap.ContainsKey(lastDigit) && digit != "4" ? SpecialTensDigitToWordMap[lastDigit] : DigitToWordRule(lastDigit))
                                  .Append("teen");
                        break;
                    default:
                        var begin = SpecialTensDigitToWordMap.ContainsKey(digit) ? SpecialTensDigitToWordMap[digit] : DigitToWordRule(digit);
                        var end = lastDigit;
                        result.Append(SpecialTensDigitToWordMap.ContainsKey(digit) ? SpecialTensDigitToWordMap[digit] : DigitToWordRule(digit))
                              .Append("ty")
                              .Append(end != "0" ? "-" + DigitToWordRule(end) : string.Empty);
                        break;
                }
                return result.ToString();
            };

            HundredsToWordRule = num =>
            {
                var temp = num.Trim();
                if (string.IsNullOrWhiteSpace(temp)) temp = "00";
                if (temp.Length <= 2) return TensToWordRule(temp);
                var digit = temp[temp.Length - 3].ToString();

                var sb = new StringBuilder();
                if (digit != "0")
                    sb.Append(DigitToWordRule(digit))
                      .Append(" ")
                      .Append(LargeNumberNames[2]);
                var tens = temp.Substring(temp.Length - 2, 2);
                if (sb.Length == 0 || tens != "00" && sb.Length > 0)
                    sb.Append(sb.Length == 0 ? string.Empty : " ").Append(TensToWordRule(tens));
                return sb.ToString();
            };
        }
    }
}
