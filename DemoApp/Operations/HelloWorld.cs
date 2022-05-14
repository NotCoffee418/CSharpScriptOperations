using CSharpScriptOperations;

namespace DemoApp.Operations;


[OperationDescription("Say 'Hello World!'")]
class HelloWorld : IOperation
{
    public async Task RunAsync()
    {
        Console.WriteLine("Hello World!");
    }
}
