using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunityNoticeBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class communityinvite1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedPost_Posts_PostId",
                table: "SavedPost");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedPost_Users_UserId",
                table: "SavedPost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedPost",
                table: "SavedPost");

            migrationBuilder.RenameTable(
                name: "SavedPost",
                newName: "SavedPosts");

            migrationBuilder.RenameIndex(
                name: "IX_SavedPost_UserId",
                table: "SavedPosts",
                newName: "IX_SavedPosts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SavedPost_PostId",
                table: "SavedPosts",
                newName: "IX_SavedPosts_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedPosts",
                table: "SavedPosts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CommunityInvites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommunityId = table.Column<int>(type: "int", nullable: false),
                    InvitedUserId = table.Column<int>(type: "int", nullable: false),
                    InvitedByUserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityInvites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunityInvites_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityInvites_Users_InvitedUserId",
                        column: x => x.InvitedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommunityInvites_CommunityId",
                table: "CommunityInvites",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityInvites_InvitedUserId",
                table: "CommunityInvites",
                column: "InvitedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedPosts_Posts_PostId",
                table: "SavedPosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedPosts_Users_UserId",
                table: "SavedPosts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedPosts_Posts_PostId",
                table: "SavedPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedPosts_Users_UserId",
                table: "SavedPosts");

            migrationBuilder.DropTable(
                name: "CommunityInvites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SavedPosts",
                table: "SavedPosts");

            migrationBuilder.RenameTable(
                name: "SavedPosts",
                newName: "SavedPost");

            migrationBuilder.RenameIndex(
                name: "IX_SavedPosts_UserId",
                table: "SavedPost",
                newName: "IX_SavedPost_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SavedPosts_PostId",
                table: "SavedPost",
                newName: "IX_SavedPost_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SavedPost",
                table: "SavedPost",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedPost_Posts_PostId",
                table: "SavedPost",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedPost_Users_UserId",
                table: "SavedPost",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
