using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionAndSpecialtyToProfessional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"Professionals\" ADD COLUMN IF NOT EXISTS \"Description\" text;");
            migrationBuilder.Sql("ALTER TABLE \"Professionals\" ADD COLUMN IF NOT EXISTS \"Specialty\" text;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Professionals");

            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Professionals");
        }
    }
}
