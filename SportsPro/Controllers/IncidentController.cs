using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore; // manually added
using SportsPro.Models; // manually added

namespace SportsPro.Controllers
{
    public class IncidentController : Controller
    {
        private SportsProContext context { get; set; }

        public IncidentController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("[controller]s/{id?}")]
        public IActionResult List(string id="all")
        {
            // Create a new instance of the view model
            var viewModel = new IncidentViewModel();

            // Populate List<> and properties in the view model that we will need in the Views/List.cshtml file
            viewModel.Products = context.Products.ToList();
            viewModel.Customers = context.Customers.ToList();
            viewModel.Incidents = context.Incidents.ToList();
            //viewModel.Technicians = context.Technicians.ToList(); // not needed in the List.cshtml

            // Populate Incidents List<> in the view model based on filtering on the page
            ViewBag.filterId = "all";

            if (id == "unassigned")
            {
                viewModel.Incidents = context.Incidents
                      .Where(i => i.TechnicianID == null)
                      .ToList();
                ViewBag.filterId = "unassigned";
            }
            if (id == "openned")
            {
                viewModel.Incidents = context.Incidents
                    .Where(i => i.DateClosed == null)
                    .ToList();
                ViewBag.filterId = "openned";
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            // create a new instance of the view model
            var viewModel = new IncidentAddEditViewModel();

            // Populate List<> and properties of the view model
            viewModel.operationType = "Add";
            viewModel.Products = context.Products.ToList();
            viewModel.Customers = context.Customers.ToList();
            viewModel.Technicians = context.Technicians.ToList();
            viewModel.CurrentIncident = new Incident();

            return View("AddEdit", viewModel);
        }
        
        [HttpGet]
        public IActionResult Edit(int id)   // Load data to the edit page when user click "add" button on incident list page
        {
            // Create a new instance of the view model and populate the necessary List<> and properties
            var viewModel = new IncidentAddEditViewModel();
            
            viewModel.operationType = "Edit";
            viewModel.Products = context.Products.ToList();
            viewModel.Customers = context.Customers.ToList();
            viewModel.Technicians = context.Technicians.ToList();
            viewModel.CurrentIncident = context.Incidents.Find(id);
            
            return View("AddEdit", viewModel);
        }
       
        [HttpPost]
        public IActionResult Edit(IncidentAddEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.CurrentIncident.IncidentID == 0)
                {
                    context.Incidents.Add(viewModel.CurrentIncident);
                    //Adding a message that add action is successfull
                    TempData["message"] = $"1 new incident has been added";
                }
                else
                {
                    context.Incidents.Update(viewModel.CurrentIncident);
                    //Adding a message that edit action is successsfull
                    TempData["message"] = $"Incident ID: {viewModel.CurrentIncident.IncidentID} has been editted.";
                }
                context.SaveChanges();
                return RedirectToAction("List", "Incident");
            }
            else
            {
                // Validation on the form fails. Reload same page with view model data already there
                if (viewModel.CurrentIncident.IncidentID == 0)
                    viewModel.operationType = "Add";
                else
                    viewModel.operationType = "Edit";

                viewModel.Products = context.Products.ToList();
                viewModel.Customers = context.Customers.ToList();
                viewModel.Technicians = context.Technicians.ToList();

                return View("AddEdit", viewModel);
            }
        } // action edit
        

        [HttpGet]
        public IActionResult Delete(int id)
        {
            //These ViewBags are used just so we can display customer name and product name on delete page
            ViewBag.Customers = context.Customers.OrderBy(c => c.FirstName).ToList();
            ViewBag.Products = context.Products.OrderBy(p => p.Name).ToList();
            var incident = context.Incidents.Find(id);               
            return View(incident);
        }

        [HttpPost]
        public IActionResult Delete(Incident incident)      // When user click delete button from delete page
        {
            context.Incidents.Remove(incident);
            context.SaveChanges();

            //Adding message that delete action is successfull
            TempData["message"] = $"Incident ID: {incident.IncidentID} has been deleted.";

            return RedirectToAction("List", "Incident");    // List() method of IncidentController
        }

    } //controller class

} //namespace
