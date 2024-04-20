using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EliteRecruit.Migrations
{
    /// <inheritdoc />
    public partial class PipelineStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PipelineStatusId",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PipelineStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contact = table.Column<bool>(type: "bit", nullable: false),
                    Interview = table.Column<bool>(type: "bit", nullable: false),
                    Offered = table.Column<bool>(type: "bit", nullable: false),
                    Hired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipelineStatus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_PipelineStatusId",
                table: "Student",
                column: "PipelineStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_PipelineStatus_PipelineStatusId",
                table: "Student",
                column: "PipelineStatusId",
                principalTable: "PipelineStatus",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_PipelineStatus_PipelineStatusId",
                table: "Student");

            migrationBuilder.DropTable(
                name: "PipelineStatus");

            migrationBuilder.DropIndex(
                name: "IX_Student_PipelineStatusId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "PipelineStatusId",
                table: "Student");
        }
    }
}
