using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigBoss.Controllers
{

    /*
    Sadrži sve metode i atribute koji su zajednički za kontrolere. Ali trenutno ne sadrži ništa.
    #DEAL WITH IT 
    #LAZYDEVELOPER
    */

    public class BaseController : Controller
    {
        // GET: Base
        public ActionResult Index()
        {
            return View();
        }
    }
}