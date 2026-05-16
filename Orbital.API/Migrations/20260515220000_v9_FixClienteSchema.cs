using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Orbital.API.Data;

#nullable disable

namespace Orbital.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20260515220000_v9_FixClienteSchema")]
    public partial class v9_FixClienteSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // =============================================
            // FIX tabla `cliente`
            // La BD fue creada con nombres distintos al modelo C#
            // =============================================

            // Renombrar columnas para coincidir con el modelo
            migrationBuilder.Sql("ALTER TABLE `cliente` RENAME COLUMN `nombre_cliente` TO `nombre`;");
            migrationBuilder.Sql("ALTER TABLE `cliente` RENAME COLUMN `correo_contacto` TO `correo`;");

            // Reemplazar índice único con nombre EF-convention
            migrationBuilder.Sql("ALTER TABLE `cliente` DROP INDEX `uq_cliente_correo`;");
            migrationBuilder.CreateIndex(
                name: "IX_cliente_Correo",
                table: "cliente",
                column: "correo",
                unique: true);

            // Eliminar galaxia_origen varchar (reemplazada por FK)
            migrationBuilder.DropColumn(
                name: "galaxia_origen",
                table: "cliente");

            // Agregar id_galaxia_origen como FK int a galaxia
            migrationBuilder.AddColumn<int>(
                name: "id_galaxia_origen",
                table: "cliente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cliente_Id_Galaxia_Origen",
                table: "cliente",
                column: "id_galaxia_origen");

            migrationBuilder.AddForeignKey(
                name: "FK_cliente_galaxia_id_galaxia_origen",
                table: "cliente",
                column: "id_galaxia_origen",
                principalTable: "galaxia",
                principalColumn: "id_galaxia",
                onDelete: ReferentialAction.Restrict);

            // =============================================
            // FIX tabla `historico_ciclo_planetario`
            // Faltan columnas requeridas por el modelo C#
            // =============================================

            migrationBuilder.Sql(
                "ALTER TABLE `historico_ciclo_planetario` ADD COLUMN `tipo_evento` longtext NOT NULL DEFAULT ('');");

            migrationBuilder.AddColumn<string>(
                name: "descripcion",
                table: "historico_ciclo_planetario",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_transaccion",
                table: "historico_ciclo_planetario",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_cliente_anterior",
                table: "historico_ciclo_planetario",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_cliente_nuevo",
                table: "historico_ciclo_planetario",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_historico_ciclo_planetario_Id_Cliente_Anterior",
                table: "historico_ciclo_planetario",
                column: "id_cliente_anterior");

            migrationBuilder.CreateIndex(
                name: "IX_historico_ciclo_planetario_Id_Cliente_Nuevo",
                table: "historico_ciclo_planetario",
                column: "id_cliente_nuevo");

            migrationBuilder.CreateIndex(
                name: "IX_historico_ciclo_planetario_Id_Transaccion",
                table: "historico_ciclo_planetario",
                column: "id_transaccion");

            migrationBuilder.AddForeignKey(
                name: "FK_historico_ciclo_planetario_cliente_id_cliente_anterior",
                table: "historico_ciclo_planetario",
                column: "id_cliente_anterior",
                principalTable: "cliente",
                principalColumn: "id_cliente",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_historico_ciclo_planetario_cliente_id_cliente_nuevo",
                table: "historico_ciclo_planetario",
                column: "id_cliente_nuevo",
                principalTable: "cliente",
                principalColumn: "id_cliente",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_historico_ciclo_planetario_transaccion_id_transaccion",
                table: "historico_ciclo_planetario",
                column: "id_transaccion",
                principalTable: "transaccion",
                principalColumn: "id_transaccion",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revertir historico_ciclo_planetario
            migrationBuilder.DropForeignKey(
                name: "FK_historico_ciclo_planetario_cliente_id_cliente_anterior",
                table: "historico_ciclo_planetario");
            migrationBuilder.DropForeignKey(
                name: "FK_historico_ciclo_planetario_cliente_id_cliente_nuevo",
                table: "historico_ciclo_planetario");
            migrationBuilder.DropForeignKey(
                name: "FK_historico_ciclo_planetario_transaccion_id_transaccion",
                table: "historico_ciclo_planetario");

            migrationBuilder.DropIndex(
                name: "IX_historico_ciclo_planetario_Id_Cliente_Anterior",
                table: "historico_ciclo_planetario");
            migrationBuilder.DropIndex(
                name: "IX_historico_ciclo_planetario_Id_Cliente_Nuevo",
                table: "historico_ciclo_planetario");
            migrationBuilder.DropIndex(
                name: "IX_historico_ciclo_planetario_Id_Transaccion",
                table: "historico_ciclo_planetario");

            migrationBuilder.DropColumn(name: "tipo_evento", table: "historico_ciclo_planetario");
            migrationBuilder.DropColumn(name: "descripcion", table: "historico_ciclo_planetario");
            migrationBuilder.DropColumn(name: "id_transaccion", table: "historico_ciclo_planetario");
            migrationBuilder.DropColumn(name: "id_cliente_anterior", table: "historico_ciclo_planetario");
            migrationBuilder.DropColumn(name: "id_cliente_nuevo", table: "historico_ciclo_planetario");

            // Revertir cliente
            migrationBuilder.DropForeignKey(
                name: "FK_cliente_galaxia_id_galaxia_origen",
                table: "cliente");
            migrationBuilder.DropIndex(
                name: "IX_cliente_Id_Galaxia_Origen",
                table: "cliente");
            migrationBuilder.DropIndex(
                name: "IX_cliente_Correo",
                table: "cliente");
            migrationBuilder.DropColumn(name: "id_galaxia_origen", table: "cliente");

            migrationBuilder.AddColumn<string>(
                name: "galaxia_origen",
                table: "cliente",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.Sql("ALTER TABLE `cliente` RENAME COLUMN `correo` TO `correo_contacto`;");
            migrationBuilder.Sql("ALTER TABLE `cliente` RENAME COLUMN `nombre` TO `nombre_cliente`;");
            migrationBuilder.Sql("ALTER TABLE `cliente` ADD UNIQUE INDEX `uq_cliente_correo` (`correo_contacto`);");
        }
    }
}
