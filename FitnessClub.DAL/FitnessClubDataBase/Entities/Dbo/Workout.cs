using FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo;

[Table("Workouts")]
public class Workout : BaseEntity
{
    public byte NumberOfRepetitions { get; set; }
    public byte QuantityPerWeek { get; set; }

    public DateTime AssignedOn { get; set; }
    public bool IsDone { get; set; }
    public byte? Pulse { get; set; }

    public Guid IndividualPlanGuid { get; set; }
    [ForeignKey(nameof(IndividualPlanGuid))]
    public IndividualPlan IndividualPlan { get; set; } = null!;

    [InverseProperty("Workouts")]
    public ICollection<Exercise> Exercises { get; set; } = new HashSet<Exercise>();
}
