using FitnessClub.DAL;
using FitnessClub.DAL.FitnessClubDataBase;
using FitnessClub.DAL.FitnessClubDataBase.FakeData;
using FitnessClub.Desktop.UI.Pages;
using FitnessClub.Desktop.UI.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace FitnessClub.Desktop;

public partial class App : Application
{
    public static IHost AppHost { get; private set; } = null!;

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddTransient<CreateRequestWindow>();
                services.AddTransient<AuthorizationPage>();
                services.AddTransient<RequestListPage>();
                services.AddTransient<IndividualPlanListPage>();

                services.AddDALServices();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var context = AppHost.Services.GetRequiredService<FitnessClubContext>();
        await context.Initialize();

        var startupFrom = AppHost.Services.GetRequiredService<MainWindow>();
        startupFrom.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}
