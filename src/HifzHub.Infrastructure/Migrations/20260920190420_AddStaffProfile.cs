using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HifzHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStaffProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StaffProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CenterId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Bio = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Qualifications = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Certificates = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    YearsOfService = table.Column<int>(type: "integer", nullable: true),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffProfiles_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StaffProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaffProfiles_CenterId",
                table: "StaffProfiles",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffProfiles_UserId",
                table: "StaffProfiles",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffProfiles");
        }
    }
}
