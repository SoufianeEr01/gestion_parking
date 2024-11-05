using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion_Parking.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groupes",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groupes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PlaceParkings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    numero = table.Column<int>(type: "int", nullable: false),
                    etat = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaceParkings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Emplois",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Groupeid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emplois", x => x.id);
                    table.ForeignKey(
                        name: "FK_Emplois_Groupes_Groupeid",
                        column: x => x.Groupeid,
                        principalTable: "Groupes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Personnes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    motdepasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    groupe_id = table.Column<int>(type: "int", nullable: true),
                    Groupeid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Personnes_Groupes_Groupeid",
                        column: x => x.Groupeid,
                        principalTable: "Groupes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    heureDebut = table.Column<TimeOnly>(type: "time", nullable: false),
                    heureFin = table.Column<TimeOnly>(type: "time", nullable: false),
                    lieu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    etudiant_id = table.Column<int>(type: "int", nullable: false),
                    personnel_id = table.Column<int>(type: "int", nullable: false),
                    placeParking_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reservations_Personnes_etudiant_id",
                        column: x => x.etudiant_id,
                        principalTable: "Personnes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Personnes_personnel_id",
                        column: x => x.personnel_id,
                        principalTable: "Personnes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_PlaceParkings_placeParking_id",
                        column: x => x.placeParking_id,
                        principalTable: "PlaceParkings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emplois_Groupeid",
                table: "Emplois",
                column: "Groupeid");

            migrationBuilder.CreateIndex(
                name: "IX_Personnes_Groupeid",
                table: "Personnes",
                column: "Groupeid");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_etudiant_id",
                table: "Reservations",
                column: "etudiant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_personnel_id",
                table: "Reservations",
                column: "personnel_id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_placeParking_id",
                table: "Reservations",
                column: "placeParking_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emplois");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Personnes");

            migrationBuilder.DropTable(
                name: "PlaceParkings");

            migrationBuilder.DropTable(
                name: "Groupes");
        }
    }
}
