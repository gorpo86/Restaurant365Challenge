using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant365Challenge.Shared.Interfaces
{
    public interface ILog
    {
        public void Log(string message);

        public void Log(string message, Exception exception);
    }
}
