using ASP.NET_MVC_EF_CodeFirst.Models;
using ASP.NET_MVC_EF_CodeFirst.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP.NET_MVC_EF_CodeFirst.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        [HttpGet]
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        public ActionResult New(Person person)
        {
            using (DatabaseContext databaseContext = new DatabaseContext())
            {
                databaseContext.Persons.Add(person);
                var result = databaseContext.SaveChanges();
                if (result > 0)
                {
                    ViewBag.Result = "Kişi Kaydedildi.";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Result = "Kişi Kaydedilemedi.";
                    ViewBag.Status = "danger";
                }

            }

            return View();
        }
        [HttpGet]
        public ActionResult Edit(int personId)
        {
            Person person = null;

            if (personId != null)
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    person = context.Persons.Where(p => p.Id == personId).SingleOrDefault();
                }
            }

            return View(person);
        }
        [HttpPost]
        public ActionResult Edit(Person person, int personId)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                Person updatedPerson = context.Persons.Where(p => p.Id == personId).SingleOrDefault();

                if (person != null)
                {
                    updatedPerson.FirstName = person.FirstName;
                    updatedPerson.LastName = person.LastName;
                    updatedPerson.Addresses = person.Addresses;
                    updatedPerson.Age = person.Age;

                    var result = context.SaveChanges();

                    if (result > 0)
                    {
                        ViewBag.Result = "Kişi Güncellendi.";
                        ViewBag.Status = "success";
                    }
                    else
                    {
                        ViewBag.Result = "Kişi Güncellenemedi.";
                        ViewBag.Status = "danger";
                    }
                }
            }
            return View(person);
        }
        [HttpGet]
        public ActionResult Delete(int personId)
        {
            Person person = null;
            if(personId != null)
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    person = context.Persons.Where(p=>p.Id==personId).SingleOrDefault();
                }
            }
            return View(person);
        }
        [HttpPost,ActionName("Delete")]
        public ActionResult DeletePerson(int personId)
        {
            if (personId != null)
            {
                using (DatabaseContext context = new DatabaseContext())
                {
                    Person person = context.Persons.Where(p => p.Id == personId).SingleOrDefault();

                    context.Persons.Remove(person);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("HomePage","Home");
        }
    }
}