using Microsoft.EntityFrameworkCore.Migrations;

namespace Kuzgun.DataAccess.Migrations
{
    public partial class changePostCommentProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PostComments_PostId",
                table: "PostComments");

            migrationBuilder.DropIndex(
                name: "IX_PostComments_UserId",
                table: "PostComments");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_PostId",
                table: "PostComments",
                column: "PostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_UserId",
                table: "PostComments",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PostComments_PostId",
                table: "PostComments");

            migrationBuilder.DropIndex(
                name: "IX_PostComments_UserId",
                table: "PostComments");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_PostId",
                table: "PostComments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_UserId",
                table: "PostComments",
                column: "UserId");
        }
    }
}
