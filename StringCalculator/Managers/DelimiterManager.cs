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
        private readonly Regex singleCharRegex = new Regex(@"\/\/.\\n");
        private readonly Regex multiCharRegex = new Regex(@"\/\/(\[.*\])*\\n");
        private readonly Regex multiDelimiterRegex = new Regex(@"\[(.*?)\]");

        public DelimiterManager(ILogger<DelimiterManager> logger)
        {
            _logger = logger;
        }

        public (string input, string[] delimiters) GetDelimitersFromInput(string input)
        {
            var delimiters = new List<string>();
            delimiters.AddRange(new string[] { ",", @"\n" });

            if (input.Substring(0, 2).Equals("//"))
            {
                if (singleCharRegex.IsMatch(input))
                {
                    var match = singleCharRegex.Match(input);
                    delimiters.Add(match.Value.Substring(2, 1));
                    input = input.Substring(match.Value.Length, input.Length - match.Value.Length);
                }
                else if (multiCharRegex.IsMatch(input))
                {
                    var match = multiCharRegex.Match(input);

                    var multiDelimiterMatch = multiDelimiterRegex.Matches(match.Value);

                    foreach(Match delimiter in multiDelimiterMatch)
                    {
                        delimiters.Add(delimiter.Value.Substring(1, delimiter.Value.Length - 2));
                    }

                    input = input.Substring(match.Value.Length, input.Length - match.Value.Length);
                }
                else
                {
                    _logger.LogInformation($"No custom delimiters found in input - {input}");
                }
            }

            return (input, delimiters.ToArray());
        }
    }
}
