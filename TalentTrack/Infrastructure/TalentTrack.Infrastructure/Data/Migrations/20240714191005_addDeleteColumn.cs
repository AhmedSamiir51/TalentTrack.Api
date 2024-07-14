using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TalentTrack.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addDeleteColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "JobTitles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Applicants",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "JobTitles");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Applicants");
        }
    }
}
