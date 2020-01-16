using StringCalculator.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StringCalculatorTest
{
    public class CalculationManagerTests
    {
        private CalculationManager calculation;
        public CalculationManagerTests()
        {
            calculation = new CalculationManager();
        }

        [Fact]
        public void PerformCalculation_3Numbers_Added()
        {
            int[] numbers = { 1, 3, 5 };
            var expectedResult = 9;

            var result = calculation.PerformCalculation(numbers, "+");

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void PerformCalculation_3Numbers_Multiplied()
        {
            int[] numbers = { 1, 5, 10 };
            var expectedResult = 50;

            var result = calculation.PerformCalculation(numbers, "*");
            
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void PerformCalculation_3Numbers_Division()
        {
            int[] numbers = { 1000, 10, 2 };
            var expectedResult = 50;

            var result = calculation.PerformCalculation(numbers, "/");

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void PerformCalculation_3Numbers_Subtraction()
        {
            int[] numbers = { 50, 30, 2 };
            var expectedResult = 18;

            var result = calculation.PerformCalculation(numbers, "-");

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void PerformanceCalculation_3NumbersWithInvalidOpetion_Returns0()
        {
            int[] numbers = { 1, 2, 3 };
            var expectedResult = 0;

            var result = calculation.PerformCalculation(numbers, "invalid");

            Assert.Equal(expectedResult, result);
        }
    }
}
