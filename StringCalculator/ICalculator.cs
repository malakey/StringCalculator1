using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculator
{
    public interface ICalculator
    {
        void StartCalculator(string operationType, string optionalDelimiter, bool allowNegatives, int upperBound);
    }
}
