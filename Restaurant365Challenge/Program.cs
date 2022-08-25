using Microsoft.Extensions.DependencyInjection;
using Restaurant365Challenge;
using Restaurant365Challenge.Shared.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        if(args.Length == 0)
        {
            Console.WriteLine("Please provide an expresion to be evalulated");
            return;
        }

        var services = StartUp.ConfigureServices();
        var serviceProvider = services.BuildServiceProvider();

        // Get Service and call method
        var service = serviceProvider.GetService<IStringEvaluator>();
        var result = service.EvaluateStringExpression(args[0]);

        Console.WriteLine(result);
    }
}

