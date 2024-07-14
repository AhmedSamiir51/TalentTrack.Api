using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TalentTrack.Core.Entities;

namespace TalentTrack.Infrastructure.Data
{
    public static class SeedData
    {
        public static readonly Todo Todo1 = new Todo
        {
            Name = "A",
        };

        public static readonly Todo Todo2 = new Todo
        {
            Name = "B",
        };
        public static readonly Todo Todo3 = new Todo
        {
            Name = "C",
        };

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if (dbContext.Todos.Any()) return;

                PopulateTestData(dbContext);
            }
        }
        public static void PopulateTestData(AppDbContext dbContext)
        {
            foreach (var contributor in dbContext.Todos)
            {
                dbContext.Remove(contributor);
            }
            dbContext.SaveChanges();

            dbContext.Todos.Add(Todo1);
            dbContext.Todos.Add(Todo2);
            dbContext.Todos.Add(Todo3);

            dbContext.SaveChanges();
        }
    }
}
