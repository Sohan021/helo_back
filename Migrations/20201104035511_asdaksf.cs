using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ofarz_rest_api.Migrations
{
    public partial class asdaksf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgentOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(nullable: true),
                    AgentOrderId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgentOrderDetails_AgentOrders_AgentOrderId",
                        column: x => x.AgentOrderId,
                        principalTable: "AgentOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgentOrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgentOrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgentOrderDetails_AgentOrderId",
                table: "AgentOrderDetails",
                column: "AgentOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentOrderDetails_OrderId",
                table: "AgentOrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_AgentOrderDetails_ProductId",
                table: "AgentOrderDetails",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentOrderDetails");
        }
    }
}
