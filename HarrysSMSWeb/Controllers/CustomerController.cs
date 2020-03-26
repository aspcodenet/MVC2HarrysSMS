using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HarrysSMSWeb.Events;
using HarrysSMSWeb.Migrations;
using HarrysSMSWeb.Models;
using HarrysSMSWeb.Services;
using HarrysSMSWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HarrysSMSWeb.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEventChannel _eventChannel;

        public CustomerController(ICustomerRepository customerRepository, IEventChannel eventChannel)
        {
            _customerRepository = customerRepository;
            _eventChannel = eventChannel;
        }
        // GET: Customer
        public ActionResult Index()
        {
            var viewModel = new CustomerListViewModel();
            viewModel.Customers.AddRange(_customerRepository.GetAll().Select(r=>new CustomerListViewModel.Customer
            {
                Namn    = r.Name,
                PersonNummer = r.PersonNummer,
                Id = r.CustomerId
            }));
            return View(viewModel);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            var viewModel = new CustomerEditViewModel();
            return View(viewModel);
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(_customerRepository.GetByPersonNummer(model.PersonNummer) != null)
                    ModelState.AddModelError("PersonNummer","Finns redan");                     
                else
                {
                    _customerRepository.Add(new Customer {PersonNummer =  model.PersonNummer, Name = model.Namn});
                    _eventChannel.Publish(new NewCustomerCreated{ PersonNummer = model.PersonNummer});
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}