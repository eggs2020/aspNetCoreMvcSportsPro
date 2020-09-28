using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using SportsPro.Models;

namespace SportsPro.Controllers
{
    public class ProductController : Controller
    {
        private SportsProContext context { get; set; }

        public ProductController(SportsProContext ctx)
        {
            context = ctx;
        }

        [Route("Products")]  // creating a new url pattern to shorten the default url route
        public IActionResult List()
        {
            var products = context.Products.ToList();

            return View(products);
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
            var product = context.Products.Find(id);
            return View("AddEdit", product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductID == 0)
                {
                    context.Products.Add(product);
                    //Adding TempData message
                    TempData["message"] = $"{product.Name} has been added.";
                }
                else
                { 
                    context.Products.Update(product);
                    //Adding TempData message
                    TempData["message"] = $"{product.Name} has been editted.";
                }
                context.SaveChanges();
                return RedirectToAction("List", "Product");
            }
            else
            {
                ViewBag.Action = (product.ProductID == 0) ? "Add" : "Edit";
              // ViewBag.Product = context.Products.OrderBy(g => g.Name).ToList(); 
                return View("AddEdit", product);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)                 // on the integer. Loads the info to the delete function 
        {
            var product = context.Products.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)        // serve a POST request from the delete page
        {
            context.Products.Remove(product);
            context.SaveChanges();
            
            //Adding TempData message
            TempData["message"] = $"{product.Name} has been deleted.";
            
            return RedirectToAction("List", "Product");
        }
    }
}
