using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MicroservicioSantiagoTrujillo.AccesoDatos.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MICROST_CLIENTE",
                columns: table => new
                {
                    ID_CLIENTE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CMP_CONTRASENA = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CMP_ESTADO = table.Column<bool>(type: "bit", nullable: false),
                    CMP_NOMBRE = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CMP_GENERO = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CMP_EDAD = table.Column<int>(type: "int", nullable: false),
                    CMP_IDENTIFICACION = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CMP_DIRECCION = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CMP_TELEFONO = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MICROST_CLIENTE", x => x.ID_CLIENTE);
                });

            migrationBuilder.CreateTable(
                name: "MICROST_CUENTAS",
                columns: table => new
                {
                    CMP_NUMERO_CUENTA = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CMP_ID_CLIENTE = table.Column<int>(type: "int", nullable: false),
                    CMP_TIPO = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CMP_ESTADO = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cuentas__5138EEC71FAE4CF6", x => x.CMP_NUMERO_CUENTA);
                    table.ForeignKey(
                        name: "FK_cuentas_cuentas",
                        column: x => x.CMP_ID_CLIENTE,
                        principalTable: "MICROST_CLIENTE",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MICROST_MOVIMIENTOS",
                columns: table => new
                {
                    ID_MOVIMIENTO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CMP_NUMERO_CUENTA = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CMP_FECHA = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    CMP_TIPO_MOVIMIENTO = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    CMP_SALDO_INICIAL = table.Column<decimal>(type: "MONEY", nullable: false),
                    CMP_MOVIMIENTO = table.Column<decimal>(type: "MONEY", nullable: false),
                    CMP_SALDO_DISPONIBLE = table.Column<decimal>(type: "MONEY", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__movimien__2D96FD74C19E909F", x => x.ID_MOVIMIENTO);
                    table.ForeignKey(
                        name: "FK_movimientos_cuentas",
                        column: x => x.CMP_NUMERO_CUENTA,
                        principalTable: "MICROST_CUENTAS",
                        principalColumn: "CMP_NUMERO_CUENTA",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MICROST_CUENTAS_CMP_ID_CLIENTE",
                table: "MICROST_CUENTAS",
                column: "CMP_ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_MICROST_MOVIMIENTOS_CMP_NUMERO_CUENTA",
                table: "MICROST_MOVIMIENTOS",
                column: "CMP_NUMERO_CUENTA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MICROST_MOVIMIENTOS");

            migrationBuilder.DropTable(
                name: "MICROST_CUENTAS");

            migrationBuilder.DropTable(
                name: "MICROST_CLIENTE");
        }
    }
}
