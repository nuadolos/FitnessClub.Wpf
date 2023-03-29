using FitnessClub.BLL.Services;
using FitnessClub.DAL.FitnessClubDataBase;
using FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo;
using FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries;
using FitnessClub.Desktop.UI.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FitnessClub.Desktop.UI.Pages
{
    /// <summary>
    /// Логика взаимодействия для SendRequestPage.xaml
    /// </summary>
    public partial class SendRequestPage : Page
    {
        private readonly FitnessClubContext _fitnessClubContext;
        public Request? Request { get; set; }

        public SendRequestPage(FitnessClubContext fitnessClubContext)
        {
            InitializeComponent();
            _fitnessClubContext = fitnessClubContext;
        }

        private async void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            var error = new StringBuilder();
            if (string.IsNullOrEmpty(Request!.Title))
                error.AppendLine("Укажите наименование");

            if (string.IsNullOrWhiteSpace(Request.Porpose))
                error.AppendLine("Укажите цель");

            if (Request.UserManager == null)
                error.AppendLine("Выберите тренера");

            if (Request.UserClient == null)
                error.AppendLine("Выберите клиента");

            if (error.Length > 0)
            {
                NotificationService.NotifyError("Сохранение заявки", $"Данные не соотвествуют следующим критериям:\n{error}");
                return;
            }

            if (Request.Guid == Guid.Empty)
                await _fitnessClubContext.Requests.AddAsync(Request);

            await _fitnessClubContext.SaveChangesAsync();

            NotificationService.NotifyInfo("Сохранение продукта", $"Продукт успешно сохранен");
            AppController.AppFrame.GoBack();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var users = await _fitnessClubContext.Users
                .ToListAsync();

            var clients = users
                .Where(u => u.UserRoleCode == UserRole.CLIENT);
            comboBoxClient.ItemsSource = clients;

            var trainers = users
                .Where(u => u.UserRoleCode == UserRole.TRAINER);
            comboBoxManager.ItemsSource = trainers;

            _ = AppController.CurrentUser!.UserRoleCode switch
            {
                UserRole.CLIENT => comboBoxClient.SelectedItem = AppController.CurrentUser,
                UserRole.TRAINER => comboBoxManager.SelectedItem = AppController.CurrentUser,
                _ => throw new Exception()
            };

            Request ??= new Request();
        }
    }
}
