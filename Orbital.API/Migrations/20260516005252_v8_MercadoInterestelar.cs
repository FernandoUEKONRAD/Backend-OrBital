using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orbital.API.Migrations
{
    /// <inheritdoc />
    public partial class v8_MercadoInterestelar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_planeta_galaxia_GalaxiaNavId_Galaxia",
                table: "planeta");

            migrationBuilder.DropIndex(
                name: "IX_planeta_GalaxiaNavId_Galaxia",
                table: "planeta");

            migrationBuilder.DropColumn(
                name: "GalaxiaNavId_Galaxia",
                table: "planeta");

            migrationBuilder.CreateIndex(
                name: "IX_planeta_id_galaxia",
                table: "planeta",
                column: "id_galaxia");

            migrationBuilder.AddForeignKey(
                name: "FK_planeta_galaxia_id_galaxia",
                table: "planeta",
                column: "id_galaxia",
                principalTable: "galaxia",
                principalColumn: "id_galaxia",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_planeta_galaxia_id_galaxia",
                table: "planeta");

            migrationBuilder.DropIndex(
                name: "IX_planeta_id_galaxia",
                table: "planeta");

            migrationBuilder.AddColumn<int>(
                name: "GalaxiaNavId_Galaxia",
                table: "planeta",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_planeta_GalaxiaNavId_Galaxia",
                table: "planeta",
                column: "GalaxiaNavId_Galaxia");

            migrationBuilder.AddForeignKey(
                name: "FK_planeta_galaxia_GalaxiaNavId_Galaxia",
                table: "planeta",
                column: "GalaxiaNavId_Galaxia",
                principalTable: "galaxia",
                principalColumn: "id_galaxia");
        }
    }
}
