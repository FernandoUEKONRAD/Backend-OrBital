using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orbital.API.Migrations
{
    /// <inheritdoc />
    public partial class v7_SchemaFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // La DB ya fue creada desde el SQL script con la estructura correcta.
            // Esta migración solo registra el estado actual del modelo en EFMigrationsHistory.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
