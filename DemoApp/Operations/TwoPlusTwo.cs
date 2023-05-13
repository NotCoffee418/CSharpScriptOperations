using CSharpScriptOperations;

namespace DemoApp.Operations;


[OperationDescription("Print the result of 2+2", priority: 2)]
class TwoPlusTwo : IOperation
{
    public async Task RunAsync()
    {
        int result = 2 + 2;
        Console.WriteLine($"2 + 2 = {result}");
    }
}
