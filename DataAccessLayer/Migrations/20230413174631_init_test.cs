using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class init_test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    UID = table.Column<Guid>(type: "Guid", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Patronymic = table.Column<string>(type: "TEXT", nullable: false),
                    Telephone = table.Column<string>(type: "TEXT", nullable: false),
                    Passport = table.Column<string>(type: "TEXT", nullable: false),
                    DateChange = table.Column<long>(type: "Int64", nullable: false),
                    FieldChanged = table.Column<int>(type: "Int32", nullable: false),
                    TypeChanged = table.Column<int>(type: "Int32", nullable: false),
                    ChangingWorker = table.Column<string>(type: "Int32", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ClientId = table.Column<Guid>(type: "Guid", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    DateOpen = table.Column<long>(type: "Int64", nullable: false),
                    Procent = table.Column<int>(type: "Double", nullable: false),
                    CountMonetaryUnit = table.Column<decimal>(type: "Decimal", nullable: false),
                    TypeAccount = table.Column<int>(type: "Int32", nullable: false),
                    IsLock = table.Column<bool>(type: "Boolean", nullable: false),
                    IsClose = table.Column<bool>(type: "Boolean", nullable: false),
                    UID = table.Column<Guid>(type: "Guid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Customers",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
