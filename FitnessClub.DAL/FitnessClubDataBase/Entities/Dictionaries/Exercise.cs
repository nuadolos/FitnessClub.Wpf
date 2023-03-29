using FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries;

[Table("Exercises", Schema = "dictionaries")]
public class Exercise
{
    [Key]
    [StringLength(100)]
    public string Code { get; set; } = null!;

    [StringLength(200)]
    public string Description { get; set; } = null!;

    [InverseProperty("Exercises")]
    public ICollection<Workout> Workouts { get; set; } = new HashSet<Workout>();

    public const string SQUATTING_ON_TOES = nameof(SQUATTING_ON_TOES);
    public const string PUSH_UP = nameof(PUSH_UP);
    public const string SHOULDER_ROTATION = nameof(SHOULDER_ROTATION);
    public const string REVERSAL_OF_HANDS = nameof(REVERSAL_OF_HANDS);
    public const string HULL_SLOPES = nameof(HULL_SLOPES);
}
