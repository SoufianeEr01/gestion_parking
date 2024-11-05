using Gestion_Parking.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // DbSet pour le modèle Personne
    public DbSet<Personne> Personnes { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Etudiant> Etudiants { get; set; }
    public DbSet<Personnel> Personnels { get; set; }
    public DbSet<Groupe> Groupes { get; set; }
    public DbSet<Emploi> Emplois { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<PlaceParking> PlaceParkings { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Etudiant)
            .WithMany(e => e.Reservations) // Relation inverse
            .HasForeignKey(r => r.etudiant_id)
            .OnDelete(DeleteBehavior.Restrict); // Ou NoAction

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Personnel)
            .WithMany(p => p.Reservations) // Relation inverse
            .HasForeignKey(r => r.personnel_id)
            .OnDelete(DeleteBehavior.Restrict); // Ou NoAction

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.PlaceParking)
            .WithMany(pp => pp.Reservations) // Relation inverse
            .HasForeignKey(r => r.placeParking_id)
            .OnDelete(DeleteBehavior.Restrict); // Ou NoAction
    }
}
