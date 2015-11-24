using BigBoss.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigBoss.Controllers {

    /*
    Sadrži sve metode i atribute koji su zajednički za kontrolere. Ali trenutno ne sadrži ništa.
    #DEAL WITH IT 
    #LAZYDEVELOPER
    */

    public class BaseController : Controller {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        
        public ApplicationSignInManager SignInManager {
            get {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set {
                _userManager = value;
            }
        }

        protected override void Dispose(bool disposing) {
            if(disposing) {
                if(_userManager != null) {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if(_signInManager != null) {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }


        public ApplicationDbContext db { get { return HttpContext.GetOwinContext().Get<ApplicationDbContext>(); } }

    }
}