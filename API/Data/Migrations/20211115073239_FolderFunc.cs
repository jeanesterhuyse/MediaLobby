using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class FolderFunc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Foldersid",
                table: "Photos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Folders",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    folderName = table.Column<string>(type: "TEXT", nullable: true),
                    AppUserid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Folders_Users_AppUserid",
                        column: x => x.AppUserid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_Foldersid",
                table: "Photos",
                column: "Foldersid");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_AppUserid",
                table: "Folders",
                column: "AppUserid");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Folders_Foldersid",
                table: "Photos",
                column: "Foldersid",
                principalTable: "Folders",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Folders_Foldersid",
                table: "Photos");

            migrationBuilder.DropTable(
                name: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Photos_Foldersid",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Foldersid",
                table: "Photos");
        }
    }
}
