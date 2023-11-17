using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ef_app.Migrations
{
    public partial class WithoutTestfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestField",
                table: "Managements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestField",
                table: "Managements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
