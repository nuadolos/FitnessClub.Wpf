using FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo;
using FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.DAL.FitnessClubDataBase.Entities.Consumers;

[Table("Users", Schema = "consumers")]
public class User : BaseEntity
{
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime BirthDay { get; set; }
    public string Gender { get; set; } = null!;

    public string UserRoleCode { get; set; } = null!;
    [ForeignKey(nameof(UserRoleCode))]
    public UserRole UserRole { get; set; } = null!;

    [InverseProperty("UserManager")]
    public ICollection<Request> ManagerRequests { get; set; } = new HashSet<Request>();

    [InverseProperty("UserClient")]
    public ICollection<Request> ClientRequests { get; set; } = new HashSet<Request>();
}
