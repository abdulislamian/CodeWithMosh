using CodeWithMosh.Data;
using CodeWithMosh.Models;
using CodeWithMosh.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeWithMosh.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CustomerController(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();  
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetCustomers(string query = null)
        {
            var customersQuery = dbContext.Customers
                .Include(c => c.MembershipType);

            //if (!String.IsNullOrWhiteSpace(query))
            //    customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            //var customerDtos = customersQuery
            //    .ToList()
            //    .Select(new Customer
            //    {
            //        Id=
            //    });

            return Ok(customersQuery.ToList());
        }
        public IActionResult New()
        {
            var membershipTypes = dbContext.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = dbContext.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (customer.Id == 0)
                dbContext.Customers.Add(customer);
            else
            {
                var customerInDb = dbContext.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }

            dbContext.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }


        public IActionResult Details(int id)
        {
            var customer = dbContext.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        public IActionResult Edit(int id)
        {
            var customer = dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = dbContext.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var customerInDb = dbContext.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                return NotFound();

            dbContext.Customers.Remove(customerInDb);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
