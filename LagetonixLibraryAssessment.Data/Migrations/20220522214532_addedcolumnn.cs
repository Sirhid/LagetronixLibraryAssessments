using Microsoft.EntityFrameworkCore.Migrations;

namespace LagetonixLibraryAssessment.Data.Migrations
{
    public partial class addedcolumnn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "LastUpdatedByUserID",
                table: "Books",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedByUserID",
                table: "Books");
        }
    }
}
