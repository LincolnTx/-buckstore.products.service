using Microsoft.EntityFrameworkCore.Migrations;

namespace buckstore.products.service.infrastructure.Data.Migrations
{
    public partial class AddUserNameToRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ProductRate",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ProductRate");
        }
    }
}
