using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunityNoticeBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class postreinitialized1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDraft",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDraft",
                table: "Posts");
        }
    }
}
