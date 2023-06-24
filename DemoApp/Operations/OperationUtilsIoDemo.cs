using CSharpScriptOperations;

namespace DemoApp.Operations;

[OperationDescription("OperationUtils.IO Demo", 6)]
internal class OperationUtilsIoDemo : IOperation
{
    public async Task RunAsync()
    {
        // Change working directory base
        // OperationUtils.IO.SetWorkingDirBase("C:", "Users", "Public", "Documents", "CSharpScriptOperations");

        // Try out GetWorkingPath()
        Console.WriteLine("Enter a relative path to get the absolute path");
        string relativePathInput = Console.ReadLine();
        string finalPath = OperationUtils.IO.GetWorkingPath(relativePathInput);
        Console.WriteLine("Working path: " + finalPath);

        // Try out EnsureValidPath()
        bool isDir = UserInput.PoseBoolQuestion("Is the path a directory?");
        bool makeMissingDir = UserInput.PoseBoolQuestion("Make missing directories?");
        bool pathExists = OperationUtils.IO.EnsureValidPath(finalPath, isDir, makeMissingDir);
        Console.WriteLine("Directory tree was prepared if needed and path " + (pathExists ? "exists." : "does not exist."));
    }
}
