using ASP.NET_MVC_EF_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_MVC_EF_CodeFirst.ViewModels.Home
{
    public class HomePageViewModel
    {
        public List<Person> Persons { get; set; }
        public List<Address> Addresses { get; set; }
    }
}