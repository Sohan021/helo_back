using Microsoft.EntityFrameworkCore.Migrations;

namespace ofarz_rest_api.Migrations
{
    public partial class Ikdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "GetMoneyByAgentShopping",
                table: "OfarzFunds",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GetMoneyByAgentShopping",
                table: "OfarzFunds");
        }
    }
}
