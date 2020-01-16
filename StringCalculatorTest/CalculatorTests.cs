using Microsoft.Extensions.Logging;
using Moq;
using StringCalculator;
using System;
using System.Collections.Generic;
using Xunit;

namespace StringCalculatorTest
{
    public class CalculatorTests
    {
        private ILogger<Calculator> logger;
        private List<string> delimiters;

        public CalculatorTests()
        {
            var loggerMock = new Mock<ILogger<Calculator>>();
            logger = loggerMock.Object;
            delimiters = new List<string>();
            delimiters.Add(",");
            delimiters.Add(@"\n");
        }

        [Fact]
        public void ParseStringAndCalculate_2Numbers_Added()
        {
            var testString = "11,22";
            var expectedResult = 33;
            var calculator = SetupCalculator(testString, delimiters.ToArray());

            var result = calculator.ParseStringAndCalculate(testString);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseStringAndCalculate_1Number1String_ReturnsNumber()
        {
            var testString = "123,asdf";
            var expectedResult = 123;
            var calculator = SetupCalculator(testString, delimiters.ToArray());

            var result = calculator.ParseStringAndCalculate(testString);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseStringAndCalculate_1String_ReturnsZero()
        {
            var testString = "qwerty";
            var expectedResult = 0;
            var calculator = SetupCalculator(testString, delimiters.ToArray());

            var result = calculator.ParseStringAndCalculate(testString);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseStringAndCalculate_6Numbers_Added()
        {
            var testString = "1,2,3,4,5,6";
            var expectedResult = 21;
            var calculator = SetupCalculator(testString, delimiters.ToArray());

            var result = calculator.ParseStringAndCalculate(testString);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseStringAndCalculate_3NumbersWithNewLineDelimiter_Added()
        {
            var testString = @"1\n2,3";
            var expectedResult = 6;
            var calculator = SetupCalculator(testString, delimiters.ToArray());

            var result = calculator.ParseStringAndCalculate(testString);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseStringAndCalculate_3NumbersWith2Negatives_ThrowsException()
        {
            var testString = @"11,-11,-22";
            var calculator = SetupCalculator(testString, delimiters.ToArray());

            var exception = Assert.Throws<Exception>(() => calculator.ParseStringAndCalculate(testString));

            Assert.True(exception.Data.Contains("NegativesEntered"));
            Assert.Equal("-11,-22", exception.Data["NegativesEntered"]);
        }

        [Fact]
        public void ParseStringAndCalculate_3Numbers_Over1000Invalid()
        {
            var testString = @"1234,11,99999";
            var expectedResult = 11;
            var calculator = SetupCalculator(testString, delimiters.ToArray());

            var result = calculator.ParseStringAndCalculate(testString);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseStringAndCalculate_3NumbersWithCustomDelimiter_Added()
        {
            var testString = @"12,13p14";
            var expectedResult = 39;
            delimiters.Add("p");
            var calculator = SetupCalculator(testString, delimiters.ToArray());

            var result = calculator.ParseStringAndCalculate(testString);

            Assert.Equal(expectedResult, result);
        }

        private Calculator SetupCalculator(string input, string[] delimiters)
        {
            var delimiterMock = new Mock<IDelimiterManager>();
            delimiterMock.Setup(x => x.GetDelimitersFromInput(It.IsAny<string>())).Returns((input, delimiters));

            return new Calculator(logger, delimiterMock.Object);
        }
    }
}
