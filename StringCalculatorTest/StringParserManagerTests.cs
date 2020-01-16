using Microsoft.Extensions.Logging;
using Moq;
using StringCalculator.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StringstringParserTest
{
    public class StringParserManagerTests
    {
        private ILogger<StringParserManager> logger;
        private List<string> delimiters;

        public StringParserManagerTests()
        {
            var loggerMock = new Mock<ILogger<StringParserManager>>();
            logger = loggerMock.Object;
            delimiters = new List<string>();
            delimiters.Add(",");
            delimiters.Add(@"\n");
        }
        
        [Fact]
        public void ParseInputString_2Numbers_Added()
        {
            var testString = "11,22";
            var expectedResult = new int[] { 11, 22 };
            var stringParser = SetupStringParserManager(testString, delimiters.ToArray());

            var result = stringParser.ParseInputString(testString, "", false, 1000);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseInputString_1Number1String_ReturnsNumber()
        {
            var testString = "123,asdf";
            var expectedResult = new int[] { 123, 0 };
            var stringParser = SetupStringParserManager(testString, delimiters.ToArray());

            var result = stringParser.ParseInputString(testString, "", false, 1000);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseInputString_1String_ReturnsZero()
        {
            var testString = "qwerty";
            var expectedResult = new int[] { 0 };
            var stringParser = SetupStringParserManager(testString, delimiters.ToArray());

            var result = stringParser.ParseInputString(testString, "", false, 1000);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseInputString_6Numbers_Added()
        {
            var testString = "1,2,3,4,5,6";
            var expectedResult = new int[] { 1, 2, 3, 4, 5, 6 };
            var stringParser = SetupStringParserManager(testString, delimiters.ToArray());

            var result = stringParser.ParseInputString(testString, "", false, 1000);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseInputString_3NumbersWithNewLineDelimiter_Added()
        {
            var testString = @"1\n2,3";
            var expectedResult = new int[] { 1, 2, 3 };
            var stringParser = SetupStringParserManager(testString, delimiters.ToArray());

            var result = stringParser.ParseInputString(testString, "", false, 1000);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseInputString_3NumbersWith2Negatives_ThrowsException()
        {
            var testString = @"11,-11,-22";
            var stringParser = SetupStringParserManager(testString, delimiters.ToArray());

            var exception = Assert.Throws<Exception>(() => stringParser.ParseInputString(testString, "", false, 1000));

            Assert.True(exception.Data.Contains("NegativesEntered"));
            Assert.Equal("-11,-22", exception.Data["NegativesEntered"]);
        }

        [Fact]
        public void ParseInputString_3Numbers_Over1000Invalid()
        {
            var testString = @"1234,11,99999";
            var expectedResult = new int[] { 0, 11, 0 };
            var stringParser = SetupStringParserManager(testString, delimiters.ToArray());

            var result = stringParser.ParseInputString(testString, "", false, 1000);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseInputString_3NumbersWithCustomDelimiter_Added()
        {
            var testString = @"12,13p14";
            var expectedResult = new int[] { 12, 13, 14 };
            delimiters.Add("p");
            var stringParser = SetupStringParserManager(testString, delimiters.ToArray());

            var result = stringParser.ParseInputString(testString, "", false, 1000);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseInputString_3NumbersAllowNegative_Parsed()
        {
            var testString = @"12,-13,-14";
            var expectedResult = new int[] { 12, -13, -14 };
            var stringParser = SetupStringParserManager(testString, delimiters.ToArray());

            var result = stringParser.ParseInputString(testString, "", true, 1000);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseInputString_3Numbers5000UpperBound_Parsed()
        {
            var testString = @"1200,5000,9000";
            var expectedResult = new int[] { 1200, 5000, 0 };
            var stringParser = SetupStringParserManager(testString, delimiters.ToArray());

            var result = stringParser.ParseInputString(testString, "", false, 5000);

            Assert.Equal(expectedResult, result);
        }

        private StringParserManager SetupStringParserManager(string input, string[] delimiters)
        {
            var delimiterMock = new Mock<IDelimiterManager>();
            delimiterMock.Setup(x => x.GetDelimitersFromInput(It.IsAny<string>(), It.IsAny<string>())).Returns((input, delimiters));

            return new StringParserManager(logger, delimiterMock.Object);
        }
        
    }
}
