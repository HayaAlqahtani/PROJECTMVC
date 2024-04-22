using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BankController : Controller
    {
        private static List<BankBranch> Branches = new List<BankBranch>
        {
            new BankBranch {Id=0,Name= "JaberBranch", Location="https://maps.app.goo.gl/skMjkovTH4YndNXc8?g_st=iw", BranchManager="nawaf", EmployeeCount= 50},
            new BankBranch {Id=1, Name= "QasrBranch", Location="https://maps.app.goo.gl/bbip7bmt6hna578z7?g_st=iw", BranchManager="talal", EmployeeCount= 70},
            new BankBranch {Id=2,Name= "KFHAuto", Location="https://maps.app.goo.gl/Ly4oSKPJvMU4SBgq6?g_st=iw", BranchManager="haya", EmployeeCount= 30}



        };

        public IActionResult Index()
        {

            var context = new BankContext();

            return View(context.BankBranches.ToList());
        }

        [HttpPost]
        public IActionResult Create(NewBranchForm newBranchform)
        {
            if (ModelState.IsValid)
            {
                using (var context = new BankContext())
                {
                    var newBranch = new BankBranch
                    {
                        Name = newBranchform.Name,
                        Location = newBranchform.Location,
                        BranchManager = newBranchform.BranchManager,
                        EmployeeCount = newBranchform.EmployeeCount,

                    };
                    context.BankBranches.Add(newBranch);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return View(newBranchform);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            using (var context = new BankContext())
            {
                var bank = context.BankBranches.Include(r=>r.Employees).SingleOrDefault(r=> r.Id == id);
                if (bank == null)
                {
                    return RedirectToAction("Index");
                }
                return View(bank);
            }

        }
        [HttpPost]
        public IActionResult Edit(int id, NewBranchForm newBranchForm)
        {
            using (var context = new BankContext())
            {
                var bank = context.BankBranches.Find(id);
                if (bank != null)
                {
                    bank.Location = newBranchForm.Location;
                    bank.BranchManager = newBranchForm.BranchManager;
                    bank.EmployeeCount = newBranchForm.EmployeeCount;
                    bank.Name = newBranchForm.Name;
                    context.SaveChanges();
                    return RedirectToAction("Index");

                }
            }
            return View();
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            using (var context = new BankContext())
            {
                var bank = context.BankBranches.Find(id);
                if (bank == null)
                {
                    return NotFound();
                }
                var form = new NewBranchForm();
                form.Name = bank.Name;
                form.BranchManager = bank.BranchManager;
                form.EmployeeCount = bank.EmployeeCount;
                form.Location = bank.Location;
                return View(form);
            }

        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            using (var context = new BankContext())
            {
                var bank = context.BankBranches.Find(id);
                if (bank == null)
                {
                    return RedirectToAction("Index");
                }
                return View(bank);
            }
        }
        [HttpPost]
        public IActionResult Delete(int id, BankBranch bankBranch)
        {
            using (var context = new BankContext())
            {
                var bank = context.BankBranches.Find(id);
                if (bank == null)
                {
                    return RedirectToAction("Index");

                }
                context.BankBranches.Remove(bank);
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult AddEmployee(int Id, AddEmployeeForm addEmployeeForm)
        {

            if (ModelState.IsValid)
            {
                var context = new BankContext();
                var branch = context.BankBranches.Find(Id);
                var newEmployee = new Employee();

                newEmployee.Name = addEmployeeForm.Name;
                newEmployee.CivilId = addEmployeeForm.CivilId;
                newEmployee.Position = addEmployeeForm.Position;


                branch.Employees.Add(newEmployee);
                context.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(addEmployeeForm);
        }

        [HttpGet]
        public IActionResult AddEmployee(int Id)
        {
            return View();
        }

    }

}



