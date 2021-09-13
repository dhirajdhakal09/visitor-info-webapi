using Microsoft.EntityFrameworkCore.Migrations;

namespace Visitor.DatabaseMigration.Migrations
{
    public partial class VisitorLog_20210912 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CheckInStatus",
                table: "Visitors",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckInStatus",
                table: "Visitors");
        }
    }
}
