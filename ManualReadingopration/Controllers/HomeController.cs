using ManualReadingopration.dbo_connt;
using ManualReadingopration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ManualReadingopration.Controllers
{
    public class HomeController : Controller
    {
        UserdatabaseEntities db = new UserdatabaseEntities();
        public ActionResult Index()
        {
            var res = db.ManualReadings.ToList();
            List<ManualModel> obj = new List<ManualModel>();
            foreach (var item in res)
            {
                obj.Add(new ManualModel
                {
                    SiteId = item.SiteId,
                    DataBaseName = item.DataBaseName,
                    MeterId = item.MeterId,
                    MainReading = item.MainReading,
                    DG_reading = item.DG_reading,
                    Engineer_Name = item.Engineer_Name,
                    joiningdate = item.joiningdate,

                }); ;
            }
            return View(obj);
        }

        public ActionResult create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(ManualModel obj)
        {

            ManualReading b = new ManualReading();
            b.SiteId = obj.SiteId;
            b.DataBaseName = obj.DataBaseName;
            b.MeterId = obj.MeterId;
            b.MainReading = obj.MainReading;
            b.DG_reading = obj.DG_reading;
            b.Engineer_Name = obj.Engineer_Name;
            b.joiningdate = obj.joiningdate;
            if (b.SiteId == 0)
            {

                db.ManualReadings.Add(b);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["inserted"] = "<script>alert('Data inserted succussfully')</script>";
                }
                return RedirectToAction("index");
            }
            else
            {
                db.Entry(b).State = System.Data.Entity.EntityState.Modified;
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["update"] = "<script>alert('Data updated Successfully')</script>";
                }
            }
            return RedirectToAction("index");
        }

        public ActionResult Edit(int? SiteId)
        {

            var Et = db.ManualReadings.Where(x => x.SiteId == SiteId).FirstOrDefault();
            ManualModel d = new ManualModel();
            d.SiteId = Et.SiteId;
            d.DataBaseName = Et.DataBaseName;
            d.MeterId = Et.MeterId;
            d.MainReading = Et.MainReading;
            d.Engineer_Name = Et.Engineer_Name;
            d.joiningdate = Et.joiningdate;
            d.DG_reading = Et.DG_reading;
            ViewBag.Edit = "Edit";
            return View("Create", d);
        }
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin loginClass)
        {
            if (ModelState.IsValid)
            {

                var result = db.UserLogins.Where(opt => opt.Engineer_Name.Equals(loginClass.Engineer_Name) && opt.Password.Equals(loginClass.Password)).FirstOrDefault();
                if (result != null)
                {
                    return RedirectToAction("index", "Home");
                }
                TempData["LoginId"] = " invalid UserName or Password";
                return RedirectToAction("Login");
            }

            return RedirectToAction("create");
        }
        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("login");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}