using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EliteRecruit.Migrations
{
    /// <inheritdoc />
    public partial class Imagepath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "ImageMimeType",
                table: "Student",
                newName: "ImagePath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Student",
                newName: "ImageMimeType");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Student",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
