﻿using FitnessClub.BLL.Domain;
using FitnessClub.DAL.FitnessClubDataBase;
using FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries;
using FitnessClub.Desktop.UI.Utilities;
using FitnessClub.Desktop.UI.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FitnessClub.Desktop.UI.Pages;

public partial class RequestListPage : Page
{
    private readonly FitnessClubContext _fitnessClubContext;
    private readonly IndividualPlanListPage _individualPlanListPage;
    private readonly IServiceProvider _serviceProvider;

    public RequestListPage(FitnessClubContext fitnessClubContext,
        IServiceProvider serviceProvider,
        IndividualPlanListPage individualPlanListPage)
    {
        InitializeComponent();

        _fitnessClubContext = fitnessClubContext;
        _serviceProvider = serviceProvider;
        _individualPlanListPage = individualPlanListPage;
    }

    private async Task GetRequestsAsync()
    {
        if (loadingSpinner.IsLoading)
            return;

        loadingSpinner.IsLoading = true;
        gridLoading.Visibility = Visibility.Visible;

        var userGuid = AppController.CurrentUser!.Guid;

        var requests = _fitnessClubContext.Requests
            .Include(r => r.IndividualPlans)
            .Include(r => r.UserClient)
            .Include(r => r.UserManager)
            .Where(r => r.UserClientGuid == userGuid
                || r.UserManagerGuid == userGuid)
            .Select(r => new RequestModel {
                RequestGuid = r.Guid,
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

        if (textBoxSearch.Text != "Введите для поиска" && !string.IsNullOrEmpty(textBoxSearch.Text))
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
        if (textBoxSearch.Text != "Введите для поиска")
        {
            await GetRequestsAsync();
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
        _individualPlanListPage.RequestGuid = (listViewRequestList.SelectedItem as RequestModel)?.RequestGuid;
        AppController.AppFrame.Navigate(_individualPlanListPage);
    }

    private async void comboBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await GetRequestsAsync();
    }

    private async void comboBoxRequestStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await GetRequestsAsync();
    }

    private async void checkBoxDescending_Unchecked(object sender, RoutedEventArgs e)
    {
        await GetRequestsAsync();
    }

    private async void checkBoxDescending_Checked(object sender, RoutedEventArgs e)
    {
        await GetRequestsAsync();
    }

    private async void btnAddRequest_Click(object sender, RoutedEventArgs e)
    {
        var dialogResult = _serviceProvider.GetRequiredService<CreateRequestWindow>().ShowDialog();

        if (dialogResult == true)
            await GetRequestsAsync();
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
