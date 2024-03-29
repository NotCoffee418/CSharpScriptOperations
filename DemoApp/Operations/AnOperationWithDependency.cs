﻿using CSharpScriptOperations;
using DemoApp.Logic;

namespace DemoApp.Operations;


[OperationDescription("Multiply with Autofac dependency injection")]
class AnOperationWithDependency : IOperation
{
    private IExampleDependency _exampleDependency;

    public AnOperationWithDependency(IExampleDependency exampleDependency)
    {
        _exampleDependency = exampleDependency;
    }

    public async Task RunAsync()
    {
        Console.WriteLine("Multiplying 2 and 3 through IExampleDependency.");

        double result = _exampleDependency.Multiply(2, 3);
        Console.WriteLine($"Result: {result}");
    }
}
