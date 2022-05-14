using System;
using System.Threading.Tasks;

namespace CSharpScriptOperations.InteralOperations
{
    /// <summary>
    /// Shuts down the application. This is always bound to 0
    /// </summary>
    [OperationDescription("Exit Application")]
    class ExitApplication : IOperation
    {
        public async Task RunAsync()
            => Environment.Exit(0);
    }
}
