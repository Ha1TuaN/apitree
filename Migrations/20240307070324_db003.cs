using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apitree.Migrations
{
    /// <inheritdoc />
    public partial class db003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TreeName",
                table: "Tree",
                newName: "title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "title",
                table: "Tree",
                newName: "TreeName");
        }
    }
}
