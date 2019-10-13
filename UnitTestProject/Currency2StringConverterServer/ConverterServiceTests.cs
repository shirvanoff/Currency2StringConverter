using Microsoft.VisualStudio.TestTools.UnitTesting;
using Currency2StringConverterServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Currency2StringConverterServer.Tests
{
    [TestClass()]
    public class ConverterServiceTests
    {
        [TestMethod()]
        public void TestConnetionTest()
        {
            ConverterService service = new ConverterService();
            Assert.AreEqual("OK", service.TestConnetion());
        }

        [DataTestMethod]
        [DataRow("0", "zero dollars")]
        [DataRow("1", "one dollar")]
        [DataRow("25,1", "twenty-five dollars and ten cents")]
        [DataRow("0,01", "zero dollars and one cent")]
        [DataRow("45 100", "forty-five thousand one hundred dollars")]
        [DataRow("999 999 999,99", "nine hundred ninety-nine million nine hundred ninety-nine thousand nine hundred ninety-nine dollars and ninety-nine cents")]
        [DataRow("45.100", "Text 45.100 is not a number or you may use wrong Decimal Separator(should be ,)")]
        [DataRow("-45 100", "Text -45 100 is not a number or you may use wrong Decimal Separator(should be ,)")]
        [DataRow("assdfdg", "Text assdfdg is not a number or you may use wrong Decimal Separator(should be ,)")]
        public void ConvertStringTest(string value, string expected)
        {
            ConverterService service = new ConverterService();
            var result = service.ConvertString(value);
            Assert.AreEqual(expected, result);
        }
    }
}