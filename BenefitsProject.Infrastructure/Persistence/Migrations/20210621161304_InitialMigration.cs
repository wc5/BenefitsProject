using Microsoft.EntityFrameworkCore.Migrations;

namespace BenefitsProject.Infrastructure.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    SalaryPerAnnum = table.Column<decimal>(type: "TEXT", precision: 2, nullable: false),
                    PersonalBenefitCostPerAnnum = table.Column<decimal>(type: "TEXT", precision: 2, nullable: false),
                    HasDiscountedPersonalBenefitCost = table.Column<bool>(type: "INTEGER", nullable: false),
                    TotalBenefitCostPerAnnum = table.Column<decimal>(type: "TEXT", precision: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dependents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    PersonalBenefitCostPerAnnum = table.Column<decimal>(type: "TEXT", precision: 2, nullable: false),
                    HasDiscountedPersonalBenefitCost = table.Column<bool>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_Dependents_EmployeeId",
                table: "Dependents",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dependents");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
