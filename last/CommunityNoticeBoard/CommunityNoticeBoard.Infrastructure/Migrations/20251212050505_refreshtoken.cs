using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunityNoticeBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class refreshtoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RefreshTokenId",
                table: "RefreshTokens",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RefreshTokens",
                newName: "RefreshTokenId");
        }
    }
}
