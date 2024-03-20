using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SellDomains",
                columns: table => new
                {
                    DomainId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DomainName = table.Column<string>(nullable: true),
                    AvalibilityStatus = table.Column<bool>(nullable: false),
                    LastDateOfCheck = table.Column<DateTime>(nullable: false),
                    DateOfExpire = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellDomains", x => x.DomainId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SellDomains");
        }
    }
}
