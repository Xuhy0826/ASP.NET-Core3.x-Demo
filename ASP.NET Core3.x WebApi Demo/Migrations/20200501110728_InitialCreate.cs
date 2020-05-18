using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP.NET_Core3.x_WebApi_Demo.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DepartmentId = table.Column<Guid>(nullable: false),
                    EmployeeNo = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("aa2ee05c-029b-4830-be6e-44df5923716c"), "KJ", "财务部" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716440"), "RS", "人事部" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("5efc910b-2f45-43df-afae-620d40542853"), "YF", "研发部" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("bbdee09c-089b-4d30-bece-44df59237100"), "HQ", "后勤部" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716400"), "GL", "管理部" });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[] { new Guid("6eaa53e1-9011-4fd7-9234-881379716400"), "XC", "宣传部" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDay", "DepartmentId", "EmployeeNo", "Gender", "Name" },
                values: new object[] { new Guid("679dfd33-32e4-4393-b061-f7abb8956f53"), new DateTime(1967, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6fb600c1-9011-4fd7-9234-881379716440"), "76009", 1, "鲁子敬" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDay", "DepartmentId", "EmployeeNo", "Gender", "Name" },
                values: new object[] { new Guid("4b501cb3-d168-4cc0-b375-48fb33f318a4"), new DateTime(1976, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5efc910b-2f45-43df-afae-620d40542853"), "52314", 1, "张文远" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDay", "DepartmentId", "EmployeeNo", "Gender", "Name" },
                values: new object[] { new Guid("7644b71d-d74e-43e2-ac32-8cbadd7b1c3a"), new DateTime(1977, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5efc910b-2f45-43df-afae-620d40542853"), "45097", 1, "关云长" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDay", "DepartmentId", "EmployeeNo", "Gender", "Name" },
                values: new object[] { new Guid("1896341e-b45b-410c-ae21-cf11f16fc574"), new DateTime(1977, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("5efc910b-2f45-43df-afae-620d40542853"), "521404", 1, "赵子龙" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDay", "DepartmentId", "EmployeeNo", "Gender", "Name" },
                values: new object[] { new Guid("7eaa532c-1be5-472c-a738-94fd26e5fad6"), new DateTime(1981, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6fb600c1-9011-4fd7-9234-881379716400"), "X5412", 1, "曹孟德" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDay", "DepartmentId", "EmployeeNo", "Gender", "Name" },
                values: new object[] { new Guid("1861341e-b42b-410c-ae21-cf11f36fc574"), new DateTime(1987, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6fb600c1-9011-4fd7-9234-881379716400"), "34404", 1, "诸葛孔明" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BirthDay", "DepartmentId", "EmployeeNo", "Gender", "Name" },
                values: new object[] { new Guid("72457e73-ea34-4e02-b575-8d384e82a481"), new DateTime(1986, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6eaa53e1-9011-4fd7-9234-881379716400"), "G3003", 0, "孙尚香" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                table: "Employees",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
