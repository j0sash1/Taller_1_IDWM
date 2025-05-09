using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taller1.Src.Data.Migrations
{
    /// <inheritdoc />
    public partial class NombreDeTuMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddres_AspNetUsers_UserId",
                table: "ShippingAddres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingAddres",
                table: "ShippingAddres");

            migrationBuilder.RenameTable(
                name: "ShippingAddres",
                newName: "ShippingAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingAddres_UserId",
                table: "ShippingAddresses",
                newName: "IX_ShippingAddresses_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddresses_AspNetUsers_UserId",
                table: "ShippingAddresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShippingAddresses_AspNetUsers_UserId",
                table: "ShippingAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses");

            migrationBuilder.RenameTable(
                name: "ShippingAddresses",
                newName: "ShippingAddres");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingAddresses_UserId",
                table: "ShippingAddres",
                newName: "IX_ShippingAddres_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingAddres",
                table: "ShippingAddres",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingAddres_AspNetUsers_UserId",
                table: "ShippingAddres",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}