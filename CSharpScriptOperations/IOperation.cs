using System;
using System.Threading.Tasks;

namespace CSharpScriptOperations
{
    public interface IOperation
    {
        /// <summary>
        /// Starts the operation
        /// </summary>
        Task RunAsync();
    }
}
