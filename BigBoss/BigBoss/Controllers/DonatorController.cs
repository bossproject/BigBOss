using BigBoss.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BigBoss.Controllers {
    [Authorize]
    public class DonatorController : Controller {

        public ApplicationDbContext db {
            get {
                return HttpContext.GetOwinContext().Get<ApplicationDbContext>();
            }
        }

        public ApplicationUserManager UserManager {
            get {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public async Task<ActionResult> Index() {
            string id = User.Identity.GetUserId();
            var user = await db.Donator.Where(u => u.usersAplication.Id.Equals(id)).Include("usersAplication").FirstAsync();
            //user.ToString();
            //if(user == null) {
            //    return View("Error");
            //}
            return View(user);
        }
    }
}