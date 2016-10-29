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
using Appointments.Api.Models.DTO;
using System.Web.Http.Results;

namespace Appointments.Api.Tests.Controllers {
    [TestClass]
    public class PeopleControllerTest {
        [TestMethod]
        public void GetPeople() {
            var repo = new Mock<IRepository<Person>>();
            // Arrange
            PeopleRepository rep = new PeopleRepository();
            PeopleController controller = new PeopleController(rep.Repo);

            // Act
            IEnumerable<PersonDTO> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.Count());
            Assert.AreEqual("Escobar Gaviria", result.ElementAt(2).LastName);
            Assert.AreEqual("White", result.ElementAt(3).LastName);
        }

        [TestMethod]
        public void GetPeopleById() {
            var repo = new Mock<IRepository<Person>>();
            // Arrange
            PeopleRepository rep = new PeopleRepository();
            PeopleController controller = new PeopleController(rep.Repo);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            IHttpActionResult result = controller.Get("jesse@pink.man");
            var contentResult = result as OkNegotiatedContentResult<PersonExtendedDTO>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("Albuquerque", contentResult.Content.City);
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
