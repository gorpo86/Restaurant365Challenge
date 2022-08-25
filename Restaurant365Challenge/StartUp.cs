using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant365Challenge.Shared;
using Restaurant365Challenge.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant365Challenge
{
    public static class StartUp
    {
        public static IServiceCollection ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<ILog, Log>();
            serviceCollection.AddTransient<IStringEvaluator, StringEvaluator>();

            return serviceCollection;
        }
       
    }
}
