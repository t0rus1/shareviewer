using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShareViewer
{
    internal static class TaskMaster
    {
        internal static Stack<CancellationTokenSource>  CtsStack = new Stack<CancellationTokenSource>();

    }
}
