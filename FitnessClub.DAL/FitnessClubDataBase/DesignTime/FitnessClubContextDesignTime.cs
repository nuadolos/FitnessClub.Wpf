using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FitnessClub.DAL.FitnessClubDataBase.DesignTime;

public class FitnessClubContextDesignTime : IDesignTimeDbContextFactory<FitnessClubContext>
{
    public FitnessClubContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FitnessClubContext>();
        optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=FitnessClub;Trusted_Connection=True;TrustServerCertificate=True;");
        return new FitnessClubContext(optionsBuilder.Options);
    }
}
