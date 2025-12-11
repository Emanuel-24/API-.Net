using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiFinal.Migrations
{
    /// <inheritdoc />
    public partial class FixReservaManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Servicios_ServicioId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_ServicioId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "ServicioId",
                table: "Reservas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServicioId",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ServicioId",
                table: "Reservas",
                column: "ServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Servicios_ServicioId",
                table: "Reservas",
                column: "ServicioId",
                principalTable: "Servicios",
                principalColumn: "ServicioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
