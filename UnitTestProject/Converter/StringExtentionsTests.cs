using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Converter.Tests
{
    [TestClass()]
    public class StringExtentionsTests
    {
        /// <summary>
        /// This method test numbers with ','(comma) as separator and up to 2 digits after comma 
        /// </summary>
        [DataTestMethod()]
        [DataRow("", false)]
        [DataRow("asbsdfsff", false)]
        [DataRow("1123 1212,123", false)]
        [DataRow("1123 1212.12", false)]
        [DataRow("1", true)]
        [DataRow("0", true)]
        [DataRow("0,12", true)]
        [DataRow(",12", true)]
        [DataRow("  1123 1212", true)]
        [DataRow("  + 1123 1212", true)]
        [DataRow("1123 1212,12", true)]
        [DataRow("1123 1212,1", true)]
        public void IsDecimalNumberTest(string text, bool expected) => Assert.AreEqual(expected, text.IsDecimalNumber());
    }
}