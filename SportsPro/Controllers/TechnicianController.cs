using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // manually added
using SportsPro.Models; // manually added

namespace SportsPro.Controllers
{
    public class TechnicianController : Controller
    {
        private SportsProContext context { get; set; }

        public TechnicianController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("Technicians")]
        public IActionResult List()
        {
            var technicians = context.Technicians.ToList();
            return View(technicians);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit", new Technician());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";           
            var technician = context.Technicians.Find(id);
            return View("AddEdit", technician);  // AddEdit is the View file name, technician is the object on line 42
        }

        [HttpPost]
        public IActionResult Edit(Technician technician)
        {
            if (ModelState.IsValid)
            {
                if (technician.TechnicianID == 0)
                {
                    context.Technicians.Add(technician);
                    //Adding TempData message
                    TempData["message"] = $"{technician.Name} has been added.";
                }
                else
                { 
                    context.Technicians.Update(technician);
                    //Adding TempData message
                    TempData["message"] = $"{technician.Name} has been editted.";
                }
                
                context.SaveChanges();
                return RedirectToAction("List", "Technician"); // List is a method in the TechnicianController
            }
            else
            {
                if (technician.TechnicianID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit";
                }
                //------------- alternative to above if else statement ------------
                //ViewBag.Action = (technician.TechnicianID == 0) ? "Add" : "Edit";
                //-----------------------------------------------------------------
                return View("AddEdit", technician); // rendering AddEdit view file and passing technician object to it
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)     //Load the info to the delete page 
        {
            var technician = context.Technicians.Find(id);
            return View(technician);
        }

        [HttpPost]
        public IActionResult Delete(Technician technician)  // Respond to a POST request when user click "delete" button on the delete page
        {
            context.Technicians.Remove(technician);
            context.SaveChanges();

            //Adding TempData message
            TempData["message"] = $"{technician.Name} has been deleted.";

            return RedirectToAction("List", "Technician"); // List() method in TechnicianController
        }

    }// TechnicianController class
}// namespace
