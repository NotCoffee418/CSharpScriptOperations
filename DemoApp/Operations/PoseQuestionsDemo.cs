using CSharpScriptOperations;

namespace DemoApp.Operations;

[OperationDescription("Post Questions Demo")]
internal class PoseQuestionsDemo : IOperation
{
    public async Task RunAsync()
    {
        int intVal = UserInput.PoseIntQuestion("Input an integer");
        Console.WriteLine("Your int was parsed: " + intVal);

        double doubleVal = UserInput.PoseDoubleQuestion("Input a double");
        Console.WriteLine("Your double was parsed: " + doubleVal);

        double floatVal = UserInput.PoseFloatQuestion("Input a float");
        Console.WriteLine("Your double was parsed: " + floatVal);

        double decimalVal = UserInput.PoseFloatQuestion("Input a decimal");
        Console.WriteLine("Your decimal was parsed: " + decimalVal);
    }
}
