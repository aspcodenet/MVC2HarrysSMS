using System.Collections.Generic;
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
            sut = new CustomerController(customerRepository.Object,eventChannel.Object);
        }

        [TestMethod]
        public void IndexShouldReturnCorrectView()
        {
            customerRepository.Setup(r => r.GetAll()).Returns(new List<Customer>());
            
            var result = sut.Index() as ViewResult;
            Assert.IsNull(result.ViewName); //Same as Action
            //Assert.AreEqual("Red", result.ViewName); //om annan än actionname
        }

        [TestMethod]
        public void IndexShouldUseCorrectViewModel()
        {
            customerRepository.Setup(r => r.GetAll()).Returns(new List<Customer>());

            var result = sut.Index() as ViewResult;
            Assert.IsInstanceOfType(result.Model, typeof(CustomerListViewModel));
        }

        [TestMethod]
        public void IndexShouldUseListOfCustomers()
        {
            customerRepository.Setup(r => r.GetAll()).Returns(new List<Customer>
            {
                new Customer { CustomerId = 1, PersonNummer = "123",Name="Hello"},
                new Customer { CustomerId = 2, PersonNummer = "222", Name = "Kajsa Anka" }
            });

            var result = sut.Index() as ViewResult;
            var model = result.Model as CustomerListViewModel;
            Assert.AreEqual(2, model.Customers.Count);
            Assert.AreEqual(1, model.Customers[0].Id);
            Assert.AreEqual("222", model.Customers[1].PersonNummer);
            Assert.AreEqual("Kajsa Anka", model.Customers[1].Namn);
        }


        [TestMethod]
        public void CreateShouldReturnSameViewWhenValidationError()
        {
            sut.ModelState.AddModelError("x", "error");
            var result = sut.Create(new CustomerEditViewModel ()) as ViewResult;
            Assert.IsNull(result.ViewName); //Same as Action
        }
        [TestMethod]
        public void CreateShouldNotSaveWhenValidationError()
        {
            customerRepository.Setup(r => r.Add(It.IsAny<Customer>())).Returns(1);
            sut.ModelState.AddModelError("x", "error");
            var result = sut.Create(new CustomerEditViewModel()) as ViewResult;
            customerRepository.Verify(r=>r.Add(It.IsAny<Customer>()), Times.Never);
        }
        [TestMethod]
        public void CreateShouldReturnErrorIfAlreadyExisting()
        {
            customerRepository.Setup(r => r.Add(It.IsAny<Customer>())).Returns(1);
            customerRepository.Setup(r => r.GetByPersonNummer(It.IsAny<string>())).Returns(new Customer());
            var result = sut.Create(new CustomerEditViewModel()) as ViewResult;

            customerRepository.Verify(r => r.Add(It.IsAny<Customer>()), Times.Never);
            Assert.IsFalse(sut.ModelState.IsValid);
            Assert.IsNull(result.ViewName); //Same as Action
        }



    }
}
