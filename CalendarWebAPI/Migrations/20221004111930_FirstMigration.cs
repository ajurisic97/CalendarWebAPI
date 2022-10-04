using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalendarWebAPI.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Catalog");

            migrationBuilder.EnsureSchema(
                name: "Person");

            migrationBuilder.CreateTable(
                name: "Applications",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });





            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });









            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Catalog",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role",
                        column: x => x.RoleId,
                        principalSchema: "Catalog",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User",
                        column: x => x.UserId,
                        principalSchema: "Catalog",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });



            migrationBuilder.CreateTable(
                name: "ApplicationEvents",
                schema: "Catalog",
                columns: table => new
                {
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationEvents", x => new { x.ApplicationId, x.EventId });
                    table.ForeignKey(
                        name: "FK_ApplicationEvent_Application",
                        column: x => x.ApplicationId,
                        principalSchema: "Catalog",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationEvent_Event",
                        column: x => x.EventId,
                        principalSchema: "Catalog",
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });



            

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationEvents_EventId",
                schema: "Catalog",
                table: "ApplicationEvents",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Catalog",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationEvents",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "CalendarDate",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Holidays",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "PersonalIncomes",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "SchedulerItems",
                schema: "Person");

            migrationBuilder.DropTable(
                name: "Shifts",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "TaxInTaxGroups",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "WorkingDays",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Applications",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Confessions",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Schedulers");

            migrationBuilder.DropTable(
                name: "CalendarItems",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Taxes",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "TaxGroups",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Events",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "People",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Calendar",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Recurrings",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Creators",
                schema: "Catalog");
        }
    }
}
