using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BenefitsApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Salary = table.Column<decimal>(type: "TEXT", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dependents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dependents_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Benefit = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    EnrollmentId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discounts_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "Salary", "StartDate" },
                values: new object[] { new Guid("95fe2082-7726-4cb3-bcc9-fc27f0135853"), "Anthony", "Soprano", 52000m, new DateTime(2021, 4, 15, 8, 7, 53, 489, DateTimeKind.Local).AddTicks(5410) });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "FirstName", "LastName", "Salary", "StartDate" },
                values: new object[] { new Guid("9718d5b6-9a69-451a-adc7-d6c763976e24"), "Christopher", "Moltisanti", 52000m, new DateTime(2021, 5, 15, 8, 7, 53, 502, DateTimeKind.Local).AddTicks(1290) });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "EmployeeId", "FirstName", "LastName" },
                values: new object[] { new Guid("c973769f-6bbf-47b1-8504-64929d6d50db"), new Guid("95fe2082-7726-4cb3-bcc9-fc27f0135853"), "Anthony Jr.", "Soprano" });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "EmployeeId", "FirstName", "LastName" },
                values: new object[] { new Guid("6fe0f280-a56e-4973-8ef5-8fe623889226"), new Guid("95fe2082-7726-4cb3-bcc9-fc27f0135853"), "Meadow", "Soprano" });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "EmployeeId", "FirstName", "LastName" },
                values: new object[] { new Guid("4e215c2b-4631-476d-8092-158640372295"), new Guid("95fe2082-7726-4cb3-bcc9-fc27f0135853"), "Carmela", "Soprano" });

            migrationBuilder.InsertData(
                table: "Dependents",
                columns: new[] { "Id", "EmployeeId", "FirstName", "LastName" },
                values: new object[] { new Guid("c6ff219f-3f4e-4d8e-b3d2-fe03fb9066d3"), new Guid("9718d5b6-9a69-451a-adc7-d6c763976e24"), "Kelli", "Moltisanti" });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "Benefit", "EmployeeId" },
                values: new object[] { new Guid("40e6437f-3093-4280-8c12-c5f74f547a52"), "GenericBenefit", new Guid("95fe2082-7726-4cb3-bcc9-fc27f0135853") });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "Id", "Benefit", "EmployeeId" },
                values: new object[] { new Guid("e303f29c-73b5-47e9-bc58-2a72199ecacf"), "GenericBenefit", new Guid("9718d5b6-9a69-451a-adc7-d6c763976e24") });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "EnrollmentId", "Name" },
                values: new object[] { new Guid("007c9870-cf16-443a-9438-f0128016ac95"), new Guid("40e6437f-3093-4280-8c12-c5f74f547a52"), "NameDiscount" });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "EnrollmentId", "Name" },
                values: new object[] { new Guid("c9a49c7a-eea8-40c5-be27-36d60bb0d031"), new Guid("e303f29c-73b5-47e9-bc58-2a72199ecacf"), "NameDiscount" });

            migrationBuilder.CreateIndex(
                name: "IX_Dependents_EmployeeId",
                table: "Dependents",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_EnrollmentId",
                table: "Discounts",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_EmployeeId",
                table: "Enrollments",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dependents");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
