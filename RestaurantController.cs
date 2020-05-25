using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant_DataContract;
using DAL;
using BAL;


namespace Restaurant.Controllers
{
    public class RestaurantController : Controller
    {
        List<FoodItems> listadd = null;
        BALFood objAdd = null;
        // GET: Restaurant
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }
        public ActionResult Login(string Username, string Password)
        {
            if (Username == "sai" && Password == "sai")
            {
                return Json("Success");
            }
            return View();
        }
        public ActionResult HomeIndex()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetData()
        {
            try
            {
                objAdd = new BALFood();
                string Foodobj = objAdd.GetFoodDetails();
                return Json(Foodobj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Register()
        {
            
            return View();
        }
    }
}