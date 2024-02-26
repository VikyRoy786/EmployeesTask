using EmployeesTask.Context;
using EmployeesTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Composition.Convention;

namespace EmployeesTask.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeController( EmployeeDbContext employeeDbContext)
        {
            EmployeeDbContext = employeeDbContext;
        }

        public EmployeeDbContext EmployeeDbContext { get; }

        [HttpGet]
        public async Task< IActionResult >Index()
        {
           var emp = await EmployeeDbContext.Employees.ToListAsync();
            return View(emp);

        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployee addEmployee) 
        {
            var employee = new Employee()
            {
                FirstName = addEmployee.FirstName,
                LastName = addEmployee.LastName,
                DateOfBirth = addEmployee.DateOfBirth,
                City = addEmployee.City,
                State = addEmployee.State,
                Country = addEmployee.Country,
                Qualification = addEmployee.Qualification,
                EmailAddress = addEmployee.EmailAddress,
                PhoneNo = addEmployee.PhoneNo,
            };
           await EmployeeDbContext.Employees.AddAsync(employee);
           await EmployeeDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> View(int Id)
        {
            var emp = await EmployeeDbContext.Employees.FirstOrDefaultAsync(  x => x.EmployeeId == Id);
            if (emp != null)
            {
                var view = new UpdateViewModel()
                {
                    EmployeeId = emp.EmployeeId,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    DateOfBirth = emp.DateOfBirth,
                    City = emp.City,
                    State = emp.State,
                    Country = emp.Country,
                    Qualification = emp.Qualification,
                    EmailAddress = emp.EmailAddress,
                    PhoneNo = emp.PhoneNo,
                };
                return await Task.Run(() => View("View",view));
            }
            return RedirectToAction("Index");
            
            
        }
        [HttpPost]
        public async Task<IActionResult>View (UpdateViewModel model)
        {
            var employee = await EmployeeDbContext.Employees.FindAsync(model.EmployeeId);
                if(employee != null)
            {
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName; 
                employee.DateOfBirth = model.DateOfBirth;
                employee.City = model.City;
                employee.State = model.State;
                employee.Country = model.Country;
                employee.Qualification = model.Qualification;
                employee.EmailAddress = model.EmailAddress;
                employee.PhoneNo = model.PhoneNo;
                await EmployeeDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
                return RedirectToAction("Index");
        }
        [HttpPost]    
        
        public async Task<IActionResult>Delete(UpdateViewModel model)
        {
            var emp = await EmployeeDbContext.Employees.FindAsync(model.EmployeeId);
            if(emp != null)
            {
                EmployeeDbContext.Employees.Remove(emp);
                await EmployeeDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
