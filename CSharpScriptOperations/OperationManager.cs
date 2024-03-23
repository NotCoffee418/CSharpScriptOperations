namespace CSharpScriptOperations;

public static class OperationManager
{
    /// <summary>
    /// Static constructor registers InternalOperations
    /// </summary>
    static OperationManager()
    {
        RegisterOperation(typeof(ExitApplication));
    }

    private static Dictionary<int, Type> _registeredOperations
        = new Dictionary<int, Type>();

    /// <summary>
    /// Use to register additional dependencies
    /// </summary>
    public static IServiceCollection Services = new ServiceCollection();

    internal static IServiceProvider EffectiveServiceProvider = null;


    /// <summary>
    /// Read-only access to RegisteredOperations.
    /// Dictionary: InputNumber, Operation
    /// </summary>
    public static ReadOnlyDictionary<int, Type> RegisteredOperations
    {
        get { return new ReadOnlyDictionary<int, Type>(_registeredOperations); }
    }

    /// <summary>
    /// Registers a new operation to the application
    /// </summary>
    /// <param name="operation">Instance of an IOperation</param>
    /// <param name="overrideIndex">Manually overrides index. Leave 0 for auto-assign (recommended).</param>
    public static void RegisterOperation(Type operation, int overrideIndex = 0)
    {
        // Determine new index;
        int newIndex = _registeredOperations.Count;
        if (overrideIndex != 0)
            newIndex = overrideIndex;

        // Validate index
        if (_registeredOperations.ContainsKey(newIndex))
            throw new ArgumentException($"RegisterOperation: The provided overrideIndex {overrideIndex} is already assigned.");

        // Register the operation
        _registeredOperations.Add(newIndex, operation);

        // Register as dependency
        Services.AddTransient(operation);
    }

    /// <summary>
    /// Function that registers a list of operations for less verbose.
    /// </summary>
    /// <param name="operations">List of IOperation instances</param>
    public static void RegisterOperationsBulk(List<Type> operations)
        => operations.ForEach(op => RegisterOperation(op));

    /// <summary>
    /// Automatically registers any operations found in the assembly.
    /// </summary>
    public static void AutoRegisterOperations()
    {
        // Get all applicable types
        List<Type> operationTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .GroupBy(x => x.FullName)
            .Select(g => g.First())
            .Where(t => !t.IsInterface && !t.IsAbstract && t.GetInterfaces().Any(t => t.FullName == typeof(IOperation).FullName))
            // Don't include any internal operations
            .Where(a => !a.FullName.StartsWith(nameof(CSharpScriptOperations)))
            .OrderBy(x => x.GetCustomAttribute<OperationDescriptionAttribute>()?.Priority ?? 0)
            .ToList();
        RegisterOperationsBulk(operationTypes);
    }

    /// <summary>
    /// Starts the listener loop which displays the registered operations,
    /// requests user input, and runs the requested operation.
    /// This will keep looping until the Exit Application operation is called.
    /// 
    /// Register any dependencies before calling this function.
    /// </summary>
    /// <param name="serviceProvider">The IServiceProvider to use for resolving dependencies.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public static async Task StartListeningAsync(IServiceProvider serviceProvider = null)
    {
        // Use the provided serviceProvider, or fallback to the Services property.
        EffectiveServiceProvider = serviceProvider
            ?? Services.BuildServiceProvider();

        // List all operations
        Console.Write(GetOperationsDisplay());

        // Request user
        while (true) // Breakout is calling Exit operation
        {
            Console.WriteLine(); // empty
            Console.WriteLine("Select an operation ('help' for list of operations)");
            string userInput = Console.ReadLine();

            // Handle "help"
            if (userInput.ToLower() == "help")
            {
                Console.Write(GetOperationsDisplay());
                continue;
            }

            // Parse input, report and repeat on invalid
            int reqOperationId = -1;
            if (!int.TryParse(userInput, out reqOperationId))
            {
                Console.WriteLine("Input must be the numeric identifier of a registered operation. Try again.");
                continue;
            }

            // Handle invalid operation id
            if (!OperationIdExists(reqOperationId))
            {
                Console.WriteLine("Invalid operation number. Try again.");
                continue;
            }

            // Create instance
            Type operationType = GetOperationTypeById(reqOperationId);
            IOperation operationInstance = (IOperation)EffectiveServiceProvider.GetService(operationType);

            // Valid registered operation, report and run it run it.
            Console.WriteLine();
            Console.WriteLine($"Running operation {reqOperationId}...");
            await operationInstance.RunAsync();
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Gets a string listing registered operations by their description and activation key
    /// </summary>
    /// <returns></returns>
    public static string GetOperationsDisplay()
    {
        // Handle incorrect usage
        if (EffectiveServiceProvider is null)
            return "Cannot call GetOperationsDisplay() manually before calling StartListeningAsync(), which will print it automatically.";

        /// Display operations
        string result = "Available Operations: " + Environment.NewLine;
        foreach (var opKvp in _registeredOperations)
        {
            /// Find the description
            // Default
            string description = "Add an [OperationDescription(\"goes here\")] attribute to the operation class.";
            // Try via description attribute
            OperationDescriptionAttribute descAttr = opKvp.Value.GetCustomAttributes(typeof(OperationDescriptionAttribute), false)
                .FirstOrDefault() as OperationDescriptionAttribute;
            if (descAttr is not null) // via attribute
                description = descAttr.Description;
            // LEGACY: Try via Description property
            else if (opKvp.Value.GetProperty("Description") is not null)
            {
                // Create an instance and extract the description
                IOperation operationInstance = null;
                try
                {
                    operationInstance = (IOperation)EffectiveServiceProvider.GetService(opKvp.Value);
                    description = opKvp.Value.GetProperty("Description").GetValue(operationInstance) as string;
                }
                catch {/* Use default description */}
            }
            result += $"{opKvp.Key}. {description}{Environment.NewLine}";
        }
        return result;
    }

    /// <summary>
    /// Check if an ID had a registered operation
    /// </summary>
    /// <param name="operationId"></param>
    /// <returns></returns>
    public static bool OperationIdExists(int operationId)
        => _registeredOperations.ContainsKey(operationId);


    /// <summary>
    /// Get the type of an operation by it's ID
    /// </summary>
    /// <param name="operationId"></param>
    /// <returns></returns>
    public static Type GetOperationTypeById(int operationId)
        => OperationIdExists(operationId) ?
        _registeredOperations[operationId] :
        throw new ArgumentException("GetOperationTypeById failed because the input id has no registered operation");

}
