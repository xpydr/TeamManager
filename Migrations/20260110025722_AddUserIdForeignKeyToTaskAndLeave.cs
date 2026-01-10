using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManager.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdForeignKeyToTaskAndLeave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AssignedUserId",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Leaves",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedUserId",
                table: "Tasks",
                column: "AssignedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_UserId",
                table: "Leaves",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_Users_UserId",
                table: "Leaves",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_AssignedUserId",
                table: "Tasks",
                column: "AssignedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_Users_UserId",
                table: "Leaves");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_AssignedUserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_AssignedUserId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Leaves_UserId",
                table: "Leaves");

            migrationBuilder.AlterColumn<string>(
                name: "AssignedUserId",
                table: "Tasks",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Leaves",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
