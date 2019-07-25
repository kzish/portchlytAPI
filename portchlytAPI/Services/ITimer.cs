using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public interface ITimer
    {
        void RunTask(object state);
    }
}
