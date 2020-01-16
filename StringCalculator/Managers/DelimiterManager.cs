using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class DelimiterManager : IDelimiterManager
    {
        private readonly ILogger<DelimiterManager> _logger;
        private readonly Regex singleItemRegex = new Regex(@"\/\/.\\n");

        public DelimiterManager(ILogger<DelimiterManager> logger)
        {
            _logger = logger;
        }

        public (string input, string[] delimiters) GetDelimitersFromInput(string input)
        {
            var delimiters = new List<string>();
            delimiters.AddRange(new string[] { ",", @"\n" });

            if (input.Substring(0, 2).Equals("//") && singleItemRegex.IsMatch(input))
            {
                var match = singleItemRegex.Match(input);
                delimiters.Add(match.Value.Substring(2, 1));
                input = input.Substring(match.Value.Length, input.Length - match.Value.Length);
            }
            else
            {
                _logger.LogInformation($"No custom delimiters found in input - {input}");
            }

            return (input, delimiters.ToArray());
        }
    }
}
