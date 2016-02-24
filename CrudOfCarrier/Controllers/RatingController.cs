using CrudOfCarrier.Models.Entities;
using CrudOfCarrier.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CrudOfCarrier.Controllers
{
    public class RatingController : Controller
    {        
        public async Task<ActionResult> Index()
        {

            CarrierRatingRepository rep = new CarrierRatingRepository();
            var allcarriers = await rep.GetAll();

            CarrierRepository cRep = new CarrierRepository();

            var rated = allcarriers.GroupBy(x => new { x.Carrier.Name })
                .Select(u => new
                {
                    Carrier = u.Key.Name,
                    Stars = u.Sum(z => z.Stars) / u.Count()
                });

            var ratedList = new List<CarrierRating>();
            foreach (var item in rated)
            {
                ratedList.Add(new CarrierRating { Stars = item.Stars, Carrier = new Carrier() { Name = item.Carrier } });
            }

            return View(ratedList);
        }

        [HttpGet]
        public async Task<ActionResult> RateCarrier()
        {
            CarrierRepository rep = new CarrierRepository();
            var allcarriers = await rep.GetAll();
            List<SelectListItem> list = allcarriers.Select(x => new SelectListItem { Value = x.Name, Text = x.Name }).ToList();
            ViewBag.carrierNames = list;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RateCarrier(CarrierRating model)
        {
            if (ModelState.IsValid)
            {
                model.User = new User() { UserName = Session["LogedUserFullname"].ToString() };
                CarrierRatingRepository rep = new CarrierRatingRepository();
                await rep.RateCarrier(model);
            }

            return RedirectToAction("Index");
        }
    }
}