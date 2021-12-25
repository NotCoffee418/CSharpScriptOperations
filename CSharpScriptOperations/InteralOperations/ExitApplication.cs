using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CSharpScriptOperations.InteralOperations
{
    /// <summary>
    /// Shuts down the application. This is always bound to 0
    /// </summary>
    class ExitApplication : IOperation
    {
        public string Description => 
            "Exit Application";

        public async Task RunAsync()
            => Environment.Exit(0);
    }
}
