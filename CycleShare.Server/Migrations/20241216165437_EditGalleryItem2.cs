using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CycleShare.Server.Migrations
{
    /// <inheritdoc />
    public partial class EditGalleryItem2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryItem_Bike_BikeId",
                table: "GalleryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_GalleryItem_Review_ReviewId",
                table: "GalleryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_GalleryItem_User_UserId",
                table: "GalleryItem");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "GalleryItem",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ReviewId",
                table: "GalleryItem",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "BikeId",
                table: "GalleryItem",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryItem_Bike_BikeId",
                table: "GalleryItem",
                column: "BikeId",
                principalTable: "Bike",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryItem_Review_ReviewId",
                table: "GalleryItem",
                column: "ReviewId",
                principalTable: "Review",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryItem_User_UserId",
                table: "GalleryItem",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryItem_Bike_BikeId",
                table: "GalleryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_GalleryItem_Review_ReviewId",
                table: "GalleryItem");

            migrationBuilder.DropForeignKey(
                name: "FK_GalleryItem_User_UserId",
                table: "GalleryItem");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "GalleryItem",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReviewId",
                table: "GalleryItem",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BikeId",
                table: "GalleryItem",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryItem_Bike_BikeId",
                table: "GalleryItem",
                column: "BikeId",
                principalTable: "Bike",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryItem_Review_ReviewId",
                table: "GalleryItem",
                column: "ReviewId",
                principalTable: "Review",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryItem_User_UserId",
                table: "GalleryItem",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
