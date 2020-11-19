using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManagement.Migrations
{
    public partial class removerelUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelUserId",
                table: "UserBooksRecords");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
