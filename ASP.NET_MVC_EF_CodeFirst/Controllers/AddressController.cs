using ASP.NET_MVC_EF_CodeFirst.Models;
using ASP.NET_MVC_EF_CodeFirst.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_MVC_EF_CodeFirst.Controllers
{
    public class AddressController : Controller
    {
        // GET: Address
        [HttpGet]
        public ActionResult New()
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                List<SelectListItem> personsList =
                    (from p in context.Persons.ToList()
                     select new SelectListItem()
                     {
                         Text = p.FirstName + " " + p.LastName,
                         Value = p.Id.ToString()
                     }).ToList();
                TempData["Person"] = personsList;
                ViewBag.Person = personsList;
            }     
            return View();
        }
        [HttpPost]
        public ActionResult New(Address address)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                Person person = context.Persons.Where(p => p.Id == address.Person.Id).SingleOrDefault();

                if(person != null)
                {
                    address.Person = person;

                    context.Addresses.Add(address);
                    var result =context.SaveChanges();

                    if (result > 0)
                    {
                        ViewBag.Result = "Adres Kaydedildi.";
                        ViewBag.Status = "success";
                    }
                    else
                    {
                        ViewBag.Result = "Adres Kaydedilemedi.";
                        ViewBag.Status = "danger";
                    }
                }
            }
            ViewBag.Person = TempData["Person"];
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int addressId)
        {
            Address address=null;
            if (addressId != null)
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    List<SelectListItem> personsList =
                        (from p in context.Persons.ToList()
                         select new SelectListItem()
                         {
                             Text = p.FirstName + " " + p.LastName,
                             Value = p.Id.ToString()
                         }).ToList();
                    TempData["Person"] = personsList;
                    ViewBag.Person = personsList;


                    address = context.Addresses.Where(a => a.Id == addressId).SingleOrDefault();

                }
            }
            return View(address);
        }
        [HttpPost]
        public ActionResult Edit(Address address,int addressId)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                Person person = context.Persons.Where(p => p.Id == address.Person.Id).SingleOrDefault();
                
                Address updatedAddress = context.Addresses.Where(a => a.Id == addressId).SingleOrDefault();

                if (person != null)
                {
                    address.Person = person;

                    updatedAddress.AddressDetail = address.AddressDetail;
                    updatedAddress.Person = address.Person;
                    var result = context.SaveChanges();

                    if (result > 0)
                    {
                        ViewBag.Result = "Adres Güncellenmiştir.";
                        ViewBag.Status = "success";
                    }
                    else
                    {
                        ViewBag.Result = "Adres Güncellenememiştir.";
                        ViewBag.Status = "danger";
                    }
                }
            }
            ViewBag.Person = TempData["Person"];
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int addressId)
        {
            Address address = null;

            if(addressId != null)
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    address = context.Addresses.Where(a=>a.Id == addressId).SingleOrDefault();
                }
            }

            return View();
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteAddress(int addressId)
        {
            if (addressId != null)
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    Address deletedAddress = context.Addresses.Where(a => a.Id == addressId).SingleOrDefault();

                    context.Addresses.Remove(deletedAddress);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("HomePage", "Home");
        }
    }
}