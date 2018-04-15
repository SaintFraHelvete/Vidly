using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private VidlyDBContext _context;

        public CustomersController()
        {
            _context = new VidlyDBContext();
        }

        protected override void Dispose(bool disposing)
        {
            //base.Dispose(disposing);
            _context.Dispose();
        }

        //List<Customer> _customers = new List<Customer>
        //    {
        //        new Customer { Name = "Customer 1", Id = 1 },
        //        new Customer { Name = "Customer 2", Id = 2 }
        //    };

        // GET: Customers
        public ActionResult Index()
        {
            var viewModel = new CustomersViewModel
            {
                Customers = _context.Customers.Include(c=>c.MembershipType).ToList()
            };

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.SingleOrDefault<Customer>(c => c.Id == id);

            return View(customer);
        }
    }
}