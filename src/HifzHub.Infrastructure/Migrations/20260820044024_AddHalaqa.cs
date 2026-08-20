using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HifzHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHalaqa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Halaqat",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CenterId = table.Column<Guid>(type: "uuid", nullable: false),
                    StageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halaqat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Halaqat_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Halaqat_Stages_StageId",
                        column: x => x.StageId,
                        principalTable: "Stages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Halaqat_CenterId",
                table: "Halaqat",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Halaqat_StageId",
                table: "Halaqat",
                column: "StageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Halaqat");
        }
    }
}
