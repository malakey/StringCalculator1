using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

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
                .BuildServiceProvider();

            var calculator = serviceProvider.GetService<ICalculator>();
            calculator.StartCalculator();
        }
    }
}
