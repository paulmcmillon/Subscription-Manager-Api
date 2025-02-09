using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subscriptions.Data.Migrations
{
    /// <inheritdoc />
    public partial class _111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_SubscriptionTypes_SubscriptionTypeId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "SusbcriptionTypeId",
                table: "Features");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriptionTypeId",
                table: "Features",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Features_SubscriptionTypes_SubscriptionTypeId",
                table: "Features",
                column: "SubscriptionTypeId",
                principalTable: "SubscriptionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_SubscriptionTypes_SubscriptionTypeId",
                table: "Features");

            migrationBuilder.AlterColumn<int>(
                name: "SubscriptionTypeId",
                table: "Features",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "SusbcriptionTypeId",
                table: "Features",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Features_SubscriptionTypes_SubscriptionTypeId",
                table: "Features",
                column: "SubscriptionTypeId",
                principalTable: "SubscriptionTypes",
                principalColumn: "Id");
        }
    }
}
