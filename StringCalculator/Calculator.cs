using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class Calculator : ICalculator
    {
        private readonly ILogger<Calculator> _logger;
        private readonly IDelimiterManager _delimiterManager;

        public Calculator(ILogger<Calculator> logger, IDelimiterManager delimiterManager)
        {
            _logger = logger;
            _delimiterManager = delimiterManager;
        }

        public void StartCalculator()
        {
            while (true)
            {
                var input = Console.ReadLine();

                try
                {
                    var result = ParseStringAndCalculate(input);
                    Console.WriteLine(result);
                }
                catch(Exception ex)
                {
                    if (ex.Data.Contains("NegativesEntered")) {
                        _logger.LogError($"Negatives aren't allowed. Negatives entered: {ex.Data["NegativesEntered"].ToString()}");
                    }
                    else
                    {
                        _logger.LogError(ex.Message.ToString());
                    }
                }
            }
        }

        public int ParseStringAndCalculate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

            (var newInput, var delimiters) = _delimiterManager.GetDelimitersFromInput(input);

            var splitInput = newInput.Split(delimiters.ToArray(), StringSplitOptions.None);

            int result = 0;
            StringBuilder formula = new StringBuilder();
            List<int> negatives = new List<int>();

            foreach (var split in splitInput)
            {
                if (formula.Length != 0)
                {
                    formula.Append("+");
                }

                if (int.TryParse(split, out int res))
                {
                    if (res < 0)
                    {
                        negatives.Add(res);
                    }

                    res = (res <= 1000) ? res : 0;
                    result += res;
                    formula.Append(res);
                }
                else
                {
                    formula.Append("0");
                }
            }

            if (negatives.Count > 0)
            {
                Exception ex = new Exception();
                ex.Data.Add("NegativesEntered", String.Join(",", negatives));
                throw ex;
            }

            formula.Append($" = {result}");

            Console.WriteLine(formula.ToString());

            return result;
        }
    }
}
