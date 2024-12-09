using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_DEMO.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "Id",
            //    table: "Categories",
            //    newName: "Category_Id");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Category_Id", "CategoryName", "DisplayOrder" },
                values: new object[] { 7, "SkinCare", 7 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Category_Id",
                keyValue: 7);

            migrationBuilder.RenameColumn(
                name: "Category_Id",
                table: "Categories",
                newName: "Id");
        }
    }
}
