using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace buckstore.products.service.infrastructure.Data.Migrations
{
    public partial class AddMapToContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products_favorites",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products_favorites", x => new { x.product_id, x.user_id });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products_favorites");
        }
    }
}
