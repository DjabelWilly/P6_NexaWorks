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

            // Relations OneToMany

            // 1 Version appartient à 1 Product
            // 1 product N version
            modelBuilder.Entity<Version>()
                .HasOne(v => v.Product)
                .WithMany(p => p.Versions)
                .HasForeignKey(v => v.ProductId);

            // 1 VersionOS pointe vers 1 seule Version.
            // 1 Version peut être associée à N VersionOS (donc plusieurs OS supportés)
            modelBuilder.Entity<VersionOS>()
                .HasOne(vo => vo.Version)
                .WithMany(v => v.VersionOSes)
                .HasForeignKey(vo => vo.VersionId);

            // 1 VersionOS correspond à 1 seul OS
            // 1 OS peut apparaître dans N associations VersionOS.
            modelBuilder.Entity<VersionOS>()
                .HasOne(vo => vo.OS)
                .WithMany(os => os.VersionOSes)
                .HasForeignKey(vo => vo.OSId);

            // 1 Issue (bug, ticket) concerne 1 combinaison précise Version + OS
            // 1 VersionOS peut avoir N Issues associées.
            modelBuilder.Entity<Issue>()
                .HasOne(i => i.VersionOS)
                .WithMany(vo => vo.Issues)
                .HasForeignKey(i => i.VersionOSId);
        }
    }
}
