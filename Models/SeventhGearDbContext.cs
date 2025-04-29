using Microsoft.EntityFrameworkCore;

namespace SeventhGearApi.Models
{
    public class SeventhGearDbContext : DbContext
    {
        public SeventhGearDbContext(DbContextOptions<SeventhGearDbContext> options)
            : base(options)
        {
        }

        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurações adicionais do modelo podem ser feitas aqui
            modelBuilder.Entity<ContactRequest>()
                .HasOne(cr => cr.Configuration)
                .WithMany()
                .HasForeignKey(cr => cr.ConfigurationId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}