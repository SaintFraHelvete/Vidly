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
            base.Dispose(disposing);

            _context.Dispose();
        }

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
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault<Customer>(c => c.Id == id);

            return View(customer);
        }

        public ActionResult New() {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(CustomerFormViewModel viewModel)
        {
            if (viewModel.Customer.Id == 0)
                _context.Customers.Add(viewModel.Customer);
            else
            {
                var customerInDB = _context.Customers.Single(c => c.Id == viewModel.Customer.Id);
                customerInDB.Name = viewModel.Customer.Name;
                customerInDB.Birthdate = viewModel.Customer.Birthdate;
                customerInDB.MembershipTypeId = viewModel.Customer.MembershipTypeId;
                customerInDB.IsSubscribedToNewsletter = viewModel.Customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null) 
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}