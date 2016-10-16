using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Appointments.Api;
using Appointments.Api.Controllers;
using Appointments.Api.Models;
using Appointments.Api.Repositories;
using Moq;
using Appointments.Tests.Mocks;

namespace Appointments.Api.Tests.Controllers {
    [TestClass]
    public class PeopleControllerTest {
        [TestMethod]
        public void Get() {
            var repo = new Mock<IRepository<Person>>();
            // Arrange
            PeopleRepository rep = new PeopleRepository();
            PeopleController controller = new PeopleController(rep.Repo);

            // Act
            IEnumerable<Person> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.Count());
            Assert.AreEqual("Escobar Gaviria", result.ElementAt(2).LastName);
            Assert.AreEqual("White", result.ElementAt(3).LastName);
        }

        [TestMethod]
        public void GetById() {
            var repo = new Mock<IRepository<Person>>();
            // Arrange
            PeopleRepository rep = new PeopleRepository();
            PeopleController controller = new PeopleController(rep.Repo);

            // Act
            Person result = controller.Get("jesse@pink.man");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Albuquerque", result.Address.City);
        }

        //[TestMethod]
        //public void Post()
        //{
        //    // Arrange
        //    ValuesController controller = new ValuesController();

        //    // Act
        //    controller.Post("value");

        //    // Assert
        //}

        //[TestMethod]
        //public void Put()
        //{
        //    // Arrange
        //    ValuesController controller = new ValuesController();

        //    // Act
        //    controller.Put(5, "value");

        //    // Assert
        //}

        //[TestMethod]
        //public void Delete()
        //{
        //    // Arrange
        //    ValuesController controller = new ValuesController();

        //    // Act
        //    controller.Delete(5);

        //    // Assert
        //}
    }
}
