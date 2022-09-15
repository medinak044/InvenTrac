using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenTrac.Migrations
{
    public partial class WorkspaceModels_UpdateMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberRoles_Members_MemberId",
                table: "MemberRoles");

            migrationBuilder.DropIndex(
                name: "IX_MemberRoles_MemberId",
                table: "MemberRoles");

            migrationBuilder.DropColumn(
                name: "MemberId",
                table: "MemberRoles");

            migrationBuilder.AddColumn<int>(
                name: "MemberRoleId",
                table: "Members",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Members_MemberRoleId",
                table: "Members",
                column: "MemberRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_MemberRoles_MemberRoleId",
                table: "Members",
                column: "MemberRoleId",
                principalTable: "MemberRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_MemberRoles_MemberRoleId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_MemberRoleId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MemberRoleId",
                table: "Members");

            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                table: "MemberRoles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberRoles_MemberId",
                table: "MemberRoles",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberRoles_Members_MemberId",
                table: "MemberRoles",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
