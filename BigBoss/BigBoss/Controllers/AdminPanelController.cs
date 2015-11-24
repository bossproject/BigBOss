using BigBoss.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BigBoss.Controllers {
    public class AdminPanelController : Controller {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AdminPanel
        public ActionResult Index() {
            return View();
        }

        //----------- Category controll -----------

        public ActionResult CategoryIndex() {
            var lista = db.Category.ToList();
            return View(db.Category.ToList());
        }

        public ActionResult CategoryCreate() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CategoryCreate(CategoryModel catModel) {
            if(ModelState.IsValid) {
                db.Category.Add(catModel);
                await db.SaveChangesAsync();
                TempData["success_msg"] = "Category saved!";
                return RedirectToAction("CategoryIndex");
            }
            return View();
        }

        public ActionResult CategoryEdit(int? id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cat = db.Category.Where(c => c.Id == id).FirstOrDefault();
            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CategoryEdit([Bind(Include = "Id, nameCategory, descriptionCategory")] CategoryModel cat) {
            if(ModelState.IsValid) {
                db.Entry(cat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                TempData["success_msg"] = "Category saved!";
                return RedirectToAction("CategoryIndex");

            }
            return View();
        }

        public ActionResult CategoryDelete(int? id) {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cat = db.Category.Where(c => c.Id == id).FirstOrDefault();

            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CategoryDelete(int id) {
            var cat = db.Category.Where(c => c.Id == id).FirstOrDefault();
            db.Category.Remove(cat);
            await db.SaveChangesAsync();
            TempData["success_msg"] = "Category deleted!";
            return RedirectToAction("CategoryIndex");
        }

        //----------- Project controll ------------

        public ActionResult RoleIndex() {
            return View(db.Roles.ToList());
        }

        public ActionResult ProjectIndex() {
            var projectList = db.Project.Include(c => c.categoryMod).ToList();
            return View(projectList);
        }

        public ActionResult ProjectCreate() {
            ViewBag.CatModel = new SelectList(db.Category.ToList(), "Id", "nameCategory");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProjectCreate(ProjectModel modelProj, int? categoryMod) {
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

        public async Task<ActionResult> ProjectEdit(string id) {

            ProjectModel mod = await db.Project.FindAsync(id);
            //    var lista = new SelectList(db.Category.ToArray(), "Id", "nameCategory");
            //    ViewBag.CatModel = lista;
            ProjectEditViewModel noviMod = new ProjectEditViewModel();
            noviMod.Id = mod.Id;
            noviMod.listaKaregorija = new SelectList(db.Category.ToList(), "Id", "nameCategory", mod.categoryMod.nameCategory);
            noviMod.categoryMod = mod.categoryMod;
            noviMod.nameProject = mod.nameProject;
            // model.OrderTemplates = new SelectList(db.OrderTemplates, "OrderTemplateId", "OrderTemplateName", 1);
            return View(noviMod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProjectEdit(ProjectEditViewModel kreiran, int categoryMod) {

            int i = categoryMod;

            ProjectModel zaBazu = db.Project.Where(p => p.Id.Equals(kreiran.Id)).First();
            zaBazu.Id = kreiran.Id;
            zaBazu.nameProject = kreiran.nameProject;
            zaBazu.categoryMod = db.Category.Where(c => c.Id == i).First();
            zaBazu.descProject = kreiran.descProject;
            zaBazu.additionalInfo = kreiran.additionalInfo;
            zaBazu.money = kreiran.money;
            zaBazu.tagsProject = kreiran.tagsProject;
            zaBazu.moneyRaised = kreiran.moneyRaised;
            zaBazu.moneyWithCommission = kreiran.moneyWithCommission;
            zaBazu.numberOfDonations = kreiran.numberOfDonations;

            await db.SaveChangesAsync();
            return RedirectToAction("ProjectIndex");

        }

        [HttpGet]
        public ActionResult ProjectDelete(string id) {
            var proj = db.Project.Where(p => p.Id.Equals(id)).FirstOrDefault();
            return View(proj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProjectDelete(ProjectModel p) {
            var pro = await db.Project.Where(c => c.Id == p.Id).FirstOrDefaultAsync();
            db.Project.Remove(pro);
            await db.SaveChangesAsync();
            TempData["success_msg_proj"] = "Project successfully deleted!";
            return RedirectToAction("ProjectIndex");
        }

        public ActionResult DonatorsIndex() {
            return View(db.Donator.ToList());
        }

        public ActionResult DonatorEdit(string id) {
            if(id == null || id.Equals(string.Empty)) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(db.Donator.Where(d => d.Id.Equals(id)).First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DonatorEdit(DonatorModel model) {
            if(!ModelState.IsValid) {
                return View();
            }

            var editModel = db.Donator.Where(d => d.Id.Equals(model.Id)).First();

            if(editModel == null) {
                return View();
            }

            editModel.Id = model.Id;
            editModel.MaticniBroj = model.MaticniBroj;
            editModel.OrganizationName = model.OrganizationName;
            editModel.street = model.street;
            editModel.City = model.City;
            editModel.Country = model.Country;
            editModel.usersAplication = editModel.usersAplication;

            await db.SaveChangesAsync();
            return RedirectToAction("DonatorsIndex");
        }

        public ActionResult DonatorDetails(string id) {
            if(id == null || id.Equals(string.Empty)) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(db.Donator.Where(d => d.Id.Equals(id)).First());
        }

        public ActionResult DonatorDelete(string id) {
            if(id == null || id.Equals(string.Empty)) {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(db.Donator.Where(d => d.Id.Equals(id)).First());
        }

        [HttpPost, ActionName("DonatorDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DonatorDeleteConfirmed(string id) {
            if(!ModelState.IsValid) {
                return View();
            }

            if(id == null || id.Equals(string.Empty)) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var deleteModel = db.Donator.Where(d => d.Id.Equals(id)).First();

            if(deleteModel == null) {
                new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            //User FinishRegistration prebaciti na false
            db.Donator.Remove(deleteModel);
            db.SaveChanges();
            return RedirectToAction("DonatorsIndex");
        }

        public ActionResult OrganizationIndex() {
            return View(db.Organization.ToList());
        }

        public ActionResult OrganizationEdit(string id) {
            if(id == null || id.Equals(string.Empty)) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.Organization.Where(o => o.Id.Equals(id)).First());
        }

        public async Task<ActionResult> OrganizationEdit(OrganizationModel model) {
            if(!ModelState.IsValid) {
                return View();
            }

            var editModel = db.Organization.Where(o => o.Id.Equals(model.Id)).First();
            editModel.Id = model.Id;
            editModel.MaticniBroj = model.MaticniBroj;
            editModel.OrganizationName = model.OrganizationName;
            editModel.PIB = model.PIB;
            editModel.usersAplication = model.usersAplication;

            await db.SaveChangesAsync();
            return RedirectToAction("OrganizationIndex");
        }

        public ActionResult OrganizationDetails(string id) {
            if(id == null || id.Equals(string.Empty)) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.Organization.Where(o => o.Id.Equals(id)).First());
        }

        public ActionResult OrganizationDelete(string id) {
            if(id == null || id.Equals(string.Empty)) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.Organization.Where(o => o.Id.Equals(id)).First());
        }

        [HttpPost, ActionName("OrganizationDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult OrganizationDeleteConfirmed(string id) {
            if(!ModelState.IsValid) {
                return View();
            }

            if(id == null || id.Equals(string.Empty)) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var deleteModel = db.Organization.Where(o => o.Id.Equals(id)).First();

            if(deleteModel == null) {
                new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            db.Organization.Remove(deleteModel);
            db.SaveChanges();
            return RedirectToAction("OrganizationIndex");
        }

        public ActionResult CompanyIndex() {
            return View(db.Company.ToList());
        }

        public ActionResult CompanyEdit(string id) {
            if(id == null || id.Equals(string.Empty)) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(db.Company.Where(c => c.Id.Equals(id)).First());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CompanyEdit(CompanyModel model) {
            if(!ModelState.IsValid) {
                return View();
            }

            var editModel = db.Company.Where(c => c.Id.Equals(model.Id)).First();

            if(editModel == null) {
                new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            editModel.Id = model.Id;
            editModel.Delatnost = model.Delatnost;
            editModel.MaticniBroj = model.MaticniBroj;
            editModel.OrganizationName = model.OrganizationName;
            editModel.PIB = model.PIB;
            editModel.usersAplication = model.usersAplication;

            await db.SaveChangesAsync();

            return RedirectToAction("CompanyIndex");
        }

        public ActionResult CompanyDetails(string id) {
            if(id == null || id.Equals(string.Empty)) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.Company.Where(c => c.Id.Equals(id)).First());
        }

        public ActionResult CompanyDelete(string id) {
            if(id == null || id.Equals(string.Empty)) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(db.Company.Where(c => c.Id.Equals(id)).First());
        }

        [HttpPost, ActionName("CompanyDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult CompanyDeleteConfirmed(string id) {
            if(!ModelState.IsValid) {
                return View();
            }

            if(id == null || id.Equals(string.Empty)) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var deleteModel = db.Company.Where(o => o.Id.Equals(id)).First();

            if(deleteModel == null) {
                new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            db.Company.Remove(deleteModel);
            db.SaveChanges();
            return RedirectToAction("OrganizationIndex");
        }
    }
}