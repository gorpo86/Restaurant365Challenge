using Restaurant365Challenge.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant365Challenge.Shared
{
    public class Log : ILog
    {
        void ILog.Log(string message)
        {
           //TODO: Implement a true logger
           Console.WriteLine(message);
        }

        void ILog.Log(string message, Exception exception)
        {
            //TODO: Implement a true logger
            Console.WriteLine($"{message} : EXCEPTION MESSAGE  {exception.Message}");
        }
    }
}
