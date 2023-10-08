using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoPizzaria.Migrations
{
    public partial class TableCarrinhoChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalTodosItens",
                table: "Carrinhos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalTodosItens",
                table: "Carrinhos",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
