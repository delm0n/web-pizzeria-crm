using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication.Migrations
{
    public partial class down_and_add_in_addish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeAddish",
                table: "Ingredients");

            migrationBuilder.AddColumn<string>(
                name: "TypeAddish",
                table: "Addishes",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeAddish",
                table: "Addishes");

            migrationBuilder.AddColumn<string>(
                name: "TypeAddish",
                table: "Ingredients",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
