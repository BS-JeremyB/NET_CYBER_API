using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NET_CYBER_API.DAL.Migrations
{
    public partial class data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "utilisateurs",
                columns: new[] { "Id", "Email", "Nom", "Password", "Prenom", "Role" },
                values: new object[] { 1, "doejohn@mail.be", "doe", "Test1234=", "john", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "utilisateurs",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
