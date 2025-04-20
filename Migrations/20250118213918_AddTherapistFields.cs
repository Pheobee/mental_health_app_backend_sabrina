using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentalHealthAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTherapistFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Region",
                table: "Therapists",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Therapists",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Therapists",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Therapists");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Therapists");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Therapists",
                newName: "Region");
        }
    }
}
