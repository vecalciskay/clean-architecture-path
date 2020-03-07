using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    public static class AppBuilderDatabaseExtensions
    {
        public static IApplicationBuilder EnsureDatabase(this IApplicationBuilder builder)
        {
            DbContext context = builder.ApplicationServices.GetService<LibraryDbContext>();

            if (!context.Database.EnsureCreated())
                context.Database.Migrate();

            return builder;
        }
    }
}
