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
        public void ParseStringAndCalculate_3Numbers_ThrowsException()
        {
            var testString = "1,2,3";

            Assert.Throws<InvalidOperationException>(() => calculator.ParseStringAndCalculate(testString));
        }
    }
}
