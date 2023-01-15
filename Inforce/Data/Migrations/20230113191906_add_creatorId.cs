using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inforce.Data.Migrations
{
    public partial class add_creatorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "UrlShortener",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "UrlShortener");
        }
    }
}
