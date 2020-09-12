using Microsoft.EntityFrameworkCore.Migrations;

namespace ComplainBox.Migrations
{
    public partial class IDAddedUserAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IDNumber",
                table: "userAccounts",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDNumber",
                table: "userAccounts");
        }
    }
}
