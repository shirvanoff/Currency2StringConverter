using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Converter.Test
{
    [TestClass]
    public class TestNumericConverter
    {
        [TestMethod]
        public void CreateConverterTest()
        {
            NumericConverter converter = new NumericConverter();
            Assert.IsNotNull(converter);
        }

        [DataTestMethod]
        [DataRow("123456")]
        [DataRow("abcdfrt")]
        public void CreateConverterWithValueTest(string value)
        {
            NumericConverter converter = new NumericConverter(value);
            Assert.AreEqual(value, converter.Value);
        }

        [DataTestMethod]
        [DataRow("0", "zero")]
        [DataRow("1", "one")]
        [DataRow("2", "two")]
        [DataRow("3", "three")]
        [DataRow("4", "four")]
        [DataRow("5", "five")]
        [DataRow("6", "six")]
        [DataRow("7", "seven")]
        [DataRow("8", "eight")]
        [DataRow("9", "nine")]
        [DataRow("10", "ten")]
        [DataRow("11", "eleven")]
        [DataRow("12", "twelve")]
        [DataRow("15", "fifteen")]
        [DataRow("81", "eighty-one")]
        [DataRow("30", "thirty")]
        [DataRow("45 100", "forty-five thousand one hundred")]
        public void ConvertDigitsToTextSuccessTest(string value, string expected)
        {
            NumericConverter converter = new NumericConverter(value);
            var result = converter.Convert();
            Assert.IsInstanceOfType(result, typeof(Success));
            Assert.AreEqual(expected, Convert.ToString(result.Value));
        }

        [DataTestMethod]
        [DataRow(",0", "zero")]
        [DataRow(",1", "zero and ten")]
        [DataRow(",2", "zero and twenty")]
        [DataRow(",24", "zero and twenty-four")]
        [DataRow("45 100,24", "forty-five thousand one hundred and twenty-four")]
        public void ConvertDigitsToTextAfterSeparatorSuccessTest(string value, string expected)
        {
            NumericConverter converter = new NumericConverter(value);
            var result = converter.Convert();
            Assert.IsInstanceOfType(result, typeof(Success));
            Assert.AreEqual(expected, Convert.ToString(result.Value));
        }

        [DataTestMethod]
        [DataRow(",0", "zero dollars")]
        [DataRow("0", "zero dollars")]
        [DataRow("0,01", "zero dollars and one cent")]
        [DataRow(",1", "zero dollars and ten cents")]
        [DataRow("1", "one dollar")]
        [DataRow("1,01", "one dollar and one cent")]
        [DataRow("1,21", "one dollar and twenty-one cents")]
        [DataRow("24", "twenty-four dollars")]
        [DataRow("45 100,24", "forty-five thousand one hundred dollars and twenty-four cents")]
        public void ConvertDigitsToTextWithCurrencySuccessTest(string value, string expected)
        {
            NumericConverter converter = new NumericConverter(value)
            {
                AddCurrency = true,
            };
            var result = converter.Convert();
            Assert.IsInstanceOfType(result, typeof(Success));
            Assert.AreEqual(expected, Convert.ToString(result.Value));
        }

        [DataTestMethod]
        [DataRow("abdscd")]
        [DataRow("1d1")]
        public void ConvertDigitsToTextFailedTest(string value)
        {
            NumericConverter converter = new NumericConverter(value);
            var result = converter.Convert();
            Assert.IsInstanceOfType(result, typeof(Error));
        }

    }
}
