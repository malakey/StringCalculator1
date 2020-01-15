using Microsoft.Extensions.Logging;
using Moq;
using StringCalculator;
using System;
using Xunit;

namespace StringCalculatorTest
{
    public class CalculatorTests
    {
        private Calculator calculator;

        public CalculatorTests()
        {
            var mock = new Mock<ILogger<Calculator>>();
            ILogger<Calculator> logger = mock.Object;
            calculator = new Calculator(logger);
        }

        [Fact]
        public void ParseStringAndCalculate_2Numbers_Added()
        {
            var testString = "11,22";
            var expectedResult = 33;

            var result = calculator.ParseStringAndCalculate(testString);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseStringAndCalculate_1Number1String_ReturnsNumber()
        {
            var testString = "123,asdf";
            var expectedResult = 123;

            var result = calculator.ParseStringAndCalculate(testString);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseStringAndCalculate_1String_ReturnsZero()
        {
            var testString = "qwerty";
            var expectedResult = 0;

            var result = calculator.ParseStringAndCalculate(testString);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseStringAndCalculate_6Numbers_Added()
        {
            var testString = "1,2,3,4,5,6";
            var expectedResult = 21;

            var result = calculator.ParseStringAndCalculate(testString);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseStringAndCalculate_3NumbersWithNewLineDelimiter_Added()
        {
            var testString = @"1\n2,3";
            var expectedResult = 6;

            var result = calculator.ParseStringAndCalculate(testString);

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ParseStringAndCalculate_3NumbersWith2Negatives_ThrowsException()
        {
            var testString = @"11,-11,-22";

            var exception = Assert.Throws<Exception>(() => calculator.ParseStringAndCalculate(testString));

            Assert.True(exception.Data.Contains("NegativesEntered"));
            Assert.Equal("-11,-22", exception.Data["NegativesEntered"]);
        }

        [Fact]
        public void ParseStringAndCalculate_3Numbers_Over1000Invalid()
        {
            var testString = @"1234,11,99999";
            var expectedResult = 11;

            var result = calculator.ParseStringAndCalculate(testString);

            Assert.Equal(expectedResult, result);
        }
    }
}
