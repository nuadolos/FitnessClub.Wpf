using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.DAL.FitnessClubDataBase;

[Index(nameof(Guid), IsUnique = true)]
public class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Guid { get; set; }

    [Column(TypeName = "datetime2(0)")]
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    [Column(TypeName = "datetime2(0)")]
    public DateTime? ModifiedOn { get; set; }

    [Timestamp]
    public byte[]? Timestamp { get; set; }
}
