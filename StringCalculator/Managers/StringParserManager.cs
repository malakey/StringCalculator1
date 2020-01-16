using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringCalculator.Managers
{
    public class StringParserManager : IStringParserManager
    {
        private readonly ILogger<StringParserManager> _logger;
        private readonly IDelimiterManager _delimiterManager;
        public StringParserManager(ILogger<StringParserManager> logger, IDelimiterManager delimiterManager)
        {
            _logger = logger;
            _delimiterManager = delimiterManager;
        }

        public int[] ParseInputString(string input, string alternateDelimiter, bool allowNegatives, int upperBound)
        {
            if (String.IsNullOrWhiteSpace(input))
            {
                return new int[] { 0 };
            }

            (var newInput, var delimiters) = _delimiterManager.GetDelimitersFromInput(input, alternateDelimiter);
            var splitInput = newInput.Split(delimiters, StringSplitOptions.None);

            List<int> numbers = new List<int>();

            foreach (var split in splitInput)
            {
                if (int.TryParse(split, out int res))
                {
                    res = (res <= upperBound) ? res : 0;
                    numbers.Add(res);
                }
                else
                {
                    numbers.Add(0);
                }
            }

            if (!allowNegatives && numbers.Any(i => i < 0))
            {
                Exception ex = new Exception();
                ex.Data.Add("NegativesEntered", String.Join(",", numbers.Where(i => i < 0)));
                throw ex;
            }

            return numbers.ToArray();
        }
    }
}
