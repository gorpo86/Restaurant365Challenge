using Microsoft.Extensions.DependencyInjection;
using Restaurant365Challenge;
using Restaurant365Challenge.Shared.Entities;
using Restaurant365Challenge.Shared.Interfaces;
using System.Text.RegularExpressions;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Please provide an expresion to be evalulated");
            return;
        }

        var services = StartUp.ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();

        // Get Service and call method
        var service = serviceProvider.GetService<IStringEvaluator>();

        //args[0] = expression to evaluate
        //args[1] = alternate Delimiter
        //args[2] = Allow Negative Values
        //args[3] = Max Number Value
        var result = service.EvaluateStringExpression(args[0]
            //Added to ensure args have a value before we try to use them
            , args.Length >= 2 ? args[1] : string.Empty
            , args.Length >= 3 ? args[2] : string.Empty
            , args.Length >= 4 ? args[3] : string.Empty);


        Console.WriteLine(result);



    }


}

