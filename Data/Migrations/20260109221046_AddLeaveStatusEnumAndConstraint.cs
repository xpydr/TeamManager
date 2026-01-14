using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManager.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaveStatusEnumAndConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Leave_Status_Valid",
                table: "Leaves",
                sql: "[Status] IN ('Pending', 'Approved', 'Denied')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Leave_Status_Valid",
                table: "Leaves");
        }
    }
}
