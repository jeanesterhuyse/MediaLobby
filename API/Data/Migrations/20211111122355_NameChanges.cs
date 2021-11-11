using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class NameChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserPasswordHash",
                table: "Users",
                newName: "userPasswordHash");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "userName");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Users",
                newName: "userEmail");

            migrationBuilder.RenameColumn(
                name: "PasswordSalt",
                table: "Users",
                newName: "passwordSalt");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Photos",
                newName: "url");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Photos",
                newName: "publicId");

            migrationBuilder.RenameColumn(
                name: "IsMain",
                table: "Photos",
                newName: "isMain");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Photos",
                newName: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userPasswordHash",
                table: "Users",
                newName: "UserPasswordHash");

            migrationBuilder.RenameColumn(
                name: "userName",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "userEmail",
                table: "Users",
                newName: "UserEmail");

            migrationBuilder.RenameColumn(
                name: "passwordSalt",
                table: "Users",
                newName: "PasswordSalt");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "url",
                table: "Photos",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "publicId",
                table: "Photos",
                newName: "PublicId");

            migrationBuilder.RenameColumn(
                name: "isMain",
                table: "Photos",
                newName: "IsMain");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Photos",
                newName: "Id");
        }
    }
}
