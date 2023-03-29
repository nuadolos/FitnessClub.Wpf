using FitnessClub.DAL.FitnessClubDataBase.Entities.Consumers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries;

[Table("UserRoles", Schema = "dictionaries")]
public class UserRole
{
    [Key]
    [StringLength(100)]
    public string Code { get; set; } = null!;

    [StringLength(200)]
    public string Description { get; set; } = null!;

    [InverseProperty(nameof(UserRole))]
    public ICollection<User> Users { get; set; } = new HashSet<User>();

    public const string CLIENT = nameof(CLIENT);
    public const string TRAINER = nameof(TRAINER);
}
