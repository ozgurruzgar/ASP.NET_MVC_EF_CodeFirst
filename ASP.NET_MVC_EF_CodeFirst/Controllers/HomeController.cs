using ASP.NET_MVC_EF_CodeFirst.Models.Managers;
using ASP.NET_MVC_EF_CodeFirst.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_MVC_EF_CodeFirst.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult HomePage()
        {
            DatabaseContext database = new DatabaseContext();
            //var persons = database.Persons.ToList();

            HomePageViewModel viewModel = new HomePageViewModel();
            viewModel.Persons = database.Persons.ToList();
            viewModel.Addresses = database.Addresses.ToList(); 

            return View(viewModel);
        }
    }
}