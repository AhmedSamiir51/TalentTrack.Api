using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TalentTrack.Core.Entities;
using TalentTrack.Core.Interfaces;
using TalentTrack.Infrastructure.Data.Config;

namespace TalentTrack.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Applicant> Applicants { get; set; }
        public virtual DbSet<JobTitle> JobTitles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.AddGlobalQueryFilter<IMustHaveDelete>(e => !e.IsDeleted);

        }

    }
}
