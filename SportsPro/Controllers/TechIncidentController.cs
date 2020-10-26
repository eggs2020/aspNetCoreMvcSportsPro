using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; // for session state
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models; 
using SportsPro.DataLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace SportsPro.Controllers
{
    [Authorize]
    public class TechIncidentController : Controller
    {
        private ISportsProUnitOfWork data { get; set; }

        public TechIncidentController(ISportsProUnitOfWork ctx)
        {
            data = ctx;
        }

        // Display technician names in a dropdown menu
        public IActionResult Get()
        {
            // Remove session state for TechnicianID so that website throws a message if user clicks "select" button without selecting a Technician
            HttpContext.Session.Remove("sessionTechId");

            var incidentOptions = new QueryOptions<Incident> { Includes = "Customer,Product" };
            var technicianOptions = new QueryOptions<Technician>();
            var customerOptions = new QueryOptions<Customer>();
            var productOptions = new QueryOptions<Product>();

            // Create a new instance of the view model
            var viewModel = new IncidentAddEditViewModel();

            // Popluate only the List<> and properties needed in the Views/Get.cshtml file
            viewModel.Incidents = data.Incidents.List(incidentOptions);
            viewModel.Technicians = data.Technicians.List(technicianOptions);
            viewModel.CurrentIncident = new Incident();
            viewModel.CurrentTechnician = new Technician();

            return View(viewModel);
        }

        // Display incidents assigned to the selected technician
        public IActionResult List(int technicianId)
        {
            // Obtain session state for TechnicianID so website shows List page for that technician only
            // The session state is set in the Edit() action method
            int? sessionTechId = HttpContext.Session.GetInt32("sessionTechId");

            // Check if there is a session state for TechnicianID (ie. user is already in session on our website)
            if (sessionTechId != null)
                technicianId = (int)sessionTechId;

            // No session state and the user selects a Technician
            if (technicianId != 0)
            {
                var productOptions = new QueryOptions<Product>();
                var customerOptions = new QueryOptions<Customer>();
                var incidentOptions = new QueryOptions<Incident>
                {
                    Includes = "Customer,Product",
                    WhereClauses = new WhereClauses<Incident>
                    {
                        {t => t.TechnicianID == technicianId },
                        {i => i.DateClosed == null}
                    }
                };

                var viewModel = new IncidentAddEditViewModel();

                viewModel.Products = data.Products.List(productOptions);
                viewModel.Customers = data.Customers.List(customerOptions);
                viewModel.CurrentTechnician = data.Technicians.Get(technicianId);

                // Populate Incidents List<> with incident object meeting query above
                viewModel.Incidents = data.Incidents.List(incidentOptions);

                // Check if the selected Technician has any open incidents
                if (viewModel.Incidents.Count == 0)
                    TempData["message"] = $"No open incidents for this technician.";

                return View(viewModel);
            }
            else
            {
                // User does not select a Technician
                TempData["message"] = "Please select a technician.";
                return RedirectToAction("Get");
            }
        }//List() action method

        // Load data to edit page
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var technicianOptions = new QueryOptions<Technician>();
            var customerOptions = new QueryOptions<Customer>();
            var productOptions = new QueryOptions<Product>();
            var incidentOptions = new QueryOptions<Incident>
            {
                Includes = "Customer,Product",
                WhereClauses = new WhereClauses<Incident>
                    {
                        {t => t.IncidentID == id }
                    }
            };

            // Create a new instance of the view model
            var viewModel = new IncidentAddEditViewModel();

            // Populate List<> and properties of the view model
            viewModel.operationType = "Edit";
            viewModel.CurrentIncident = data.Incidents.Get(id);
            viewModel.Technicians = data.Technicians.List(technicianOptions);
            viewModel.Customers = data.Customers.List(customerOptions);
            viewModel.Products = data.Products.List(productOptions);

            // Populate Incidents List<> with those having the selected Incident
            viewModel.Incidents = data.Incidents.List(incidentOptions);

            // Set session state for TechnicianID 
            HttpContext.Session.SetInt32("sessionTechId", (int)viewModel.CurrentIncident.TechnicianID);

            return View(viewModel);
        }

        // Save revised incident that is assigned to a technician
        [HttpPost]
        public IActionResult Edit(IncidentAddEditViewModel viewModel)   // To save revised data for the incident
        {

            if (ModelState.IsValid)
            {
                data.Incidents.Update(viewModel.CurrentIncident);
                data.Incidents.Save();
                TempData["message"] = $"Incident updated successfully.";
                return RedirectToAction("List", "TechIncident");
            }
            else
            {
                viewModel.CurrentIncident = data.Incidents.Get(viewModel.CurrentIncident.IncidentID);
                viewModel.operationType = "Edit";
                return View("Edit", viewModel);
            }
        }

    }// controller
}// namespace
