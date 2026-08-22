using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HifzHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRecitation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recitations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CenterId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    HalaqaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    FromSurah = table.Column<int>(type: "integer", nullable: false),
                    FromAyah = table.Column<int>(type: "integer", nullable: false),
                    ToSurah = table.Column<int>(type: "integer", nullable: false),
                    ToAyah = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Grade = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recitations_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recitations_Halaqat_HalaqaId",
                        column: x => x.HalaqaId,
                        principalTable: "Halaqat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Recitations_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recitations_CenterId",
                table: "Recitations",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Recitations_HalaqaId",
                table: "Recitations",
                column: "HalaqaId");

            migrationBuilder.CreateIndex(
                name: "IX_Recitations_StudentId_Date",
                table: "Recitations",
                columns: new[] { "StudentId", "Date" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recitations");
        }
    }
}
