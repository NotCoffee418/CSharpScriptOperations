# CSharpScriptOperations

[![Nuget](https://img.shields.io/nuget/v/CSharpScriptOperations?style=for-the-badge "Nuget")](https://www.nuget.org/packages/CSharpScriptOperations)

This is my current approach to running blocks of code for scripting long-running operations using C#.  
This isn't really intended for anyone but myself, but it's a simple, documented and working so maybe someone can get some use out of it.  

There may be better options for you such as [.NET Core for Jupyter](https://devblogs.microsoft.com/dotnet/net-core-with-juypter-notebooks-is-here-preview-1/), [CSharp Scripting](https://github.com/dotnet/roslyn/blob/master/docs/wiki/Scripting-API-Samples.md) or [an actual scripting language](https://www.python.org/) but unfortunately my (lack of) Python skills slow me down and the other options are missing some important features as well.

### How to use it
#### 1. Install the nuget package.
Install the  [nuget package](https://www.nuget.org/packages/CSharpScriptOperations/) into a **Console Application**.  
Call `using CSharpScriptOperations` wherever you need it.

#### 2. Create operations
Operations are class objects dedicated to a specific task or set of tasks. They implement this package's `IOperation` class.  
An operation will look something like this:
```
class TwoPlusTwo : IOperation
{
    public string Description => 
        "Print the result of 2+2";

    public void Run()
    {
        int result = 2 + 2;
        Console.WriteLine($"2 + 2 = {result}");
    }
}
```
Whatever is in the `Run()` function is called when the operation is requested.  
The description is used in the console to show what the operration does.

#### 3. Register your operations
Bulk register your operations in one swoop:
```
OperationManager.RegisterOperationsBulk(
    new List<IOperation>() {
        new TwoPlusTwo(),
        new LondonWeather(),
    }
);
```

Or register operations one by one:
```
OperationManager.RegisterOperation(new HelloWorld());
```

#### 4. Start the listener
This will display our options and interpret user input to run the approperiate operation.
```
OperationManager.StartListening();
```
Alternatively you can implement your own version of `StartListening()`.  
You can access the registered operations and it's classes through the
`OperationManager.RegisteredOperations` object.

#### 5. Try it out
The output of the ExampleConsoleApp looks like this:
```
Available Operations:
0. Exit Application
1. Print the result of 2+2
2. Print the current weather in London
3. Say 'Hello World!'

Select an operation ('help' for list of operations)
```
And we can run the operations by entering the requested operation's number.
```
Select an operation ('help' for list of operations)
2

Running operation 2: Print the current weather in London...
The weather in London is currently: Light Rain
Operation complete (or running in the background)
```
The application will run until operation 0 is called.  
The `0. Exit Application` is already installed by default.

### Example

See the implementation in the [ExampleConsoleApplication](https://github.com/NotCoffee418/CSharpScriptOperations/blob/main/ExampleConsoleApp).  

### todo
- background running with startup args as an option,
- background runner within same application instance as an option
