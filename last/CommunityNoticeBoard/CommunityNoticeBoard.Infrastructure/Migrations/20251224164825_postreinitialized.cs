using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunityNoticeBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class postreinitialized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPinned",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPinned",
                table: "Posts");
        }
    }
}
