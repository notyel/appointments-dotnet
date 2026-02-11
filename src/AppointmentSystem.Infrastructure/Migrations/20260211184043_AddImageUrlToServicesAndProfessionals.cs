using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToServicesAndProfessionals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Services",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Professionals",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Professionals");
        }
    }
}
