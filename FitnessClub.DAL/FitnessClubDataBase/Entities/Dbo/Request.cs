using FitnessClub.DAL.FitnessClubDataBase.Entities.Consumers;
using FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo;

[Table("Requests")]
public class Request : BaseEntity
{
    public string Title { get; set; } = null!;
    public string Porpose { get; set; } = null!;

    public string RequestStatusCode { get; set; } = null!;
    [ForeignKey(nameof(RequestStatusCode))]
    public RequestStatus RequestStatus { get; set; } = null!;

    public Guid UserManagerGuid { get; set; }
    [ForeignKey(nameof(UserManagerGuid))]
    public User UserManager { get; set; } = null!;

    public Guid UserClientGuid { get; set; }
    [ForeignKey(nameof(UserClientGuid))]
    public User UserClient { get; set; } = null!;

    [InverseProperty(nameof(Request))]
    public ICollection<IndividualPlan> IndividualPlans { get; set; } = new HashSet<IndividualPlan>();
}
