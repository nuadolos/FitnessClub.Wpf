using FitnessClub.Desktop.UI.Pages;
using FitnessClub.Desktop.UI.Utilities;
using System.Windows;

namespace FitnessClub.Desktop;

public partial class MainWindow : Window
{
    private readonly AuthorizationPage _authorizationPage;

    public MainWindow(AuthorizationPage authorizationPage)
    {
        InitializeComponent();

        _authorizationPage = authorizationPage;
        AppController.AppFrame = mainFrame;
        AppController.AppFrame.Navigate(_authorizationPage);
    }

    private void btnBack_Click(object sender, RoutedEventArgs e)
    {
        AppController.AppFrame.GoBack();
    }

    private void mainFrame_ContentRendered(object sender, System.EventArgs e)
    {
        if (AppController.AppFrame.CanGoBack)
            btnBack.Visibility = Visibility.Visible;
        else
            btnBack.Visibility = Visibility.Hidden;
    }
}
