using BigBoss.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
                TempData["success_msg"] = "Category saved!";
                return RedirectToAction("CategoryIndex");
            }
            return View();
        }

        public ActionResult CategoryEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cat = db.Category.Where(c => c.Id == id).FirstOrDefault();
            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CategoryEdit([Bind(Include = "Id, nameCategory, descriptionCategory")] CategoryModel cat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["success_msg"] = "Category saved!";
                return RedirectToAction("CategoryIndex");

            }
            return View();
        }

        public ActionResult CategoryDelete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cat = db.Category.Where(c => c.Id == id).FirstOrDefault();

            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CategoryDelete(int id)
        {
            var cat = db.Category.Where(c => c.Id == id).FirstOrDefault();
            db.Category.Remove(cat);
            await db.SaveChangesAsync();
            TempData["success_msg"] = "Category deleted!";
            return View("CategoryIndex");
        }

        //----------- Project controll ------------

        public ActionResult ProjectIndex()
        {
            var projectList = db.Project.ToList();
            return View(projectList);
        }

        public ActionResult ProjectCreate()
        {
            ViewBag.CatModel = new SelectList(db.Category.ToList(), "Id", "nameCategory");
            return View();
        }

     /*   [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProjectCreate(ProjectModel modelProj, int idCat)
        {

        }

    */
    }
}