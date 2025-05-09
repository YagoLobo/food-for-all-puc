using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodforAll.Migrations
{
    /// <inheritdoc />
    public partial class M01CriandoTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstabelecimentoDoador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeFantasia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransporteProprio = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstabelecimentoDoador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstabelecimentoReceptor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeReceptor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistroGoverno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aprovada = table.Column<bool>(type: "bit", nullable: false),
                    RefeicoesDiarias = table.Column<int>(type: "int", nullable: true),
                    ResponsavelTecnico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HorarioRecebimentoDoacoes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstabelecimentoReceptor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    EstabelecimentoDoadorId = table.Column<int>(type: "int", nullable: true),
                    EstabelecimentoReceptorId = table.Column<int>(type: "int", nullable: true),
                    DoadorId = table.Column<int>(type: "int", nullable: true),
                    ReceptorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_EstabelecimentoDoador_DoadorId",
                        column: x => x.DoadorId,
                        principalTable: "EstabelecimentoDoador",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuario_EstabelecimentoReceptor_ReceptorId",
                        column: x => x.ReceptorId,
                        principalTable: "EstabelecimentoReceptor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_DoadorId",
                table: "Usuario",
                column: "DoadorId",
                unique: true,
                filter: "[DoadorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_ReceptorId",
                table: "Usuario",
                column: "ReceptorId",
                unique: true,
                filter: "[ReceptorId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "EstabelecimentoDoador");

            migrationBuilder.DropTable(
                name: "EstabelecimentoReceptor");
        }
    }
}
