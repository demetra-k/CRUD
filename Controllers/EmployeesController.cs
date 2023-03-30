namespace CRUDApplication.Controllers
{
    using CRUDApplication.Data;
    using CRUDApplication.Models;
    using CRUDApplication.Models.Domain;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class EmployeesController : Controller
    {
        private readonly MVCDdContext mvcDdContext;

        public EmployeesController(MVCDdContext mvcDdContext)
        {
            this.mvcDdContext = mvcDdContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvcDdContext.Employees.ToListAsync();
            return View(employees);

        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeVM addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth
            };

            await mvcDdContext.Employees.AddAsync(employee);
            await mvcDdContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var employee = await mvcDdContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeVM()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth

                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeVM model)
        {
            var employee = await mvcDdContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await mvcDdContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeVM model)
        {
            var employee = await mvcDdContext.Employees.FindAsync(model.Id);

            if(employee != null)
            {
                mvcDdContext.Employees.Remove(employee);
                await mvcDdContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
