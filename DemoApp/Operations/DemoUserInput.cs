using CSharpScriptOperations;

namespace DemoApp.Operations;


[OperationDescription("Demo UserInput", priority: 3)]
class DemoUserInput : IOperation
{
    public async Task RunAsync()
    {
        bool input = UserInput.PoseBoolQuestion("This is a sample question?");
        Console.WriteLine($"User input: {input}");
    }
}
