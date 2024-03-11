using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apitree.Migrations
{
    /// <inheritdoc />
    public partial class db001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase(
                oldCollation: "en_US_CI_AS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase(
                collation: "en_US_CI_AS");
        }
    }
}
