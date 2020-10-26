using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using SportsPro.Models; 
using SportsPro.DataLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TechnicianController : Controller
    {
        private IRepository<Technician> data { get; set; }

        public TechnicianController(IRepository<Technician> ctx)
        {
            data = ctx;
        }

        // Method to list all technicians from DB
        [Route("Technicians")]
        public IActionResult List()
        {
            var techOptions = new QueryOptions<Technician>();
            var technicians = data.List(techOptions);
            return View(technicians);
        }


        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit", new Technician());
        }

        // method to load selected-technician data to the edit page
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";           
            var technician = data.Get(id);
            return View("AddEdit", technician); 
        }


        // method to save new or revised details of a technician
        [HttpPost]
        public IActionResult Edit(Technician technician)
        {
            if (ModelState.IsValid)
            {
                if (technician.TechnicianID == 0)
                {
                    data.Insert(technician);
                    //Adding TempData message
                    TempData["message"] = $"{technician.Name} has been added.";
                }
                else
                { 
                    data.Update(technician);
                    //Adding TempData message
                    TempData["message"] = $"{technician.Name} has been editted.";
                }
                
                data.Save();
                return RedirectToAction("List", "Technician"); // List is a method in the TechnicianController
            }
            else
            {
                if (technician.TechnicianID == 0)
                    ViewBag.Action = "Add";
                else
                    ViewBag.Action = "Edit";

                return View("AddEdit", technician); // rendering AddEdit view file and passing technician object to it
            }
        }

        //Load info to the delete page 
        [HttpGet]
        public IActionResult Delete(int id)  
        {
            var technician = data.Get(id);
            return View(technician);
        }

        // Serve a POST request when user click "delete" button on the delete page
        [HttpPost]
        public IActionResult Delete(Technician technician)  
        {
            data.Delete(technician);
            data.Save();

            //Adding TempData message
            TempData["message"] = $"{technician.Name} has been deleted.";

            return RedirectToAction("List", "Technician"); 
        }

    }// Controller class
}// namespace
