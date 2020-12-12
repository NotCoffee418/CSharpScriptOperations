using System;

namespace CSharpScriptOperations
{
    public interface IOperation
    {
        /// <summary>
        /// Description displayed in the console
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Starts the operation
        /// </summary>
        void Run();
    }
}
