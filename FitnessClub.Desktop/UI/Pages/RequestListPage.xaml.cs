using FitnessClub.BLL.Domain;
using FitnessClub.DAL.FitnessClubDataBase;
using FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FitnessClub.Desktop.UI.Pages;

public partial class RequestListPage : Page
{
    private readonly FitnessClubContext _fitnessClubContext;

    public RequestListPage(FitnessClubContext fitnessClubContext)
    {
        InitializeComponent();

        _fitnessClubContext = fitnessClubContext;
    }

    private async Task GetProducts()
    {
        if (loadingSpinner.IsLoading)
            return;

        loadingSpinner.IsLoading = true;
        gridLoading.Visibility = Visibility.Visible;

        var requests = _fitnessClubContext.Requests
            .Include(r => r.IndividualPlans)
            .Include(r => r.UserClient)
            .Include(r => r.UserManager)
            .Select(r => new RequestModel {
                Title = r.Title,
                Porpose = r.Porpose,
                RequestStatusCode = r.RequestStatusCode,
                RequestStatusDescription = r.RequestStatus.Description,
                IndividualPlanCount = r.IndividualPlans.Count,
                ClientFullName = r.UserClient.FullName,
                ManagerFullName = r.UserManager.FullName,
                CreatedOn = r.CreatedOn,
                ModifiedOn = r.ModifiedOn
            })
            .AsQueryable();

        if (comboBoxRequestStatus.SelectedIndex > 0)
            requests = requests
                .Where(p => p.RequestStatusCode == ((RequestStatus)comboBoxRequestStatus.SelectedItem).Code);

        if (textBoxSearch.Text != "Введите для поиска" && string.IsNullOrWhiteSpace(textBoxSearch.Text))
            requests = requests
                .Where(p => p.Title.ToLower().Contains(textBoxSearch.Text.ToLower()));

        requests = comboBoxSort.SelectedIndex switch
        {
            1 => !(bool)checkBoxDescending.IsChecked!
                ? requests.OrderBy(p => p.Title)
                : requests.OrderByDescending(p => p.Title),
            _ => !(bool)checkBoxDescending.IsChecked!
                ? requests.OrderBy(p => p.CreatedOn)
                : requests.OrderByDescending(p => p.CreatedOn),
        };

        listViewRequestList.ItemsSource = await requests.ToListAsync();
        
        loadingSpinner.IsLoading = false;
        gridLoading.Visibility = Visibility.Hidden;
    }

    private async void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (textBoxSearch.Text != "Введите для поиска" && string.IsNullOrWhiteSpace(textBoxSearch.Text))
        {
            await GetProducts();
        }
    }

    private void textBoxSearch_GotFocus(object sender, RoutedEventArgs e) =>
        textBoxSearch.Text = null;

    private void textBoxSearch_LostFocus(object sender, RoutedEventArgs e)
    {
        if (textBoxSearch.Text != null)
            textBoxSearch.Text = "Введите для поиска";
    }

    private void listViewRequestList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        //AppController.AppFrame.Navigate(new ProductCardPage(listViewProductList.SelectedItem as Product));
    }

    private async void comboBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await GetProducts();
    }

    private async void comboBoxRequestStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await GetProducts();
    }

    private async void checkBoxDescending_Unchecked(object sender, RoutedEventArgs e)
    {
        await GetProducts();
    }

    private async void checkBoxDescending_Checked(object sender, RoutedEventArgs e)
    {
        await GetProducts();
    }

    private void btnAddRequest_Click(object sender, RoutedEventArgs e)
    {
        //AppController.AppFrame.Navigate(new ProductCardPage(null));
    }

    private void listViewRequestList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private async void Page_Loaded(object sender, RoutedEventArgs e)
    {
        var requestStatutes = await _fitnessClubContext.RequestStatuses
            .ToListAsync();
        requestStatutes.Insert(0, new RequestStatus {
            Code = "0",
            Description = "Все статусы"
        });

        comboBoxRequestStatus.ItemsSource = requestStatutes;
        comboBoxRequestStatus.SelectedIndex = 0;
        comboBoxSort.SelectedIndex = 0;
    }
}
