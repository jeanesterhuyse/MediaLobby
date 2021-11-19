using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class Newww : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Users_AppUserid",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Folders_Foldersid",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "Foldersid",
                table: "Photos",
                newName: "foldersId");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_Foldersid",
                table: "Photos",
                newName: "IX_Photos_foldersId");

            migrationBuilder.RenameColumn(
                name: "AppUserid",
                table: "Folders",
                newName: "appUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Folders_AppUserid",
                table: "Folders",
                newName: "IX_Folders_appUserId");

            migrationBuilder.AlterColumn<int>(
                name: "foldersId",
                table: "Photos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "appUserId",
                table: "Folders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "MetaData",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    photoid = table.Column<int>(type: "INTEGER", nullable: false),
                    location = table.Column<string>(type: "TEXT", nullable: true),
                    tags = table.Column<string>(type: "TEXT", nullable: true),
                    date = table.Column<string>(type: "TEXT", nullable: true),
                    capturedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaData", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Users_appUserId",
                table: "Folders",
                column: "appUserId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Folders_foldersId",
                table: "Photos",
                column: "foldersId",
                principalTable: "Folders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Users_appUserId",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Folders_foldersId",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "MetaData");

            migrationBuilder.RenameColumn(
                name: "foldersId",
                table: "Photos",
                newName: "Foldersid");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_foldersId",
                table: "Photos",
                newName: "IX_Photos_Foldersid");

            migrationBuilder.RenameColumn(
                name: "appUserId",
                table: "Folders",
                newName: "AppUserid");

            migrationBuilder.RenameIndex(
                name: "IX_Folders_appUserId",
                table: "Folders",
                newName: "IX_Folders_AppUserid");

            migrationBuilder.AlterColumn<int>(
                name: "Foldersid",
                table: "Photos",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserid",
                table: "Folders",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Users_AppUserid",
                table: "Folders",
                column: "AppUserid",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Folders_Foldersid",
                table: "Photos",
                column: "Foldersid",
                principalTable: "Folders",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
