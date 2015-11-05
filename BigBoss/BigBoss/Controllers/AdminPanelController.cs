using BigBoss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BigBoss.Controllers
{
    public class AdminPanelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminPanel
        public ActionResult Index()
        {
            return View();
        }

        //----------- Category controll -----------

        public ActionResult CategoryIndex()
        {
            var lista = db.Category.ToArray();
            return View(lista);
        }

        public ActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CategoryCreate(CategoryModel catModel)
        {
            if (ModelState.IsValid)
            {
                db.Category.Add(catModel);
                await db.SaveChangesAsync();
                TempData["poruka"] = "Category saved!";
                return RedirectToAction("CategoryIndex");
            }
            return View();
        }

    }
}