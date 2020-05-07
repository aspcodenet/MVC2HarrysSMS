using System.Collections.Generic;
using Grpc.Core;
using HarrysSMSWeb.Controllers;
using HarrysSMSWeb.Events;
using HarrysSMSWeb.Models;
using HarrysSMSWeb.Services;
using HarrysSMSWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Mvc.Tests
{

    
    [TestClass]
    public class CustomerControllerTests
    {
        private CustomerController sut;
        private Mock<ICustomerRepository> customerRepository;
        private Mock<IEventChannel> eventChannel;

        [TestInitialize]
        public void Initialize()
        {
            customerRepository = new Mock<ICustomerRepository>();
            eventChannel = new Mock<IEventChannel>();
            sut = new CustomerController(customerRepository.Object, eventChannel.Object);
        }

        [TestMethod]
        public void Index_should_return_correct_view()
        {
            var result = sut.Index() as ViewResult;
            Assert.IsNull(result.ViewName); //Same as Action
        }


        [TestMethod]
        public void Index_should_use_correct_viewmodel()
        {
            var result = sut.Index() as ViewResult;
            Assert.IsInstanceOfType( result.Model,   typeof(CustomerListViewModel));
        }

        [TestMethod]
        public void Index_should_fill_list_of_customers_in_viewmodel()
        {
            var list = new List<Customer>();
            list.Add(new Customer {Name="Hej",CustomerId = 1});
            list.Add(new Customer { Name = "Hopp", CustomerId = 2 });

            customerRepository.Setup(c => c.GetAll()).Returns(list);

            var result = sut.Index() as ViewResult;
            var model = result.Model as CustomerListViewModel;
            Assert.IsTrue(model.Customers.Count > 0);
        }


        [TestMethod]
        public void Index_should_fill_complete_list_of_customers()
        {
            var list = new List<Customer>();
            list.Add(new Customer { Name = "Hej", CustomerId = 1 });
            list.Add(new Customer { Name = "Hopp", CustomerId = 2 });

            customerRepository.Setup(c => c.GetAll()).Returns(list);

            var result = sut.Index() as ViewResult;
            var model = result.Model as CustomerListViewModel;
            Assert.AreEqual( list.Count,  model.Customers.Count );
        }


        [TestMethod]
        public void Index_should_map_correctly()
        {
            var list = new List<Customer>();
            list.Add(new Customer { Name = "Hej", CustomerId = 1 });
            list.Add(new Customer { Name = "Hopp", CustomerId = 2 });

            customerRepository.Setup(c => c.GetAll()).Returns(list);

            var result = sut.Index() as ViewResult;
            var model = result.Model as CustomerListViewModel;
            Assert.AreEqual("Hej", model.Customers[0].Namn);
            Assert.AreEqual(1, model.Customers[0].Id);
        }




        [TestMethod]
        public void Create_should_return_same_view_when_not_valid()
        {
            sut.ModelState.AddModelError("x","asdasd");
            var result = sut.Create(new CustomerEditViewModel()) as ViewResult;
            Assert.IsNull(result.ViewName); //Same as Action
        }


        [TestMethod]
        public void Create_should_not_save_when_not_valid()
        {
            sut.ModelState.AddModelError("x", "asdasd");
            var result = sut.Create(new CustomerEditViewModel()) as ViewResult;
            customerRepository.Verify(r=>r.Add(It.IsAny<Customer>()), Times.Never);
        }


        [TestMethod]
        public void Create_should_return_error_when_already_existing()
        {
            customerRepository.Setup(r => r.GetByPersonNummer(It.IsAny<string>())).Returns(new Customer());
            var result = sut.Create(new CustomerEditViewModel()) as ViewResult;
            Assert.IsFalse(sut.ModelState.IsValid);
        }


    }
}
