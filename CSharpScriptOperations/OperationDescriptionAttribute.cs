using System;

namespace CSharpScriptOperations
{
    public class OperationDescriptionAttribute : Attribute
    {
        /// <summary>
        /// Describe the operation
        /// </summary>
        /// <param name="description">Display string as it shows up in the main menu</param>
        public OperationDescriptionAttribute(string description)
        {
            Description = description;
        }

        /// <summary>
        /// Description displayed in the console
        /// </summary>
        public string Description { get; set; }
    }
}
