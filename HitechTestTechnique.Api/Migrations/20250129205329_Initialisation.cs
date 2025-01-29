using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HitechTestTechnique.Web.Migrations
{
    /// <inheritdoc />
    public partial class Initialisation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Depenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Montant = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Commentaire = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    DistanceKm = table.Column<long>(type: "bigint", nullable: true),
                    NombreInvites = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depenses", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Depenses");
        }
    }
}
