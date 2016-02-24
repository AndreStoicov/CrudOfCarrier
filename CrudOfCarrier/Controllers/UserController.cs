using CrudOfCarrier.Models.Entities;
using CrudOfCarrier.Repository;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace CrudOfCarrier.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var userRep = new UserRepository();
                var isValid = userRep.IsValid(user.UserName, user.Password).Result;

                if (isValid)
                {
                    Session["LogedUserFullname"] = user.UserName;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    throw new Exception("Login data is incorrect!");
                }
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            Session["LogedUserFullname"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                UserRepository userRep = new UserRepository();

                if (userRep.GetByName(model.UserName).Result == null)
                {
                    throw new Exception("This UserName has already been taken.");
                }

                var result = await userRep.Register(model);
                Login(result);
            }

            return View(model);
        }
    }
}