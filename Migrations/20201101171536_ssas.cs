using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ofarz_rest_api.Migrations
{
    public partial class ssas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgentOrderId",
                table: "OrderDetails",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AgentOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderNo = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    PhoneNo = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<double>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: true),
                    AddressId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgentOrders_Markets_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgentOrders_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgentOrders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_AgentOrderId",
                table: "OrderDetails",
                column: "AgentOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentOrders_AddressId",
                table: "AgentOrders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentOrders_RoleId",
                table: "AgentOrders",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentOrders_UserId",
                table: "AgentOrders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_AgentOrders_AgentOrderId",
                table: "OrderDetails",
                column: "AgentOrderId",
                principalTable: "AgentOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_AgentOrders_AgentOrderId",
                table: "OrderDetails");

            migrationBuilder.DropTable(
                name: "AgentOrders");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_AgentOrderId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "AgentOrderId",
                table: "OrderDetails");
        }
    }
}
