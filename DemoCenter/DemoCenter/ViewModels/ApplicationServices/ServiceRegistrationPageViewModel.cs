using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;

using Eremex.AvaloniaUI.Controls.ApplicationServices;

namespace DemoCenter.ViewModels.ApplicationServices;

/// <summary>
/// How the services are wired up. The library describes what to register; the host decides
/// with which container and with which lifetime.
/// </summary>
public partial class ServiceRegistrationPageViewModel : ApplicationServicesPageViewModelBase
{
    public ServiceRegistrationPageViewModel()
    {
        Registrations = new ObservableCollection<ServiceRegistrationInfo>(DescribeRegistrations());
    }

    /// <summary>
    /// The services that RegisterApplicationServices hands to the host, in registration order.
    /// </summary>
    public ObservableCollection<ServiceRegistrationInfo> Registrations { get; }

    /// <summary>
    /// Microsoft.Extensions.DependencyInjection integration, as a one-liner.
    /// </summary>
    public string MicrosoftDiSnippet =>
        """
        ApplicationServicesContext.RegisterApplicationServices(
            (type, factory) => services.AddSingleton(type, sp => factory(sp)));
        """;

    /// <summary>
    /// Autofac integration, as a one-liner.
    /// </summary>
    public string AutofacSnippet =>
        """
        ApplicationServicesContext.RegisterApplicationServices(
            (type, factory) => builder
                .Register(c => factory(c.Resolve<IServiceProvider>()))
                .As(type)
                .SingleInstance());
        """;

    /// <summary>
    /// No container at all: the built-in provider, which is what this demo uses.
    /// </summary>
    public string NoContainerSnippet =>
        """
        var serviceProvider = new SimpleServiceProvider();
        ApplicationServicesContext.RegisterApplicationServices(serviceProvider.AddSingleton);
        ApplicationServicesContext.SetCurrent(serviceProvider);
        """;

    /// <summary>
    /// Constructor injection is the normal way to consume the services.
    /// </summary>
    public string InjectionSnippet =>
        """
        public class DocumentViewModel
        {
            private readonly IDialogService dialogService;

            public DocumentViewModel(IDialogService dialogService)
                => this.dialogService = dialogService;
        }
        """;

    /// <summary>
    /// Resolves every registered service to show that the current setup is complete.
    /// </summary>
    [RelayCommand]
    private void CheckRegistrations()
    {
        foreach (var registration in Registrations)
        {
            var instance = ApplicationServicesContext.Current?.GetService(registration.ServiceType);
            registration.Resolved = instance is not null;
            registration.ImplementationName = instance?.GetType().Name ?? "not registered";
        }

        var resolved = Registrations.Count(r => r.Resolved);
        Report("Check registrations", $"{resolved} of {Registrations.Count} services resolved");
    }

    /// <summary>
    /// Asks the library which services exist without instantiating any of them:
    /// the host receives service type / factory pairs, not ready instances.
    /// </summary>
    private static IEnumerable<ServiceRegistrationInfo> DescribeRegistrations()
    {
        var types = new List<Type>();
        ApplicationServicesContext.RegisterApplicationServices((type, _) => types.Add(type));
        return types.Select(type => new ServiceRegistrationInfo(type, DescribeService(type)));
    }

    private static string DescribeService(Type serviceType) => serviceType.Name switch
    {
        nameof(IWindowsManager) => "Tracks the active window; every other service falls back to it.",
        nameof(IMessageBoxService) => "Standard message boxes with a fixed set of buttons.",
        nameof(IWindowService) => "Non-modal windows driven by a view model.",
        nameof(IOpenFileDialogService) => "Open file dialog, synchronous and asynchronous.",
        nameof(ISaveFileDialogService) => "Save file dialog, synchronous and asynchronous.",
        nameof(IDialogService) => "Modal dialogs driven by a view model.",
        nameof(IChoiceDialogService) => "Dialogs whose buttons are defined by application code.",
        _ => string.Empty,
    };
}

/// <summary>
/// A single row in the registration table.
/// </summary>
public partial class ServiceRegistrationInfo : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{
    private bool resolved;
    private string implementationName = "not checked";

    public ServiceRegistrationInfo(Type serviceType, string description)
    {
        ServiceType = serviceType;
        Description = description;
    }

    public Type ServiceType { get; }

    public string ServiceName => ServiceType.Name;

    public string Description { get; }

    public bool Resolved
    {
        get => resolved;
        set => SetProperty(ref resolved, value);
    }

    public string ImplementationName
    {
        get => implementationName;
        set => SetProperty(ref implementationName, value);
    }
}
