using FitnessClub.DAL.FitnessClubDataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.DAL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDALServices(this IServiceCollection services)
    {
        services.AddDbContext<FitnessClubContext>(options => {
            options.UseSqlServer("Server=.\\sqlexpress;Database=FitnessClub;Trusted_Connection=True;TrustServerCertificate=True;",
                sqlOptions => sqlOptions.EnableRetryOnFailure());
        });

        return services;
    }
}
