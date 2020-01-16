using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculator.Managers
{
    public interface IDelimiterManager
    {
        (string input, string[] delimiters) GetDelimitersFromInput(string input, string optionalDelimiter);
    }
}
