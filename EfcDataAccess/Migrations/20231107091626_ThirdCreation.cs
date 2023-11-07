using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfcDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ThirdCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedditPosts_OwnerDto_Username1",
                table: "RedditPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_RedditPosts_Users_Username",
                table: "RedditPosts");

            migrationBuilder.DropTable(
                name: "OwnerDto");

            migrationBuilder.DropIndex(
                name: "IX_RedditPosts_Username1",
                table: "RedditPosts");

            migrationBuilder.DropColumn(
                name: "Username1",
                table: "RedditPosts");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "RedditPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RedditPosts_Users_Username",
                table: "RedditPosts",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedditPosts_Users_Username",
                table: "RedditPosts");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "RedditPosts",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Username1",
                table: "RedditPosts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "OwnerDto",
                columns: table => new
                {
                    Username = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerDto", x => x.Username);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RedditPosts_Username1",
                table: "RedditPosts",
                column: "Username1");

            migrationBuilder.AddForeignKey(
                name: "FK_RedditPosts_OwnerDto_Username1",
                table: "RedditPosts",
                column: "Username1",
                principalTable: "OwnerDto",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RedditPosts_Users_Username",
                table: "RedditPosts",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username");
        }
    }
}
