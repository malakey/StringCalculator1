using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculator
{
    public interface ICalculator
    {
        void StartCalculator();

        int ParseStringAndCalculate(string input);
    }
}
