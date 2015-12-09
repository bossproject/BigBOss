using BigBoss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BigBoss.Controllers {
    public class ProjectController : Controller {
        public ApplicationDbContext db { get { return HttpContext.GetOwinContext().Get<ApplicationDbContext>(); } }
        // GET: Project
        public async Task<ActionResult> Show(string id) {
            return View(await db.Project.FindAsync(id));
        }

        public ActionResult Search(string q) {
            var list = db.Project.Where(p => p.nameProject.Contains(q) || p.tagsProject.Contains(q) || p.categoryMod.nameCategory.Contains(q)).ToList();
            return View(list);
        }
    }
}