using CSharpScriptOperations;

namespace DemoApp.Operations;


[OperationDescription("Demo UserInput")]
class DemoUserInput : IOperation
{
    public async Task RunAsync()
    {
        bool input = UserInput.PoseBoolQuestion("This is a sample question?");
        Console.WriteLine($"User input: {input}");
    }
}
