using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_sem4_api.Migrations
{
    /// <inheritdoc />
    public partial class f2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tableId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_tableId",
                table: "Notifications",
                column: "tableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Restaurant_Tables_tableId",
                table: "Notifications",
                column: "tableId",
                principalTable: "Restaurant_Tables",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Restaurant_Tables_tableId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_tableId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "tableId",
                table: "Notifications");
        }
    }
}
