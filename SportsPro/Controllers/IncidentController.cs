using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore; 
using SportsPro.Models; 
using SportsPro.DataLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class IncidentController : Controller
    {
        
        private ISportsProUnitOfWork data { get; set; }
        public IncidentController(ISportsProUnitOfWork unit)
        {
            data = unit;
        }

        [Route("[controller]s/{id?}")]
        public IActionResult List(string id = "all")
        {            
            ViewBag.filterId = "all";
            var incidentOptions = new QueryOptions<Incident> { Includes = "Customer,Product" };
            
            if (id == "unassigned")
            {
                incidentOptions.Where = i => i.TechnicianID == null;
                ViewBag.filterId = "unassigned";
            }
            if (id == "openned")
            {
                incidentOptions.Where = i => i.DateClosed == null;
                ViewBag.filterId = "openned";
            }
                        
            var incidents = data.Incidents.List(incidentOptions);

            return View(incidents);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var incidentOptions = new QueryOptions<Incident> { Includes = "Customer,Product" };
            var technicianOptions = new QueryOptions<Technician>();
            var customerOptions = new QueryOptions<Customer>();
            var productOptions = new QueryOptions<Product>();

            // create a new instance of the view model
            var viewModel = new IncidentAddEditViewModel();

            // Populate List<> and properties of the view model
            viewModel.operationType = "Add";
            viewModel.Products = data.Products.List(productOptions);
            viewModel.Customers = data.Customers.List(customerOptions);
            viewModel.Technicians = data.Technicians.List(technicianOptions);
            viewModel.CurrentIncident = new Incident();

            return View("AddEdit", viewModel);
        }

        // Load data to the edit page 
        [HttpGet]
        public IActionResult Edit(int id)   
        {
            var incidentOptions = new QueryOptions<Incident> { Includes = "Customer,Product" };
            var technicianOptions = new QueryOptions<Technician>();
            var customerOptions = new QueryOptions<Customer>();
            var productOptions = new QueryOptions<Product>();

            // Create a new instance of the view model and populate the necessary List<> and properties
            var viewModel = new IncidentAddEditViewModel();

            viewModel.operationType = "Edit";
            viewModel.Products = data.Products.List(productOptions);
            viewModel.Customers = data.Customers.List(customerOptions);
            viewModel.Technicians = data.Technicians.List(technicianOptions);
            viewModel.CurrentIncident = data.Incidents.Get(id);

            return View("AddEdit", viewModel);
        }

        // Serving post request to save new or revised incident information
        [HttpPost]
        public IActionResult Edit(IncidentAddEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.CurrentIncident.IncidentID == 0)
                {
                    data.Incidents.Insert(viewModel.CurrentIncident);
                    TempData["message"] = $"1 new incident has been added";
                }
                else
                {
                    data.Incidents.Update(viewModel.CurrentIncident);
                    TempData["message"] = $"Incident ID: {viewModel.CurrentIncident.IncidentID} has been editted.";
                }
                data.Incidents.Save();
                return RedirectToAction("List", "Incident");
            }
            else
            {
                // Validation on the form fails. Reload same page with viewmodel data already there
                viewModel.operationType = (viewModel.CurrentIncident.IncidentID == 0) ? "Add" : "Edit"; 
                return View("AddEdit", viewModel);
            }
        }

        // Load incident details to the confirm-deletion page
        [HttpGet]
        public IActionResult Delete(int id)
        {
            //These ViewBags are used just so we can display customer name and product name on delete page
            var customerOptions = new QueryOptions<Customer>();
            ViewBag.Customers = data.Customers.List(customerOptions);

            var productOptions = new QueryOptions<Product>();
            ViewBag.Products = data.Products.List(productOptions);
            var incident = data.Incidents.Get(id);
            return View(incident);
        }

        // Delete record from DB for incident detail sent from confirm-deletion page
        [HttpPost]
        public IActionResult Delete(Incident incident)      
        {
            data.Incidents.Delete(incident);
            data.Incidents.Save();
            TempData["message"] = $"Incident ID: {incident.IncidentID} has been deleted.";

            return RedirectToAction("List", "Incident");    // List() method of IncidentController
        }       

    } //controller class

} //namespace
