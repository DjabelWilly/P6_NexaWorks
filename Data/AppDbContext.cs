using Microsoft.EntityFrameworkCore;
using P6_NexaWorks.Models.Entities;
using Version = P6_NexaWorks.Models.Entities.Version;

namespace P6_NexaWorks.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Tables
        public DbSet<Product> Products { get; set; }
        public DbSet<Version> Versions { get; set; }
        public DbSet<OS> OSes { get; set; }
        public DbSet<VersionOS> VersionOSes { get; set; }
        public DbSet<Issue> Issues { get; set; }

        // Config du modèle de données
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Version>()
                .HasOne(v => v.Product)
                .WithMany(p => p.Versions)
                .HasForeignKey(v => v.ProductId);

            modelBuilder.Entity<VersionOS>()
                .HasOne(vo => vo.Version)
                .WithMany(v => v.VersionOSes)
                .HasForeignKey(vo => vo.VersionId);

            modelBuilder.Entity<VersionOS>()
                .HasOne(vo => vo.OS)
                .WithMany(os => os.VersionOSes)
                .HasForeignKey(vo => vo.OSId);

            modelBuilder.Entity<Issue>()
                .HasOne(i => i.VersionOS)
                .WithMany(vo => vo.Issues)
                .HasForeignKey(i => i.VersionOSId);
        }
    }
}
