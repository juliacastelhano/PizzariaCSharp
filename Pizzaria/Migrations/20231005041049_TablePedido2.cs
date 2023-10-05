using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoPizzaria.Migrations
{
    public partial class TablePedido2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Cardapios_CardapioId",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "SaborCardapio",
                table: "Pedidos");

            migrationBuilder.AlterColumn<int>(
                name: "CardapioId",
                table: "Pedidos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Cardapios_CardapioId",
                table: "Pedidos",
                column: "CardapioId",
                principalTable: "Cardapios",
                principalColumn: "CardapioId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Cardapios_CardapioId",
                table: "Pedidos");

            migrationBuilder.AlterColumn<int>(
                name: "CardapioId",
                table: "Pedidos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "SaborCardapio",
                table: "Pedidos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Cardapios_CardapioId",
                table: "Pedidos",
                column: "CardapioId",
                principalTable: "Cardapios",
                principalColumn: "CardapioId");
        }
    }
}
