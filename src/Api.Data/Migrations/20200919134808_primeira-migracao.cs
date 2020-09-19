using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class primeiramigracao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: true),
                    UpdateAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    Role = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreateAt", "Email", "Name", "Role", "UpdateAt" },
                values: new object[] { new Guid("eca959fb-e684-4c13-97df-27dfb887b3b3"), new DateTime(2020, 9, 19, 10, 48, 8, 336, DateTimeKind.Local).AddTicks(8500), "adm@adm.com", "Administrador", "Administrator", new DateTime(2020, 9, 19, 10, 48, 8, 338, DateTimeKind.Local).AddTicks(6359) });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreateAt", "Email", "Name", "Role", "UpdateAt" },
                values: new object[] { new Guid("45ba8fc0-db73-43e8-a2d4-f0115f9eb5f0"), new DateTime(2020, 9, 19, 10, 48, 8, 338, DateTimeKind.Local).AddTicks(7290), "user@example.com", "Cliente", "Client", new DateTime(2020, 9, 19, 10, 48, 8, 338, DateTimeKind.Local).AddTicks(7312) });

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
