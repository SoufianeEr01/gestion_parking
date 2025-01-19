﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gestion_Parking.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250111204627_AddPaiementRelation2")]
    partial class AddPaiementRelation2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gestion_Parking.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateEnvoi")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Gestion_Parking.Models.Emploi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("DateDebut")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("DateFin")
                        .HasColumnType("time");

                    b.Property<int>("Groupe_Id")
                        .HasColumnType("int");

                    b.Property<string>("Jour")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Groupe_Id");

                    b.ToTable("Emplois");
                });

            modelBuilder.Entity("Gestion_Parking.Models.EmploiPersonnel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("HeureDebut")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("HeureFin")
                        .HasColumnType("time");

                    b.Property<string>("Jour")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PersonnelId")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PersonnelId");

                    b.ToTable("EmploiPersonnels");
                });

            modelBuilder.Entity("Gestion_Parking.Models.Groupe", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Groupes");
                });

            modelBuilder.Entity("Gestion_Parking.Models.Paiement", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("mode_paiement")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("personne_id")
                        .HasColumnType("int");

                    b.Property<float>("prix_paye")
                        .HasColumnType("real");

                    b.HasKey("id");

                    b.HasIndex("personne_id");

                    b.ToTable("Paiements");
                });

            modelBuilder.Entity("Gestion_Parking.Models.Personne", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("motdepasse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Personnes");

                    b.HasDiscriminator().HasValue("Personne");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Gestion_Parking.Models.PlaceParking", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateOnly?>("dateFinReservation")
                        .HasColumnType("date");

                    b.Property<int>("etage")
                        .HasColumnType("int");

                    b.Property<string>("etat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("numero")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("PlaceParkings");
                });

            modelBuilder.Entity("Gestion_Parking.Models.Reponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ContactId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateReponse")
                        .HasColumnType("datetime2");

                    b.Property<string>("MessageReponse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("Reponses");
                });

            modelBuilder.Entity("Reservation", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int?>("Etudiantid")
                        .HasColumnType("int");

                    b.Property<int?>("Personnelid")
                        .HasColumnType("int");

                    b.Property<DateOnly>("date")
                        .HasColumnType("date");

                    b.Property<string>("etat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeOnly>("heureDebut")
                        .HasColumnType("time");

                    b.Property<TimeOnly>("heureFin")
                        .HasColumnType("time");

                    b.Property<string>("lieu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("personne_id")
                        .HasColumnType("int");

                    b.Property<int>("placeParking_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Etudiantid");

                    b.HasIndex("Personnelid");

                    b.HasIndex("personne_id");

                    b.HasIndex("placeParking_id");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Gestion_Parking.Models.Admin", b =>
                {
                    b.HasBaseType("Gestion_Parking.Models.Personne");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("Gestion_Parking.Models.Etudiant", b =>
                {
                    b.HasBaseType("Gestion_Parking.Models.Personne");

                    b.Property<int>("GroupeId")
                        .HasColumnType("int");

                    b.HasIndex("GroupeId");

                    b.HasDiscriminator().HasValue("Etudiant");
                });

            modelBuilder.Entity("Gestion_Parking.Models.Personnel", b =>
                {
                    b.HasBaseType("Gestion_Parking.Models.Personne");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Personnel");
                });

            modelBuilder.Entity("Gestion_Parking.Models.Emploi", b =>
                {
                    b.HasOne("Gestion_Parking.Models.Groupe", "Groupe")
                        .WithMany("Emplois")
                        .HasForeignKey("Groupe_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Groupe");
                });

            modelBuilder.Entity("Gestion_Parking.Models.EmploiPersonnel", b =>
                {
                    b.HasOne("Gestion_Parking.Models.Personnel", "Personnel")
                        .WithMany("EmploiPersonnels")
                        .HasForeignKey("PersonnelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personnel");
                });

            modelBuilder.Entity("Gestion_Parking.Models.Paiement", b =>
                {
                    b.HasOne("Gestion_Parking.Models.Personne", "Personne")
                        .WithMany("Paiements")
                        .HasForeignKey("personne_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Personne");
                });

            modelBuilder.Entity("Gestion_Parking.Models.Reponse", b =>
                {
                    b.HasOne("Gestion_Parking.Models.Contact", "Contact")
                        .WithMany("Reponses")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Reservation", b =>
                {
                    b.HasOne("Gestion_Parking.Models.Etudiant", null)
                        .WithMany("Reservations")
                        .HasForeignKey("Etudiantid");

                    b.HasOne("Gestion_Parking.Models.Personnel", null)
                        .WithMany("Reservations")
                        .HasForeignKey("Personnelid");

                    b.HasOne("Gestion_Parking.Models.Personne", "Personne")
                        .WithMany()
                        .HasForeignKey("personne_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Gestion_Parking.Models.PlaceParking", "PlaceParking")
                        .WithMany("Reservations")
                        .HasForeignKey("placeParking_id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Personne");

                    b.Navigation("PlaceParking");
                });

            modelBuilder.Entity("Gestion_Parking.Models.Etudiant", b =>
                {
                    b.HasOne("Gestion_Parking.Models.Groupe", "EtudiantGroupe")
                        .WithMany("Etudiants")
                        .HasForeignKey("GroupeId");

                    b.Navigation("EtudiantGroupe");
                });

            modelBuilder.Entity("Gestion_Parking.Models.Contact", b =>
                {
                    b.Navigation("Reponses");
                });

            modelBuilder.Entity("Gestion_Parking.Models.Groupe", b =>
                {
                    b.Navigation("Emplois");

                    b.Navigation("Etudiants");
                });

            modelBuilder.Entity("Gestion_Parking.Models.Personne", b =>
                {
                    b.Navigation("Paiements");
                });

            modelBuilder.Entity("Gestion_Parking.Models.PlaceParking", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Gestion_Parking.Models.Etudiant", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Gestion_Parking.Models.Personnel", b =>
                {
                    b.Navigation("EmploiPersonnels");

                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
