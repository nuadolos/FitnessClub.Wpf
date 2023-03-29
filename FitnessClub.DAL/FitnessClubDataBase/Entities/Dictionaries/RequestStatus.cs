using FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries;

[Table("RequestStatuses", Schema = "dictionaries")]
public class RequestStatus
{
    [Key]
    [StringLength(100)]
    public string Code { get; set; } = null!;

    [StringLength(200)]
    public string Description { get; set; } = null!;

    [InverseProperty(nameof(RequestStatus))]
    public ICollection<Request> Requests { get; set; } = new HashSet<Request>();

    public const string CREATED = nameof(CREATED); 
    public const string IN_PROGRESS = nameof(IN_PROGRESS); 
    public const string REJECTED = nameof(REJECTED); 
    public const string COMPLETED = nameof(COMPLETED); 
}
