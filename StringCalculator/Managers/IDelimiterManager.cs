using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculator
{
    public interface IDelimiterManager
    {
        (string input, string[] delimiters) GetDelimitersFromInput(string input);
    }
}
