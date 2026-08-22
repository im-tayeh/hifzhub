using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HifzHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSurah : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Surahs",
                columns: table => new
                {
                    Number = table.Column<int>(type: "integer", nullable: false),
                    NameAr = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NameEn = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AyahCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surahs", x => x.Number);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Surahs");
        }
    }
}
