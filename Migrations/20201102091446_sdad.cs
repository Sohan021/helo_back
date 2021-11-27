using Microsoft.EntityFrameworkCore.Migrations;

namespace ofarz_rest_api.Migrations
{
    public partial class sdad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DistrictName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DivisionName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarketCode",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarketName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefferName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnionOrWardName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpozilaName",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DistrictName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DivisionName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MarketCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MarketName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefferName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UnionOrWardName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpozilaName",
                table: "AspNetUsers");
        }
    }
}
