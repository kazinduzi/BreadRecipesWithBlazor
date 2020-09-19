using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountOwnerServer.Migrations
{
    public partial class Added_Recipe_Duration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "healthyStatus",
                table: "Recipes",
                newName: "HealthyStatus");

            migrationBuilder.AddColumn<int>(
                name: "DurationInMinutes",
                table: "Recipes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInMinutes",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "HealthyStatus",
                table: "Recipes",
                newName: "healthyStatus");
        }
    }
}
