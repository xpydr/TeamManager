using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManager.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRoleEnumAndConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_User_Role_Valid",
                table: "Users",
                sql: "[Role] IN ('Employee', 'Admin')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_User_Role_Valid",
                table: "Users");
        }
    }
}
