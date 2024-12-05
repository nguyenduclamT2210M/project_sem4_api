using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_sem4_api.Migrations
{
    /// <inheritdoc />
    public partial class f4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Events_eventId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "eventId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Events_eventId",
                table: "Orders",
                column: "eventId",
                principalTable: "Events",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Events_eventId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "eventId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Events_eventId",
                table: "Orders",
                column: "eventId",
                principalTable: "Events",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
