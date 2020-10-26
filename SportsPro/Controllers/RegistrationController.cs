using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using SportsPro.Models;
using SportsPro.DataLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace SportsPro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RegistrationController : Controller
    {
        private SportsProContext context { get; set; }

        public RegistrationController(SportsProContext ctx)
        {
            context = ctx;
        }

        public IActionResult GetCustomer()
        {
            HttpContext.Session.Remove("sessionID");

            var viewModel = new RegistrationViewModel();

            viewModel.ActiveCustomer = new Customer();
            viewModel.Customers = context.Customers.ToList();
            viewModel.Products = context.Products.ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult List(int customerId) 
        {
            // Obtain session state for CustomerID
            int? sessionID = HttpContext.Session.GetInt32("sessionID");

            if (customerId == 0 && sessionID == null)
            {
                TempData["message"] = "Please select a customer";
                return RedirectToAction("GetCustomer");
            }
            else
            {
                var viewModel = new RegistrationViewModel();

                viewModel.ActiveCustomer = context.Customers.Find(customerId);
                viewModel.Customers = context.Customers.ToList();
                viewModel.Products = context.Products.ToList();

                IQueryable<Registration> query = context.Registrations;
                query = query.Where(p => p.CustomerID == viewModel.ActiveCustomer.CustomerID);
                viewModel.Registrations = query.ToList();

                if (viewModel.Registrations.Count == 0)
                    TempData["message"] = $"No registered products for {viewModel.ActiveCustomer.FullName}."; 

                // Set session state for CustomerID
                HttpContext.Session.SetInt32("sessionID", viewModel.ActiveCustomer.CustomerID);

                return View(viewModel);
            }
        } // List action method

        [HttpPost]
        public IActionResult Add(Registration registration)
        {
            int? sessionID = HttpContext.Session.GetInt32("sessionID");

            if (registration.ProductID == 0)
            {
                TempData["message"] = "Select a product to be registered to the customer.";
                return RedirectToAction("List","Registration");
            }
            else
            {
                //Add product only if it is NOT already registered to the customer
                var existingRegistration = context.Registrations.Find(registration.CustomerID, registration.ProductID);
                if (existingRegistration == null)
                {
                    context.Registrations.Add(registration);
                    context.SaveChanges();
                    return RedirectToAction("List", "Registration", registration);
                }
                else 
                {
                    TempData["message"] = "Product is already registered to the customer.";
                    return RedirectToAction("List", "Registration", registration);
                }
            }        
        } // Add action method

        [HttpGet]
        public IActionResult Delete(int id)
        {
            int? customerId = HttpContext.Session.GetInt32("sessionID");
            var productId = id;

            var registration = context.Registrations.Find(customerId, productId);

            context.Registrations.Remove(registration);
            context.SaveChanges();
            return RedirectToAction("List", "Registration", registration);
        }


    } //controller class

} //namespace
