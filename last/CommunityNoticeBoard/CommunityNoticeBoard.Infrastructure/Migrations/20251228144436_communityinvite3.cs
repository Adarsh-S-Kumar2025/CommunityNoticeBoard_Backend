using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunityNoticeBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class communityinvite3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunityInvites_Users_InvitedUserId",
                table: "CommunityInvites");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityInvites_InvitedByUserId",
                table: "CommunityInvites",
                column: "InvitedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityInvites_Users_InvitedByUserId",
                table: "CommunityInvites",
                column: "InvitedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityInvites_Users_InvitedUserId",
                table: "CommunityInvites",
                column: "InvitedUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommunityInvites_Users_InvitedByUserId",
                table: "CommunityInvites");

            migrationBuilder.DropForeignKey(
                name: "FK_CommunityInvites_Users_InvitedUserId",
                table: "CommunityInvites");

            migrationBuilder.DropIndex(
                name: "IX_CommunityInvites_InvitedByUserId",
                table: "CommunityInvites");

            migrationBuilder.AddForeignKey(
                name: "FK_CommunityInvites_Users_InvitedUserId",
                table: "CommunityInvites",
                column: "InvitedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
