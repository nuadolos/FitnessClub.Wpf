using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo;

[Table("IndividualPlans")]
public class IndividualPlan : BaseEntity
{
    public DateTime StartedOn { get; set; }
    public DateTime EndedOn { get; set; }

    [Column(TypeName = "money")]
    public decimal Cost { get; set; }

    public Guid RequestGuid { get; set; }
    [ForeignKey(nameof(RequestGuid))]
    public Request Request { get; set; } = null!;

    [InverseProperty(nameof(IndividualPlan))]
    public ICollection<Workout> Workouts { get; set; } = new HashSet<Workout>();
}
