using Microsoft.Extensions.Logging;
using Moq;
using StringCalculator;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StringCalculatorTest
{
    public class DelimiterManagerTests
    {
        private readonly DelimiterManager _delimiterManager;
        private List<string> expectedDelimiters;

        public DelimiterManagerTests()
        {
            var loggerMock = new Mock<ILogger<DelimiterManager>>();
            _delimiterManager = new DelimiterManager(loggerMock.Object);

            expectedDelimiters = new List<string>() { ",", @"\n" };
        }

        [Fact]
        public void GetDelimitersFromInput_NoCustom_ReturnsDefault()
        {
            var input = "1,1,1";
            
            (var newInput, var delimiters) = _delimiterManager.GetDelimitersFromInput(input);

            Assert.Equal(input, newInput);
            Assert.Equal(expectedDelimiters.ToArray(), delimiters);
        }

        [Fact]
        public void GetDelimitersFromInput_WithCustomSingleChar_ReturnsDefaultAndCustom()
        {
            var input = @"//p\nasdf1,fdf,1,2,2";
            expectedDelimiters.Add("p");
            var expectedNewInput = "asdf1,fdf,1,2,2";

            (var newInput, var delimiters) = _delimiterManager.GetDelimitersFromInput(input);

            Assert.Equal(expectedNewInput, newInput);
            Assert.Equal(expectedDelimiters.ToArray(), delimiters);
        }

        [Fact]
        public void GetDelimitersFromInput_WithCustomMultiChar_ResultDefaultAndCustom()
        {
            var input = @"//[asdf]\n1,12,1,12,12asdf1";
            expectedDelimiters.Add("asdf");
            var expectedNewInput = "1,12,1,12,12asdf1";

            (var newInput, var delimiters) = _delimiterManager.GetDelimitersFromInput(input);

            Assert.Equal(expectedNewInput, newInput);
            Assert.Equal(expectedDelimiters.ToArray(), delimiters);
        }

        [Fact]
        public void GetDelimitersFromInput_WithMultiCustom_ReturnsDefaultAndCustom()
        {
            var input = @"//[asdf][zxcv][qwer]\n123,345,567asdf678";
            expectedDelimiters.AddRange(new string[] { "asdf", "zxcv", "qwer" });
            var expectedNewInput = "123,345,567asdf678";

            (var newInput, var delimiters) = _delimiterManager.GetDelimitersFromInput(input);

            Assert.Equal(expectedNewInput, newInput);
            Assert.Equal(expectedDelimiters.ToArray(), delimiters);
        }
    }
}
