using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountOwnerServer.Migrations
{
    public partial class Renaming_Column_UnityOfMeasure_For_Table_Ingredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnityMesure",
                table: "Ingredients");

            migrationBuilder.AddColumn<string>(
                name: "UnityOfMeasure",
                table: "Ingredients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnityOfMeasure",
                table: "Ingredients");

            migrationBuilder.AddColumn<string>(
                name: "UnityMesure",
                table: "Ingredients",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
