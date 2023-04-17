using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tdt4240_diving_duck_server.Migrations
{
    /// <inheritdoc />
    public partial class ChangedToFloat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScoreXPos",
                table: "Scores");

            migrationBuilder.AddColumn<float>(
                name: "TimeElapsed",
                table: "Scores",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeElapsed",
                table: "Scores");

            migrationBuilder.AddColumn<int>(
                name: "ScoreXPos",
                table: "Scores",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
