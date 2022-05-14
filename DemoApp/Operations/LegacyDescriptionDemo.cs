using CSharpScriptOperations;

namespace DemoApp.Operations;

internal class LegacyDescriptionDemo : IOperation
{
    [Obsolete("Please see other operations for the modern implementation")]
    public string Description => "Legacy Description'";

    public async Task RunAsync()
    {
        Console.WriteLine("Prior to v1.3.0, descriptions would be defined with a the Description property in each class.",
            "This is no longer the recommended approach, but it is still supported for projects made using older versions.");
    }
}
