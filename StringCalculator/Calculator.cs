using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringCalculator
{
    public class Calculator : ICalculator
    {
        private readonly ILogger<Calculator> _logger;

        public Calculator(ILogger<Calculator> logger)
        {
            _logger = logger;
        }

        public void StartCalculator()
        {
            var input = Console.ReadLine();

            var result = ParseStringAndCalculate(input);

            Console.WriteLine(result);
            
        }

        public int ParseStringAndCalculate(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

            var splitInput = input.Split(',');

            int result = 0;
            StringBuilder formula = new StringBuilder();

            foreach (var split in splitInput)
            {
                if (formula.Length != 0)
                {
                    formula.Append("+");
                }

                if (int.TryParse(split, out int res))
                {
                    result += res;
                    formula.Append(res);
                }
                else
                {
                    formula.Append("0");
                }
            }

            formula.Append($" = {result}");

            Console.WriteLine(formula.ToString());

            return result;
        }
    }
}
