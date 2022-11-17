using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDatabaseApplication_A11.Migrations
{
    public partial class InsertOccupations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Migrations", "Data", @"3-InsertOccupations.sql");
            migrationBuilder.Sql(File.ReadAllText(sqlFile));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from occupations");
        }
    }
}
