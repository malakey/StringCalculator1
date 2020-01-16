using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculator.Managers
{
    public interface IStringParserManager
    {
        int[] ParseInputString(string input, string alternateDelimiter, bool allowNegatives, int upperBound);
    }
}
