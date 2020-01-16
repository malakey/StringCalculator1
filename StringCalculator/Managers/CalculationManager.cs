using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringCalculator.Managers
{
    public class CalculationManager : ICalculationManager
    {
        public CalculationManager()
        {
        }

        public double PerformCalculation(int[] numbers, string calculationType)
        {
            switch (calculationType)
            {
                case "+":
                    return numbers.Sum();

                case "-":
                    return numbers.Aggregate((a, b) => a - b);

                case "*":
                    return numbers.Aggregate(1, (a, b) => a * b);

                case "/":
                    return numbers.Aggregate((a, b) => a / b);

                default:
                    return 0;
            }
        }
    }
}
