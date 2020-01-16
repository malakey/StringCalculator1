using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StringCalculator.Managers;
using System;
using System.Linq;

namespace StringCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.AddDebug();
                })
                .AddSingleton<ICalculator, Calculator>()
                .AddSingleton<IDelimiterManager, DelimiterManager>()
                .AddSingleton<ICalculationManager, CalculationManager>()
                .AddSingleton<IStringParserManager, StringParserManager>()
                .BuildServiceProvider();

            string operationType = "+";
            string alternateDelimiter = @"\n";
            bool allowNegatives = false;
            int upperBound = 1000;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].StartsWith("--operation"))
                {
                    operationType = args[i + 1];
                }
                else if (args[i].StartsWith("--alternateDelimiter"))
                {
                    alternateDelimiter = args[i + 1];
                }
                else if (args[i].StartsWith("--allowNegatives"))
                {
                    allowNegatives = true;
                }
                else if (args[i].StartsWith("--upperBound"))
                {
                    if (!int.TryParse(args[i + 1], out upperBound))
                    {
                        Console.WriteLine("UpperBound entered invalid, using 1000");
                    }
                }
            }

            var calculator = serviceProvider.GetService<ICalculator>();
            calculator.StartCalculator(operationType, alternateDelimiter, allowNegatives, upperBound);
        }
    }
}
