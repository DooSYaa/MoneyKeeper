using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyKeeper.Migrations
{
    /// <inheritdoc />
    public partial class stringId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Money_AspNetUsers_AppUserId1",
                table: "Money");

            migrationBuilder.DropIndex(
                name: "IX_Money_AppUserId1",
                table: "Money");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Money");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Money",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Money_AppUserId",
                table: "Money",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Money_AspNetUsers_AppUserId",
                table: "Money",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Money_AspNetUsers_AppUserId",
                table: "Money");

            migrationBuilder.DropIndex(
                name: "IX_Money_AppUserId",
                table: "Money");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Money",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "Money",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Money_AppUserId1",
                table: "Money",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Money_AspNetUsers_AppUserId1",
                table: "Money",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
