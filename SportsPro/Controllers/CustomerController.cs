using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // manually added
using SportsPro.Models; // manually added

namespace SportsPro.Controllers
{
    public class CustomerController : Controller
    {
        private SportsProContext context { get; set; }

        public CustomerController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("Customers")]
        public IActionResult List()
        {
            var customers = context.Customers.ToList();

            return View(customers);
        }


        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Countries = context.Countries.OrderBy(c => c.Name).ToList();
            return View("AddEdit", new Customer());
        }

        [HttpGet]
        public IActionResult Edit(int id)   // Load data to the edit page when user click "add" button on customer list page
        {
            ViewBag.Action = "Edit";
            ViewBag.Countries = context.Countries.OrderBy(c => c.Name).ToList();
            var customer = context.Customers.Find(id);
            return View("AddEdit", customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)    // This method is to send revised-data from edit page
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                {
                    context.Customers.Add(customer);
                    // Display message on page
                    TempData["message"] = $"{customer.FullName} has been added.";
                }
                else
                {
                    context.Customers.Update(customer);
                    // Display message on page
                    TempData["message"] = $"{customer.FullName} has been editted.";
                }

                context.SaveChanges();
                return RedirectToAction("List", "Customer");  // List() method of CustomerController
            }
            else
            {
                // message for validation summary
                ModelState.AddModelError(string.Empty,"There are errors in the form.");
                
                ViewBag.Action = (customer.CustomerID == 0) ? "Add" : "Edit";
                ViewBag.Countries = context.Countries.OrderBy(c => c.Name).ToList();
                return View("AddEdit", customer); // render the view page and passing customer object to it
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)  // Loads info to the delete page
        {
            var customer = context.Customers.Find(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)   // on the customer. When user click delete button from delete page
        {
            context.Customers.Remove(customer);
            context.SaveChanges();

            //Adding TempData message
            TempData["message"] = $"{customer.FirstName} {customer.LastName} has been deleted.";

            return RedirectToAction("List", "Customer"); // List() method of CustomerController
        }

        // To check if email entered is already in the database. Function call from Models/Customer.cs
        public JsonResult CheckEmail(string email, int CustomerID)
        {
            if (CustomerID == 0) // Adding a customer. So, check if email already exists
            {
                Customer cust = context.Customers.FirstOrDefault(c => c.Email == email);
                if (cust == null)   // email does not exist
                    return Json(true);    
                else
                    return Json($"{email} already exists.");
            }
            else  // Editing a customer. Don't check email              
                return Json(true);

            
        }


    } //CustomerController class
} //namespace
