using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesMac.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCategoriasnaDespesas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Despesas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_CategoriaId",
                table: "Despesas",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Categorias_CategoriaId",
                table: "Despesas",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Categorias_CategoriaId",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_CategoriaId",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Despesas");
        }
    }
}
