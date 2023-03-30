using FitnessClub.BLL.Services;
using FitnessClub.DAL.FitnessClubDataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Controls;

namespace FitnessClub.Desktop.UI.Pages;

public partial class IndividualPlanListPage : Page
{
    private readonly FitnessClubContext _fitnessClubContext;

    public Guid? RequestGuid { get; set; }

    public IndividualPlanListPage(FitnessClubContext fitnessClubContext)
    {
        InitializeComponent();
        _fitnessClubContext = fitnessClubContext;
    }

    private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        if (!RequestGuid.HasValue)
        {
            NotificationService.NotifyError("Индивидуальные планы", "Отсутствует идентификатор заявки!");
            return;
        }

        dataGrid.ItemsSource = await _fitnessClubContext.IndividualPlans
            .Include(ip => ip.Workouts)
                .ThenInclude(w => w.Exercises)
            .Where(ip => ip.RequestGuid == RequestGuid.Value)
            .ToListAsync();
    }
}
