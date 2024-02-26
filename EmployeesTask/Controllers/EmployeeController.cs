using EmployeesTask.Context;
using EmployeesTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Composition.Convention;

namespace EmployeesTask.Controllers
{
    public class EmployeeController : Controller
    {private readonly EmployeeDbContext _employeeDbContext;
        public EmployeeController( EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

       

        [HttpGet]
        public async Task< IActionResult >Index()
        {
           var emp = await _employeeDbContext.Employees.ToListAsync();
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
           await _employeeDbContext.Employees.AddAsync(employee);
           await _employeeDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> View(int Id)
        {
            var emp = await _employeeDbContext.Employees.FirstOrDefaultAsync(  x => x.EmployeeId == Id);
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
            var employee = await _employeeDbContext.Employees.FindAsync(model.EmployeeId);
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
                await _employeeDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
                return RedirectToAction("Index");
        }
        [HttpPost]    
        
        public async Task<IActionResult>Delete(UpdateViewModel model)
        {
            var emp = await _employeeDbContext.Employees.FindAsync(model.EmployeeId);
            if(emp != null)
            {
                _employeeDbContext.Employees.Remove(emp);
                await _employeeDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
