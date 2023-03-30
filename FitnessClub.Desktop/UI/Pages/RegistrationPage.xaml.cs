using FitnessClub.BLL.Services;
using FitnessClub.DAL.FitnessClubDataBase;
using FitnessClub.DAL.FitnessClubDataBase.Entities.Consumers;
using FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries;
using FitnessClub.Desktop.UI.Utilities;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace FitnessClub.Desktop.UI.Pages;

public partial class RegistrationPage : Page
{
    private readonly FitnessClubContext _fitnessClubContext;

    public RegistrationPage(FitnessClubContext fitnessClubContext)
    {
        InitializeComponent();
        _fitnessClubContext = fitnessClubContext;
    }

    private async void btnRegister_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        var error = new StringBuilder();
        if (!Regex.IsMatch(textBoxPhone.Text, "^(\\+7|7|8)?[\\s\\-]?\\(?[489][0-9]{2}\\)?[\\s\\-]?[0-9]{3}[\\s\\-]?[0-9]{2}[\\s\\-]?[0-9]{2}$"))
            error.AppendLine("Указан некорректно номер телефона");

        if (string.IsNullOrWhiteSpace(passwordBox.Password))
            error.AppendLine("Укажите пароль");

        if (string.IsNullOrWhiteSpace(textBoxLastName.Text))
            error.AppendLine("Укажите фамилию");

        if (string.IsNullOrWhiteSpace(textBoxFirstName.Text))
            error.AppendLine("Укажите имя");

        if (string.IsNullOrWhiteSpace(textBoxMiddleName.Text))
            error.AppendLine("Укажите отчество");

        if (!DateTime.TryParse(textBoxBirthDay.Text, out var birthDay))
            error.AppendLine("Укажите дату рождения");

        if (error.Length > 0)
        {
            NotificationService.NotifyError("Регистрация",
                $"Данные не соотвествуют следующим критериям:\n{error}");
            return;
        }

        var user = new User {
            PhoneNumber = textBoxPhone.Text,
            Password = passwordBox.Password,
            FullName = $"{textBoxLastName.Text} {textBoxFirstName.Text} {textBoxMiddleName.Text}",
            BirthDay = birthDay,
            Gender = "Не определен",
            UserRoleCode = UserRole.CLIENT
        };

        await _fitnessClubContext.Users.AddAsync(user);

        try
        {
            await _fitnessClubContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            NotificationService.NotifyError("Регистрация", ex.Message);
        }

        NotificationService.NotifyInfo("Регистрация", "Вы успешно зарегистрировались!");
        
        if (AppController.AppFrame.CanGoBack)
            AppController.AppFrame.GoBack();
    }
}
