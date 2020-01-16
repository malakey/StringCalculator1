using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculator.Managers
{
    public interface ICalculationManager
    {
        double PerformCalculation(int[] numbers, string calculationType);
    }
}
