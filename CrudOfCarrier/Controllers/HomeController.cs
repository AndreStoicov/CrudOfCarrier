using CrudOfCarrier.Models.Entities;
using CrudOfCarrier.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CrudOfCarrier.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            CarrierRepository rep = new CarrierRepository();
            var allcarriers = await rep.GetAll();

            ViewBag.UserLogged = Session["LogedUserFullname"];

            return View(allcarriers);
        }

        [HttpGet]
        public ActionResult Create()
        {          
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Carrier model)
        {
            if (ModelState.IsValid)
            {
                CarrierRepository rep = new CarrierRepository();

                if (rep.GetByName(model.Name).Result == null)
                {                    
                    throw new Exception("This Carrier already exists.");
                }
                else
                {
                    var allcarriers = await rep.AddCarrier(model);
                }
            }

            return RedirectToAction("Index");
        }
    }
}