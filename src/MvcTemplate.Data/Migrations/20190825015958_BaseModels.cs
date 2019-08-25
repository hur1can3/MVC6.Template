using Microsoft.EntityFrameworkCore.Migrations;

namespace MvcTemplate.Data.Migrations
{
    public partial class BaseModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactTypeId",
                table: "Contact",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressTypeId",
                table: "Address",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contact_ContactTypeId",
                table: "Contact",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_AddressTypeId",
                table: "Address",
                column: "AddressTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_AddressType_AddressTypeId",
                table: "Address",
                column: "AddressTypeId",
                principalTable: "AddressType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_ContactType_ContactTypeId",
                table: "Contact",
                column: "ContactTypeId",
                principalTable: "ContactType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_AddressType_AddressTypeId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_ContactType_ContactTypeId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_ContactTypeId",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Address_AddressTypeId",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "ContactTypeId",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "AddressTypeId",
                table: "Address");
        }
    }
}
