using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Converter
{
    public class NumericConverter : IConverter
    {
        public NumericConverter() : this(string.Empty) { }
        public NumericConverter(string value) : this(value, null) { }

        public NumericConverter(string value, CultureInfo cInfo)
        {
            Value = value;
            CultureInfo = cInfo ?? new DefaultCultureInfo();
        }

        public CultureInfo CultureInfo { get; }

        public bool AddCurrency { get; set; } = false;

        public string Value { get; set; }

        public Result Convert() => Convert(Value);

        public Result Convert(string text2Convert)
        {
            if (!text2Convert.IsDecimalNumber())
                return new Error(string.Format(Properties.Resources.ErrorTextTemplate, text2Convert, Properties.Resources.DecimalSeperator));
            return new Success(ConvertToText(text2Convert));
        }

        private string ConvertToText(string text)
        {
            var parts = text.Replace(" ","").Split(new[] { Properties.Resources.DecimalSeperator }, StringSplitOptions.None);
            var sb = new StringBuilder();
            var hundredsParts = new Stack<string>();
            int firstStrLenght = parts[0].Length;
            for (int i = 0; i <= firstStrLenght/3; i++)
            {
                var startIndex = firstStrLenght - 3 * (i + 1);
                int blockLength = 3;
                if(startIndex < 0)
                {
                    blockLength += startIndex;
                    startIndex = 0;
                    if (blockLength == 0) break;
                }
                var num = parts[0].Substring(startIndex, blockLength);

                if (CultureInfo.LargeNumberNames.TryGetValue(i * 3, out var name))
                    hundredsParts.Push(name);
                hundredsParts.Push(CultureInfo.HundredsToWordRule(num));
            }
            while (hundredsParts.Count > 0)
                sb.Append(hundredsParts.Pop()).Append(hundredsParts.Count > 0 ? " " : string.Empty);

            if (sb.Length == 0)
                sb.Append(CultureInfo.DigitToWordRule("0"));

            if (AddCurrency)
                sb.Append(" ").Append(IsOne(parts[0]) ? CultureInfo.CurrencyName : CultureInfo.CurrencyNamePlural);
            return ParseAfterSeparator(parts.ElementAtOrDefault(1), sb).ToString();
        }

        private StringBuilder ParseAfterSeparator(string num, StringBuilder sb)
        {
            if (string.IsNullOrWhiteSpace(num)) return sb;
            num = num.Trim();
            if (num.Length < 2)
                num += "0";
            else if (num.Length > 2)
                num = num.Substring(0, 2);
            if (System.Convert.ToInt32(num) != 0)
            {
                sb.Append(" ").Append(CultureInfo.Union).Append(" ")
                  .Append(CultureInfo.TensToWordRule(num));
                if (AddCurrency)
                    sb.Append(" ").Append(IsOne(num) ? CultureInfo.HundredthOfCurrencyName : CultureInfo.HundredthOfCurrencyNamePlural);
            }
            return sb;
        }

        private bool IsOne(string num) => num.Trim().TrimStart('0') == "1";
    }
}
