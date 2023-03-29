using FitnessClub.DAL.FitnessClubDataBase.Entities.Consumers;
using FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo;
using FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.DAL.FitnessClubDataBase;

public class FitnessClubContext : DbContext
{
    public FitnessClubContext(DbContextOptions<FitnessClubContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<IndividualPlan> IndividualPlans { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<RequestStatus> RequestStatuses { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasMany(e => e.ClientRequests)
                .WithOne(e => e.UserClient)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasMany(e => e.ManagerRequests)
                .WithOne(e => e.UserManager)
                .OnDelete(DeleteBehavior.NoAction);
        });

        base.OnModelCreating(modelBuilder);
    }
}
