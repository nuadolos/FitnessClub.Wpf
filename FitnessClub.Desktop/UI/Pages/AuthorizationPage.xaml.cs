using FitnessClub.BLL.Services;
using FitnessClub.DAL.FitnessClubDataBase;
using FitnessClub.Desktop.UI.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace FitnessClub.Desktop.UI.Pages;

public partial class AuthorizationPage : Page
{
    private readonly FitnessClubContext _fitnessClubContext;
    private readonly RequestListPage _requestListPage;

    public AuthorizationPage(FitnessClubContext fitnessClubContext, 
        RequestListPage requestListPage)
    {
        InitializeComponent();

        _fitnessClubContext = fitnessClubContext;
        _requestListPage = requestListPage;
    }

    private async void btnEntry_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(loginBox.Text) 
            || string.IsNullOrWhiteSpace(passwordBox.Password))
        {
            NotificationService.NotifyError("Авторизация", "Заполните все пустые поля!");
            return;
        }

        var user = await _fitnessClubContext.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == loginBox.Text 
                && u.Password == passwordBox.Password);

        //if (user == null)
        //{
        //    MessageBox.Show("Пользователь не найден",
        //        "Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);
        //    return;
        //}

        //MainWindow.UserName = $"{user.UserSurname} {user.UserName} {user.UserPatronymic}";

        AppController.AppFrame.Navigate(_requestListPage);

        MessageBox.Show("Вы успешно вошли в систему",
                "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    /// <summary>
    /// Закрывает приложение
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnClose_Click(object sender, RoutedEventArgs e) =>
        Application.Current.Shutdown();
}
