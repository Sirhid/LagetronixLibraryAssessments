using Microsoft.EntityFrameworkCore.Migrations;

namespace LagetonixLibraryAssessment.Data.Migrations
{
    public partial class newupdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Catergories_catergoriesCategoryId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_catergoriesCategoryId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "catergoriesCategoryId",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "catergoriesCategoryId",
                table: "Books",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_catergoriesCategoryId",
                table: "Books",
                column: "catergoriesCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Catergories_catergoriesCategoryId",
                table: "Books",
                column: "catergoriesCategoryId",
                principalTable: "Catergories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
