using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

//define employee model


public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Salary { get; set; }
    public DateTime JoiningDate { get; set; }
}

// Define the DbContext
public class EmployeeDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure the database connection
        optionsBuilder.UseSqlServer("YourConnectionString");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Initialize the DbContext
        using (var context = new EmployeeDbContext())
        {
            // Fetch all employees
            var allEmployees = context.Employees.ToList();

            // Fetch individual employee using Id
            var employee = context.Employees.Find(1);

            // Update employee details
            if (employee != null)
            {
                employee.Name = "Updated Name";
                context.SaveChanges();
            }

            // Add new employee
            var newEmployee = new Employee
            {
                Name = "New Employee",
                Salary = 50000,
                JoiningDate = DateTime.Now
            };
            context.Employees.Add(newEmployee);
            context.SaveChanges();

            // Delete existing employee
            var employeeToDelete = context.Employees.Find(2);
            if (employeeToDelete != null)
            {
                context.Employees.Remove(employeeToDelete);
                context.SaveChanges();
            }

            // Filter employees by salary
            var filteredEmployees = context.Employees.Where(e => e.Salary > 60000).ToList();
        }
    }
}
