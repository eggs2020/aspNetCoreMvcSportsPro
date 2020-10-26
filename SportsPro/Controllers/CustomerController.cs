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
    [Authorize (Roles = "Admin")]
    public class CustomerController : Controller
    {
        private ISportsProUnitOfWork data { get; set; }
        public CustomerController(ISportsProUnitOfWork ctx)
        {
            data = ctx;
        }

        [Route("Customers")]
        public IActionResult List()
        {
            var custOptions = new QueryOptions<Customer>();
            var customers = data.Customers.List(custOptions);
            return View(customers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            //HttpContext.Session.SetString("action", "Add");
            ViewBag.Action = "Add";
            
            var countryOptions = new QueryOptions<Country> { OrderBy = c => c.Name };
            ViewBag.Countries = data.Countries.List(countryOptions);
            return View("AddEdit", new Customer());
        }

        // Load data to the edit page when user click "add" button on customer list page
        [HttpGet]
        public IActionResult Edit(int id)   
        {
            //HttpContext.Session.SetString("action", "Edit");
            ViewBag.Action = "Edit";
           
            var countryOptions = new QueryOptions<Country> { OrderBy = c => c.Name };
            ViewBag.Countries = data.Countries.List(countryOptions);

            var customer = data.Customers.Get(id);
            return View("AddEdit", customer);
        }

        // Send revised-data from edit page
        [HttpPost]
        public IActionResult Edit(Customer customer)    
        {
            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                {
                    data.Customers.Insert(customer);
                    TempData["message"] = $"{customer.FullName} has been added.";
                }
                else
                {
                    data.Customers.Update(customer); 
                    TempData["message"] = $"{customer.FullName} has been editted.";
                }

                data.Customers.Save();
                return RedirectToAction("List", "Customer");  // List() method of CustomerController
            }
            else
            {
                // message for validation summary
                ModelState.AddModelError(string.Empty,"There are errors in the form.");
                
                ViewBag.Action = (customer.CustomerID == 0) ? "Add" : "Edit";
                var countryOptions = new QueryOptions<Country> { OrderBy = c => c.Name };
                ViewBag.Countries = data.Countries.List(countryOptions);
                return View("AddEdit", customer); // render the view page and passing customer object to it
            }
        }

        // Loads data to the delete page
        [HttpGet]
        public IActionResult Delete(int id)  
        {
            var customer = data.Customers.Get(id);
            return View(customer);
        }

        // delete the record in DB based on data on delete page
        [HttpPost]
        public IActionResult Delete(Customer customer)   
        {
            data.Customers.Delete(customer);
            data.Customers.Save();

            TempData["message"] = $"{customer.FirstName} {customer.LastName} has been deleted.";

            return RedirectToAction("List", "Customer"); // List() method of CustomerController
        }

        
        // To check if email entered is already in the database. Function call from Models/Customer.cs
        public JsonResult CheckEmail(string email, int CustomerID)
        {
            if (CustomerID == 0) // Adding a customer. So, check if email already exists
            {
                //Customer cust = context.Customers.FirstOrDefault(c => c.Email == email);
                var custOptions = new QueryOptions<Customer>();
                var customers = data.Customers.List(custOptions);
                if (customers == null)   // email does not exist
                    return Json(true);    
                else
                    return Json($"{email} already exists.");
            }
            else  // Editing a customer. Don't check email              
                return Json(true);   
        }

    } //Controller class
} //namespace
