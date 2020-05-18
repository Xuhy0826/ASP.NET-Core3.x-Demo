using System;
using ASP.NET_Core3.x_WebApi_Demo.Entities;
using Demo.Dto.Enum;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core3.x_WebApi_Demo.Data
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .Property(x => x.Name).IsRequired().HasMaxLength(100);

            modelBuilder.Entity<Employee>()
                .Property(x => x.EmployeeNo).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Employee>()
                .Property(x => x.Name).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Department)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Department>().HasData(
                new Department
                {
                    Id = Guid.Parse("aa2ee05c-029b-4830-be6e-44df5923716c"),
                    Name = "财务部",
                    Code = "KJ"
                },
                new Department
                {
                    Id = Guid.Parse("6fb600c1-9011-4fd7-9234-881379716440"),
                    Name = "人事部",
                    Code = "RS"
                },
                new Department
                {
                    Id = Guid.Parse("5efc910b-2f45-43df-afae-620d40542853"),
                    Name = "研发部",
                    Code = "YF"
                },
                new Department
                {
                    Id = Guid.Parse("bbdee09c-089b-4d30-bece-44df59237100"),
                    Name = "后勤部",
                    Code = "HQ"
                },
                new Department
                {
                    Id = Guid.Parse("6fb600c1-9011-4fd7-9234-881379716400"),
                    Name = "管理部",
                    Code = "GL"
                },
                new Department
                {
                    Id = Guid.Parse("6eaa53e1-9011-4fd7-9234-881379716400"),
                    Name = "宣传部",
                    Code = "XC"
                });

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = Guid.Parse("4b501cb3-d168-4cc0-b375-48fb33f318a4"),
                    DepartmentId = Guid.Parse("5efc910b-2f45-43df-afae-620d40542853"),
                    BirthDay = new DateTime(1976, 1, 2),
                    EmployeeNo = "52314",
                    Name = "张文远",
                    Gender = Gender.Male
                },
                new Employee
                {
                    Id = Guid.Parse("7eaa532c-1be5-472c-a738-94fd26e5fad6"),
                    DepartmentId = Guid.Parse("6fb600c1-9011-4fd7-9234-881379716400"),
                    BirthDay = new DateTime(1981, 12, 5),
                    EmployeeNo = "X5412",
                    Name = "曹孟德",
                    Gender = Gender.Male
                },
                new Employee
                {
                    Id = Guid.Parse("72457e73-ea34-4e02-b575-8d384e82a481"),
                    DepartmentId = Guid.Parse("6eaa53e1-9011-4fd7-9234-881379716400"),
                    BirthDay = new DateTime(1986, 11, 4),
                    EmployeeNo = "G3003",
                    Name = "孙尚香",
                    Gender = Gender.Female
                },
                new Employee
                {
                    Id = Guid.Parse("7644b71d-d74e-43e2-ac32-8cbadd7b1c3a"),
                    DepartmentId = Guid.Parse("5efc910b-2f45-43df-afae-620d40542853"),
                    BirthDay = new DateTime(1977, 4, 6),
                    EmployeeNo = "45097",
                    Name = "关云长",
                    Gender = Gender.Male
                },
                new Employee
                {
                    Id = Guid.Parse("679dfd33-32e4-4393-b061-f7abb8956f53"),
                    DepartmentId = Guid.Parse("6fb600c1-9011-4fd7-9234-881379716440"),
                    BirthDay = new DateTime(1967, 1, 24),
                    EmployeeNo = "76009",
                    Name = "鲁子敬",
                    Gender = Gender.Male
                },
                new Employee
                {
                    Id = Guid.Parse("1861341e-b42b-410c-ae21-cf11f36fc574"),
                    DepartmentId = Guid.Parse("6fb600c1-9011-4fd7-9234-881379716400"),
                    BirthDay = new DateTime(1987, 3, 8),
                    EmployeeNo = "34404",
                    Name = "诸葛孔明",
                    Gender = Gender.Male
                }, 
                new Employee
                {
                    Id = Guid.Parse("1896341e-b45b-410c-ae21-cf11f16fc574"),
                    DepartmentId = Guid.Parse("5efc910b-2f45-43df-afae-620d40542853"),
                    BirthDay = new DateTime(1977, 6, 8),
                    EmployeeNo = "521404",
                    Name = "赵子龙",
                    Gender = Gender.Male
                });
        }
    }
}
