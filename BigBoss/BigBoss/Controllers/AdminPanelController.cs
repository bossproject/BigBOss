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
            var projectList = db.Project.Include(c => c.categoryMod).ToList();
            return View(projectList);
        }

        public ActionResult ProjectCreate()
        {
            ViewBag.CatModel = new SelectList(db.Category.ToList(), "Id", "nameCategory");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProjectCreate(ProjectModel modelProj, int? categoryMod)
        {

            modelProj.Id = Guid.NewGuid().ToString();
            modelProj.categoryMod = db.Category.Where(c => c.Id == categoryMod).FirstOrDefault();
            modelProj.moneyWithCommission = (modelProj.money / 100) * 104;
            modelProj.moneyRaised = 0;
            modelProj.numberOfDonations = 0;
            //       if (ModelState.IsValid)
            //       {
            db.Project.Add(modelProj);
            await db.SaveChangesAsync();
            return RedirectToAction("ProjectIndex");
            //       }
            //       return View();
        }

        public async Task<ActionResult> ProjectEdit(string id)
        {
            ProjectModel mod = await db.Project.FindAsync(id);
            //    var lista = new SelectList(db.Category.ToArray(), "Id", "nameCategory");
            //    ViewBag.CatModel = lista;
            ProjectEditViewModel noviMod = new ProjectEditViewModel()
            {
                listaKaregorija = db.Category.ToList(),
                Id = mod.Id,
                catMod = mod.categoryMod,
                nameProject = mod.nameProject,
                additionalInfo = mod.additionalInfo,
                descProject = mod.descProject,
                money = mod.money,
                moneyRaised = mod.moneyRaised,
                moneyWithCommission = mod.moneyWithCommission,
                numberOfDonations = mod.numberOfDonations,
                tagsProject = mod.tagsProject
            };

            // model.OrderTemplates = new SelectList(db.OrderTemplates, "OrderTemplateId", "OrderTemplateName", 1);
            return View(noviMod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProjectEdit([Bind(Include = "Id, nameProject, descProject, additionalInfo, money, tagsProject, moneyRaised, moneyWithCommission, numberOfDonations, categoryMod")]ProjectEditViewModel kreiran, int catMod)
        {
            kreiran.listaKaregorija = db.Category.ToList();
            ProjectModel zaBazu = new ProjectModel()
            {
                Id = kreiran.Id,
                nameProject = kreiran.nameProject,
                categoryMod = db.Category.Where(c => c.Id == catMod).FirstOrDefault(),
                descProject = kreiran.descProject,
                additionalInfo = kreiran.additionalInfo,
                money = kreiran.money,
                tagsProject = kreiran.tagsProject,
                moneyRaised = kreiran.moneyRaised,
                moneyWithCommission = kreiran.moneyWithCommission,
                numberOfDonations = kreiran.numberOfDonations

            };
            //  TryUpdateModel(zaBazu, new string[] { "categoryMod" });
            //  db.Project.Attach(zaBazu);
            db.Entry(zaBazu).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("ProjectIndex");

        }


    }
}