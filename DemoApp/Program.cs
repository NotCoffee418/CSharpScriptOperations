using Autofac; // Import Autofac if you want DI
using Autofac.Extensions.DependencyInjection;
using CSharpScriptOperations;
using DemoApp.Logic;
using DemoApp.Operations;


/* --- REGISTER OPERATIONS --- */
// You can automatically register all operations
OperationManager.AutoRegisterOperations();

// You can also register operations one by one.
//OperationManager.RegisterOperation(typeof(HelloWorld));

// Alternatively we can register operations in the order we want them displayed
//OperationManager.RegisterOperationsBulk(
//    new List<Type>() {
//        typeof(TwoPlusTwo),
//        typeof(LondonWeather),
//        typeof(DemoUserInput),
//        typeof(AnOperationWithDependency),
//        typeof(LegacyDescriptionDemo),
//        typeof(PoseQuestionsDemo)
//    }
//);


/* --- OPTIONAL: REGISTER DEPENDENCIES --- */
// Optionally register any custom dependencies through "OperationManager.ContainerBuilder" if needed
// This example uses Autofac to register our operations. You need the following nuget dependencies:
// - Autofac
// - Autofac.Extensions.DependencyInjection
ContainerBuilder AutofacContainerBuilder = new ContainerBuilder();

// Include application dependencies
AutofacContainerBuilder
    .RegisterType<ExampleDependency>()
    .As<IExampleDependency>();

// Include the services registered by the OperationManager
AutofacContainerBuilder.Populate(OperationManager.Services);

// Build the container
var serviceProvider = new AutofacServiceProvider(AutofacContainerBuilder.Build());



/* --- START LISTENING --- */
// Start the listener loop
// This will display our options and interpret user input to run the approperiate operation
await OperationManager.StartListeningAsync(serviceProvider); // optional service provider

// Alternatively, you can implement your own approach 
// using the OperationManager.RegisteredOperations object