using Microsoft.EntityFrameworkCore.Migrations;

namespace TickettingSystem.Data.Migrations
{
    public partial class TradeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExchangeUserId",
                table: "Exchanges",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExchangeUserId",
                table: "Exchanges");
        }
    }
}
