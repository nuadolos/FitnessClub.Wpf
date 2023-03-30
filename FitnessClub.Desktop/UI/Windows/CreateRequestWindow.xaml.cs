using FitnessClub.BLL.Services;
using FitnessClub.DAL.FitnessClubDataBase;
using FitnessClub.Desktop.UI.Utilities;
using System.Text;
using System;
using System.Windows;
using FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo;
using FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FitnessClub.DAL.FitnessClubDataBase.Entities.Consumers;
using System.Collections.Generic;

namespace FitnessClub.Desktop.UI.Windows;

public partial class CreateRequestWindow : Window
{
    private readonly FitnessClubContext _fitnessClubContext;
    private readonly Request _request;

    public CreateRequestWindow(FitnessClubContext fitnessClubContext)
    {
        InitializeComponent();
        _fitnessClubContext = fitnessClubContext;
        _request = new Request();
        DataContext = _request;
    }

    private async void btnCreateRequest_Click(object sender, RoutedEventArgs e)
    {
        var error = new StringBuilder();
        if (string.IsNullOrEmpty(_request.Title))
            error.AppendLine("Укажите наименование");

        if (string.IsNullOrWhiteSpace(_request.Porpose))
            error.AppendLine("Укажите цель");

        if (_request.UserManager == null)
            error.AppendLine("Выберите тренера");

        if (_request.UserClient == null)
            error.AppendLine("Выберите клиента");

        if (error.Length > 0)
        {
            NotificationService.NotifyError("Сохранение заявки",
                $"Данные не соотвествуют следующим критериям:\n{error}");
            return;
        }

        _request.RequestStatusCode = RequestStatus.CREATED;

        await _fitnessClubContext.Requests.AddAsync(_request);

        try
        {
            await _fitnessClubContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            NotificationService.NotifyError("Сохранение заявки", ex.Message);
        }

        NotificationService.NotifyInfo("Сохранение заявки", "Продукт успешно сохранен");

        DialogResult = true;
        Close();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        if (AppController.CurrentUser!.UserRoleCode == UserRole.TRAINER)
        {
            var clients = await _fitnessClubContext.Users
                .Where(u => u.UserRoleCode == UserRole.CLIENT)
                .ToListAsync();
            comboBoxClient.ItemsSource = clients;

            comboBoxManager.ItemsSource = new List<User> { AppController.CurrentUser };
            comboBoxManager.SelectedIndex = 0;
            comboBoxManager.IsEnabled = false;
        }

        if (AppController.CurrentUser!.UserRoleCode == UserRole.CLIENT)
        {
            var trainers = await _fitnessClubContext.Users
                .Where(u => u.UserRoleCode == UserRole.TRAINER)
                .ToListAsync();
            comboBoxManager.ItemsSource = trainers;

            comboBoxClient.ItemsSource = new List<User> { AppController.CurrentUser };
            comboBoxClient.SelectedIndex = 0;
            comboBoxClient.IsEnabled = false;
        }
    }
}
