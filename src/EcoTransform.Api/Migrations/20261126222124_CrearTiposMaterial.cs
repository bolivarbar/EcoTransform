using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EcoTransform.Api.Migrations
{
    /// <inheritdoc />
    public partial class CrearTiposMaterial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TiposMaterial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ruta = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PurezaMinima = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    ContaminacionMaxima = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposMaterial", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TiposMaterial",
                columns: new[] { "Id", "Activo", "ContaminacionMaxima", "Descripcion", "Nombre", "PurezaMinima", "Ruta" },
                values: new object[,]
                {
                    { 1, true, 10m, "Plástico PET limpio, sin tapas ni etiquetas.", "PET (botellas de plástico)", 90m, 1 },
                    { 2, true, 15m, "Neumáticos fuera de uso, con acero y fibras por separar.", "Caucho de neumáticos usados", 85m, 2 },
                    { 3, true, 5m, "Restos de cocina y vegetales, sin plásticos ni vidrio.", "Residuos orgánicos para compost", 95m, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TiposMaterial_Nombre",
                table: "TiposMaterial",
                column: "Nombre",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TiposMaterial");
        }
    }
}
