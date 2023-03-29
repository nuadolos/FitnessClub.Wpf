using FitnessClub.DAL.FitnessClubDataBase.Entities.Consumers;
using System.Windows.Controls;

namespace FitnessClub.Desktop.UI.Utilities;

public static class AppController
{
    public static Frame AppFrame { get; set; } = null!;
    public static User? CurrentUser { get; set; }
}
