namespace CSharpScriptOperations;

public class OperationDescriptionAttribute : Attribute
{
    /// <summary>
    /// Describe the operation
    /// </summary>
    /// <param name="description">Display string as it shows up in the main menu</param>
    /// <param name="priority">When auto registering, items with lower priority will be placed earlier in the list. Values with 0 or lower will be ignored.</param>
    public OperationDescriptionAttribute(string description, float priority = 0)
    {
        Description = description;
        Priority = priority;
    }

    /// <summary>
    /// Description displayed in the console
    /// </summary>
    public string Description { get; set; }
    public float Priority { get; set; }
}
