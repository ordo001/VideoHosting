using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoHostingApi.FileService.Context.Migrations
{
    /// <inheritdoc />
    public partial class addSizeFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "Video",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "Images",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Images");
        }
    }
}
