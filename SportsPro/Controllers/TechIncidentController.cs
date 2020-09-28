using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; // manually added to use session state
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // manually added
using SportsPro.Models; // manually added


namespace SportsPro.Controllers
{
    public class TechIncidentController : Controller
    {
        private SportsProContext context { get; set; }

        public TechIncidentController(SportsProContext ctx)
        {
            context = ctx;
        }

        public IActionResult Get()
        {
            // Remove session state for TechnicianID so that website throws a message if user clicks "select" button without selecting a Technician
            HttpContext.Session.Remove("sessionTechId");

            // Create a new instance of the view model
            var viewModel = new IncidentAddEditViewModel();

            // Popluate only the List<> and properties needed in the Views/Get.cshtml file
            viewModel.Incidents = context.Incidents.ToList();
            viewModel.Technicians = context.Technicians.ToList();
            viewModel.CurrentIncident = new Incident();
            viewModel.CurrentTechnician = new Technician();

            return View(viewModel);
        }

        
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
                var viewModel = new IncidentAddEditViewModel();

                viewModel.Products = context.Products.ToList();
                viewModel.Customers = context.Customers.ToList();
                viewModel.CurrentTechnician = context.Technicians.Find(technicianId);

                // Query expression for incidents that are assigned to the current technician AND with no DateClosed
                IQueryable<Incident> query = context.Incidents;
                query = query.Where(i => i.TechnicianID == viewModel.CurrentTechnician.TechnicianID);
                query = query.Where(i => i.DateClosed == null);

                // Populate Incidents List<> with incident object meeting query above
                viewModel.Incidents = query.ToList();

                // Check if the selected Technician has any open incidents
                if (viewModel.Incidents.Count == 0)
                    TempData["message"] = $"No open incidents for this technician.";

                return View(viewModel);
            }
            else 
            {
                // User does not select a Technician
                TempData["message"] = $"Please select a technician.";
                return RedirectToAction("Get");
            }
        }

        
        [HttpGet]
        public IActionResult Edit(int id)   // Load data to edit page when user click "edit" button on List page
        {
            // Create a new instance of the view model
            var viewModel = new IncidentAddEditViewModel();

            // Populate List<> and properties of the view model
            viewModel.operationType = "Edit";
            viewModel.CurrentIncident = context.Incidents.Find(id);
            viewModel.Technicians = context.Technicians.ToList();
            viewModel.Customers = context.Customers.ToList();
            viewModel.Products = context.Products.ToList();
            
            // Query and populate Incidents List<> with those having the selected Incident
            IQueryable<Incident> query = context.Incidents;
            query = query.Where(i => i.IncidentID == viewModel.CurrentIncident.IncidentID);
            viewModel.Incidents = query.ToList();
            
            // Set session state for TechnicianID 
            HttpContext.Session.SetInt32("sessionTechId", (int)viewModel.CurrentIncident.TechnicianID);

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult Edit(IncidentAddEditViewModel viewModel)   // To save revised data for the incident
        {

            if (ModelState.IsValid)
            {
                context.Incidents.Update(viewModel.CurrentIncident);
                context.SaveChanges();
                TempData["message"] = $"Incident updated successfully.";
                return RedirectToAction("List", "TechIncident");
            }
            else 
            {
                viewModel.CurrentIncident = context.Incidents.Find(viewModel.CurrentIncident.IncidentID);
                viewModel.operationType = "Edit";
                return View("Edit", viewModel);
            }
        }

    }// controller

}// namespace
