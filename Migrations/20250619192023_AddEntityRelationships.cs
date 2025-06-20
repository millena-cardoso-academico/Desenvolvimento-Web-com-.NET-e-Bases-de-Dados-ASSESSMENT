using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgenciaTurismo.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CidadesDestino_PacotesTuristicos_PacoteTuristicoId",
                table: "CidadesDestino");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Clientes_ClienteId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_PacotesTuristicos_PacoteTuristicoId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_CidadesDestino_PacoteTuristicoId",
                table: "CidadesDestino");

            migrationBuilder.DropColumn(
                name: "PacoteTuristicoId",
                table: "CidadesDestino");

            migrationBuilder.AlterColumn<int>(
                name: "PacoteTuristicoId",
                table: "Reservas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Reservas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CidadeDestinoPacoteTuristico",
                columns: table => new
                {
                    DestinosId = table.Column<int>(type: "INTEGER", nullable: false),
                    PacoteTuristicoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CidadeDestinoPacoteTuristico", x => new { x.DestinosId, x.PacoteTuristicoId });
                    table.ForeignKey(
                        name: "FK_CidadeDestinoPacoteTuristico_CidadesDestino_DestinosId",
                        column: x => x.DestinosId,
                        principalTable: "CidadesDestino",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CidadeDestinoPacoteTuristico_PacotesTuristicos_PacoteTuristicoId",
                        column: x => x.PacoteTuristicoId,
                        principalTable: "PacotesTuristicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CidadeDestinoPacoteTuristico_PacoteTuristicoId",
                table: "CidadeDestinoPacoteTuristico",
                column: "PacoteTuristicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Clientes_ClienteId",
                table: "Reservas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_PacotesTuristicos_PacoteTuristicoId",
                table: "Reservas",
                column: "PacoteTuristicoId",
                principalTable: "PacotesTuristicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Clientes_ClienteId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_PacotesTuristicos_PacoteTuristicoId",
                table: "Reservas");

            migrationBuilder.DropTable(
                name: "CidadeDestinoPacoteTuristico");

            migrationBuilder.AlterColumn<int>(
                name: "PacoteTuristicoId",
                table: "Reservas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Reservas",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "PacoteTuristicoId",
                table: "CidadesDestino",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CidadesDestino_PacoteTuristicoId",
                table: "CidadesDestino",
                column: "PacoteTuristicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CidadesDestino_PacotesTuristicos_PacoteTuristicoId",
                table: "CidadesDestino",
                column: "PacoteTuristicoId",
                principalTable: "PacotesTuristicos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Clientes_ClienteId",
                table: "Reservas",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_PacotesTuristicos_PacoteTuristicoId",
                table: "Reservas",
                column: "PacoteTuristicoId",
                principalTable: "PacotesTuristicos",
                principalColumn: "Id");
        }
    }
}
