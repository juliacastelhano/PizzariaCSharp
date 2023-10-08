using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoPizzaria.Migrations
{
    public partial class TablePedidoChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "Pedidos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "Pedidos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
