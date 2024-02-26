﻿using EmployeesTask.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeesTask.Context
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext()
        {

        }
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)

        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}

