using CSharpScriptOperations;

namespace DemoApp.Operations;


[OperationDescription("Say 'Hello World!'", priority: 1)]
class HelloWorld : IOperation
{
    public async Task RunAsync()
    {
        Console.WriteLine("Hello World!");
    }
}
