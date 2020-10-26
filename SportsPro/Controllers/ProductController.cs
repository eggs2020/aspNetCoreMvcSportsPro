using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using SportsPro.DataLayer;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class ProductController : Controller
    {
        private IRepository<Product> data { get; set; }

        public ProductController(IRepository<Product> ctx)
        {
            data = ctx;
        }

        [Route("Products")]  // creating a new url pattern to shorten the default url route
        public IActionResult List()
        {
            var productsOptions = new QueryOptions<Product>();
            return View(data.List(productsOptions)); ;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit", new Product());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var product = data.Get(id);
            return View("AddEdit", product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                    data.Insert(product);
                    TempData["message"] = $"{product.Name} has been added.";
                }
                else
                { 
                    data.Update(product);
                    TempData["message"] = $"{product.Name} has been editted.";
                }
                data.Save();
                return RedirectToAction("List", "Product");
            }
            else
            {
                ViewBag.Action = (product.ProductID == 0) ? "Add" : "Edit"; 
                return View("AddEdit", product);
            }
        }

        // Load the info to the delete function 
        [HttpGet]
        public IActionResult Delete(int id)                 
        {
            var product = data.Get(id);
            return View(product);
        }

        // Serve a POST request from the delete page
        [HttpPost]
        public IActionResult Delete(Product product)        
        {
            data.Delete(product);
            data.Save();
            TempData["message"] = $"{product.Name} has been deleted.";
            
            return RedirectToAction("List", "Product");
        }
    }
}
