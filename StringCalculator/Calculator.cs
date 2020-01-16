using Microsoft.Extensions.Logging;
using StringCalculator.Managers;
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
        private readonly IStringParserManager _stringParserManager;
        private readonly ICalculationManager _calculationManager;

        public Calculator(ILogger<Calculator> logger, IStringParserManager stringParserManager, ICalculationManager calculationManager)
        {
            _logger = logger;
            _stringParserManager = stringParserManager;
            _calculationManager = calculationManager;
        }

        public void StartCalculator(string operationType, string alternateDelimiter, bool allowNegatives, int upperBound)
        {
            while (true)
            {
                var input = Console.ReadLine();

                try
                {
                    var numbers = _stringParserManager.ParseInputString(input, alternateDelimiter, allowNegatives, upperBound);
                    var result = _calculationManager.PerformCalculation(numbers, operationType);
                    Console.WriteLine(String.Concat(String.Join(operationType, numbers), " = ", result));
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
    }
}
