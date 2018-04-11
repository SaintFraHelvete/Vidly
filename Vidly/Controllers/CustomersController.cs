using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        List<Customer> _customers = new List<Customer>
            {
                new Customer { Name = "Customer 1", Id = 1 },
                new Customer { Name = "Customer 2", Id = 2 }
            };

        // GET: Customers
        public ActionResult Index()
        {
            var viewModel = new CustomersViewModel
            {
                Customers = _customers
            };

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var customer = (from c in _customers
                           where c.Id == id
                           select c).SingleOrDefault<Customer>();

            return View(customer);
        }
    }
}