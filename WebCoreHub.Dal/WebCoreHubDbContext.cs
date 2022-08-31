using Microsoft.EntityFrameworkCore;
using WebCoreHub.Models;

namespace WebCoreHub.Dal
{
    public class WebCoreHubDbContext : DbContext
    {
        public WebCoreHubDbContext()
        {

        }

        public WebCoreHubDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Data Source = localhost; Initial Catalog = WebCoreApiDb; Integrated Security = true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee() {  EmployeeId = 1, EmployeeName = "John Doe", Address = "East Blue", City = "Cebu", Country = "Ph", Zipcode = "8710", Phone = "09979232212", Email = "john.doe@mailinato.com", Skillsets = "DBA", Avatar = "/images/john-doe.png"},
                new Employee() {  EmployeeId = 2, EmployeeName = "Jane Doe", Address = "West Blue", City = "Cebu", Country = "Ph", Zipcode = "9900", Phone = "09979217813", Email = "jane.doe@mailinato.com", Skillsets = "Secretary", Avatar = "/images/jane-doe.png"},
                new Employee() {  EmployeeId = 3, EmployeeName = "Joe Doe", Address = "North Blue", City = "Cebu", Country = "Ph", Zipcode = "7091", Phone = "09979213663", Email = "joe.doe@mailinato.com", Skillsets = "Programmer", Avatar = "/images/joe-doe.png" }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role() { RoleId = 1, RoleName = "Employee", RoleDescription = "Employee of WebCoreHub Organization"},
                new Role() { RoleId = 2, RoleName = "HR", RoleDescription = "HR of WebCoreHub Organization"}
            );
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }
    }
}