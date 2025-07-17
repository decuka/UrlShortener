using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLastEditedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AboutInfos_AspNetUsers_LastEditedById",
                table: "AboutInfos");

            migrationBuilder.DropIndex(
                name: "IX_AboutInfos_LastEditedById",
                table: "AboutInfos");

            migrationBuilder.DropColumn(
                name: "LastEditedById",
                table: "AboutInfos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastEditedById",
                table: "AboutInfos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AboutInfos_LastEditedById",
                table: "AboutInfos",
                column: "LastEditedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AboutInfos_AspNetUsers_LastEditedById",
                table: "AboutInfos",
                column: "LastEditedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
