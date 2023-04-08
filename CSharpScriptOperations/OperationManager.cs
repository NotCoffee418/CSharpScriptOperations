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
    public static ContainerBuilder ContainerBuilder = new ContainerBuilder();

    /// <summary>
    /// Built container for internal access
    /// </summary>
    internal static Autofac.IContainer Container = null;

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
        ContainerBuilder.RegisterType(operation);
    }

    /// <summary>
    /// Function that registers a list of operations for less verbose.
    /// </summary>
    /// <param name="operations">List of IOperation instances</param>
    public static void RegisterOperationsBulk(List<Type> operations)
        => operations.ForEach(op => RegisterOperation(op));

    /// <summary>
    /// Starts the listener loop which displays the registered operations,
    /// requests user input and runs the requested operation.
    /// This will keep looping until the Exit Application operation is called.
    /// 
    /// Register any dependencies before calling this function.
    /// </summary>
    /// <returns></returns>
    public static async Task StartListeningAsync()
    {
        // Register application dependencies
        ContainerBuilder.RegisterType<Application>();

        // Prevents IServiceProvider related issues
        // See: https://stackoverflow.com/questions/61779868/injecting-iserviceprovider-into-factory-class-with-autofac
        ContainerBuilder.Populate(Enumerable.Empty<ServiceDescriptor>());

        // Create container with all dependencies
        Container = ContainerBuilder.Build();

        // Start listening in container
        await Container.Resolve<Application>().RunAsync();
    }

    /// <summary>
    /// Gets a string listing registered operations by their description and activation key
    /// </summary>
    /// <returns></returns>
    public static string GetOperationsDisplay()
    {
        // Handle incorrect usage
        if (Container is null)
            return "Cannot call GetOperationsDisplay() manually before calling StartListeningAsync(), which will print it automatically.";

        /// Display operations
        string result = "Available Operations: " + Environment.NewLine;
        foreach (var opKvp in _registeredOperations)
        {
            

            /// Find the description
            // Default
            string description = "Add an [OperationDescription(\"goes here\")] attribute to the operation class.";
            // Try via description attribute
            OperationDescriptionAttribute? descAttr = opKvp.Value.GetCustomAttributes(typeof(OperationDescriptionAttribute), false)
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
                    operationInstance = (IOperation)Container.Resolve(opKvp.Value);
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
