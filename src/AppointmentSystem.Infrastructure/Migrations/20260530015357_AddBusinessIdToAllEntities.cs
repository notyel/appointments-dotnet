using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBusinessIdToAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Services",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Professionals",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Notifications",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Clients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Categories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BusinessId",
                table: "Appointments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            // Backfill: actualizar registros existentes con el BusinessId del tenant configurado
            var businessId = "e10d22f9-7871-4124-bf1c-a46472954d27";
            migrationBuilder.Sql($@"
                UPDATE ""Services""      SET ""BusinessId"" = '{businessId}' WHERE ""BusinessId"" = '00000000-0000-0000-0000-000000000000';
                UPDATE ""Professionals"" SET ""BusinessId"" = '{businessId}' WHERE ""BusinessId"" = '00000000-0000-0000-0000-000000000000';
                UPDATE ""Notifications"" SET ""BusinessId"" = '{businessId}' WHERE ""BusinessId"" = '00000000-0000-0000-0000-000000000000';
                UPDATE ""Clients""       SET ""BusinessId"" = '{businessId}' WHERE ""BusinessId"" = '00000000-0000-0000-0000-000000000000';
                UPDATE ""Categories""   SET ""BusinessId"" = '{businessId}' WHERE ""BusinessId"" = '00000000-0000-0000-0000-000000000000';
                UPDATE ""Appointments"" SET ""BusinessId"" = '{businessId}' WHERE ""BusinessId"" = '00000000-0000-0000-0000-000000000000';
            ");

            // Índices para consultas eficientes por tenant
            migrationBuilder.CreateIndex("IX_Services_BusinessId",      "Services",      "BusinessId");
            migrationBuilder.CreateIndex("IX_Professionals_BusinessId", "Professionals", "BusinessId");
            migrationBuilder.CreateIndex("IX_Clients_BusinessId",       "Clients",       "BusinessId");
            migrationBuilder.CreateIndex("IX_Appointments_BusinessId",  "Appointments",  "BusinessId");
            migrationBuilder.CreateIndex("IX_Categories_BusinessId",    "Categories",    "BusinessId");
            migrationBuilder.CreateIndex("IX_Notifications_BusinessId", "Notifications", "BusinessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex("IX_Services_BusinessId",      "Services");
            migrationBuilder.DropIndex("IX_Professionals_BusinessId", "Professionals");
            migrationBuilder.DropIndex("IX_Clients_BusinessId",       "Clients");
            migrationBuilder.DropIndex("IX_Appointments_BusinessId",  "Appointments");
            migrationBuilder.DropIndex("IX_Categories_BusinessId",    "Categories");
            migrationBuilder.DropIndex("IX_Notifications_BusinessId", "Notifications");

            migrationBuilder.DropColumn(name: "BusinessId", table: "Services");
            migrationBuilder.DropColumn(name: "BusinessId", table: "Professionals");
            migrationBuilder.DropColumn(name: "BusinessId", table: "Notifications");
            migrationBuilder.DropColumn(name: "BusinessId", table: "Clients");
            migrationBuilder.DropColumn(name: "BusinessId", table: "Categories");
            migrationBuilder.DropColumn(name: "BusinessId", table: "Appointments");
        }
    }
}
