using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgenciaTurismo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PacotesTuristicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DataPartida = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DuracaoDias = table.Column<int>(type: "INTEGER", nullable: false),
                    CapacidadeMaxima = table.Column<int>(type: "INTEGER", nullable: false),
                    Preco = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacotesTuristicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaisesDestino",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaisesDestino", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataReserva = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClienteId = table.Column<int>(type: "INTEGER", nullable: true),
                    PacoteTuristicoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservas_PacotesTuristicos_PacoteTuristicoId",
                        column: x => x.PacoteTuristicoId,
                        principalTable: "PacotesTuristicos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CidadesDestino",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PaisDestinoId = table.Column<int>(type: "INTEGER", nullable: false),
                    PacoteTuristicoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CidadesDestino", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CidadesDestino_PacotesTuristicos_PacoteTuristicoId",
                        column: x => x.PacoteTuristicoId,
                        principalTable: "PacotesTuristicos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CidadesDestino_PaisesDestino_PaisDestinoId",
                        column: x => x.PaisDestinoId,
                        principalTable: "PaisesDestino",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CidadesDestino_PacoteTuristicoId",
                table: "CidadesDestino",
                column: "PacoteTuristicoId");

            migrationBuilder.CreateIndex(
                name: "IX_CidadesDestino_PaisDestinoId",
                table: "CidadesDestino",
                column: "PaisDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ClienteId",
                table: "Reservas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_PacoteTuristicoId",
                table: "Reservas",
                column: "PacoteTuristicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CidadesDestino");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "PaisesDestino");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "PacotesTuristicos");
        }
    }
}
