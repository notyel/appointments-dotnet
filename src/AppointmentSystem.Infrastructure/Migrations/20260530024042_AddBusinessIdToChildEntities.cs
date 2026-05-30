using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBusinessIdToChildEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "ProfessionalServices",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "ProfessionalSchedules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "ProfessionalAbsences",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "BranchServices",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "BranchSchedules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "BranchHolidays",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            // Backfill: propagar BusinessId desde la tabla padre al hijo mediante JOIN
            var businessId = "e10d22f9-7871-4124-bf1c-a46472954d27";
            migrationBuilder.Sql($@"
                UPDATE ""BranchServices""       SET ""BusinessId"" = b.""BusinessId"" FROM ""Branches"" b       WHERE ""BranchServices"".""BranchId"" = b.""Id"" AND ""BranchServices"".""BusinessId"" = '00000000-0000-0000-0000-000000000000';
                UPDATE ""BranchSchedules""      SET ""BusinessId"" = b.""BusinessId"" FROM ""Branches"" b       WHERE ""BranchSchedules"".""BranchId"" = b.""Id"" AND ""BranchSchedules"".""BusinessId"" = '00000000-0000-0000-0000-000000000000';
                UPDATE ""BranchHolidays""       SET ""BusinessId"" = b.""BusinessId"" FROM ""Branches"" b       WHERE ""BranchHolidays"".""BranchId"" = b.""Id"" AND ""BranchHolidays"".""BusinessId"" = '00000000-0000-0000-0000-000000000000';
                UPDATE ""ProfessionalServices"" SET ""BusinessId"" = p.""BusinessId"" FROM ""Professionals"" p  WHERE ""ProfessionalServices"".""ProfessionalId"" = p.""Id"" AND ""ProfessionalServices"".""BusinessId"" = '00000000-0000-0000-0000-000000000000';
                UPDATE ""ProfessionalSchedules"" SET ""BusinessId"" = p.""BusinessId"" FROM ""Professionals"" p WHERE ""ProfessionalSchedules"".""ProfessionalId"" = p.""Id"" AND ""ProfessionalSchedules"".""BusinessId"" = '00000000-0000-0000-0000-000000000000';
                UPDATE ""ProfessionalAbsences"" SET ""BusinessId"" = p.""BusinessId"" FROM ""Professionals"" p  WHERE ""ProfessionalAbsences"".""ProfessionalId"" = p.""Id"" AND ""ProfessionalAbsences"".""BusinessId"" = '00000000-0000-0000-0000-000000000000';
            ");

            // Índices para queries directas por tenant sin JOIN
            migrationBuilder.CreateIndex("IX_BranchServices_BusinessId",       "BranchServices",       "BusinessId");
            migrationBuilder.CreateIndex("IX_BranchSchedules_BusinessId",      "BranchSchedules",      "BusinessId");
            migrationBuilder.CreateIndex("IX_BranchHolidays_BusinessId",       "BranchHolidays",       "BusinessId");
            migrationBuilder.CreateIndex("IX_ProfessionalServices_BusinessId", "ProfessionalServices", "BusinessId");
            migrationBuilder.CreateIndex("IX_ProfessionalSchedules_BusinessId","ProfessionalSchedules","BusinessId");
            migrationBuilder.CreateIndex("IX_ProfessionalAbsences_BusinessId", "ProfessionalAbsences", "BusinessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex("IX_BranchServices_BusinessId",       "BranchServices");
            migrationBuilder.DropIndex("IX_BranchSchedules_BusinessId",      "BranchSchedules");
            migrationBuilder.DropIndex("IX_BranchHolidays_BusinessId",       "BranchHolidays");
            migrationBuilder.DropIndex("IX_ProfessionalServices_BusinessId", "ProfessionalServices");
            migrationBuilder.DropIndex("IX_ProfessionalSchedules_BusinessId","ProfessionalSchedules");
            migrationBuilder.DropIndex("IX_ProfessionalAbsences_BusinessId", "ProfessionalAbsences");

            migrationBuilder.DropColumn(name: "BusinessId", table: "ProfessionalServices");
            migrationBuilder.DropColumn(name: "BusinessId", table: "ProfessionalSchedules");
            migrationBuilder.DropColumn(name: "BusinessId", table: "ProfessionalAbsences");
            migrationBuilder.DropColumn(name: "BusinessId", table: "BranchServices");
            migrationBuilder.DropColumn(name: "BusinessId", table: "BranchSchedules");
            migrationBuilder.DropColumn(name: "BusinessId", table: "BranchHolidays");
        }
    }
}
