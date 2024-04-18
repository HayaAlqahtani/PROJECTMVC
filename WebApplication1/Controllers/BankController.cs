using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
//using System.Linq;
//using System.Collections.Generic;

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
            return View(Branches);
        }

        [HttpPost]
        public IActionResult Create(NewBranchForm newBranchform)
        {
            if (ModelState.IsValid)
            {
                var newBranch = new BankBranch
                {
                    Name = newBranchform.Name,
                    Location = newBranchform.Location,
                    BranchManager = newBranchform.BranchManager,
                    EmployeeCount = newBranchform.EmployeeCount,


                };
                Branches.Add(newBranch);
                return RedirectToAction("Index");
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
            var bankBranch = Branches.FirstOrDefault(b => b.Id == id);
            if (bankBranch == null)
            {
                return RedirectToAction("Index");
            }
            return View(bankBranch);
        }
    }
}
