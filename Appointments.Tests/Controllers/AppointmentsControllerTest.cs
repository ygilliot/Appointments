using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Appointments.Api.Models;
using Appointments.Api.Repositories;
using Moq;
using Appointments.Tests.Mocks;
using Appointments.Api.Controllers;
using Appointments.Api.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Appointments.Tests.Controllers {
    [TestClass]
    public class AppointmentsControllerTest {
        [TestMethod]
        public void GetAppointments() {
            var repo = new Mock<IRepository<Appointment>>();
            // Arrange
            AppointmentRepository rep = new AppointmentRepository();
            PeopleRepository peopleRep = new PeopleRepository();
            AppointmentsController controller = new AppointmentsController(rep.Repo, peopleRep.Repo);

            // Act
            IEnumerable<AppointmentDTO> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Count());
            Assert.AreEqual("Escobar Gaviria", result.ElementAt(1).Client.LastName);
            Assert.AreEqual("White", result.ElementAt(2).Client.LastName);
            Assert.AreEqual(result.ElementAt(0).Client.UserName, result.ElementAt(0).Client.Email);
            Assert.AreNotEqual(result.ElementAt(0).StartDate, result.ElementAt(0).EndDate);
            CollectionAssert.AllItemsAreUnique(result.ToList());
        }

        [TestMethod]
        public void GetAppointmentsByCollaborater() {
            var repo = new Mock<IRepository<Appointment>>();
            // Arrange
            AppointmentRepository rep = new AppointmentRepository();
            PeopleRepository peopleRep = new PeopleRepository();
            AppointmentsController controller = new AppointmentsController(rep.Repo, peopleRep.Repo);

            // Act
            IEnumerable<AppointmentDTO> result = controller.Get("admin@admin.com");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(6, result.Count());
            Assert.AreEqual("Escobar Gaviria", result.ElementAt(1).Client.LastName);
            Assert.AreEqual("White", result.ElementAt(2).Client.LastName);
            Assert.AreEqual(result.ElementAt(0).Client.UserName, result.ElementAt(0).Client.Email);
            Assert.AreNotEqual(result.ElementAt(0).StartDate, result.ElementAt(0).EndDate);
            CollectionAssert.AllItemsAreUnique(result.ToList());
        }

        [TestMethod]
        public void CreateAppointment() {
            var repo = new Mock<IRepository<Appointment>>();
            // Arrange
            AppointmentRepository rep = new AppointmentRepository();
            PeopleRepository peopleRep = new PeopleRepository();
            AppointmentsController controller = new AppointmentsController(rep.Repo, peopleRep.Repo);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            var testAppointment = new AppointmentDTO() {
                Client = new PersonDTO(peopleRep.Repo.All().FirstOrDefault(o => o.ApplicationUser.UserName == "pablo.escobar@coca.in")),
                Collaborater = new PersonDTO(peopleRep.Repo.All().FirstOrDefault(o => o.ApplicationUser.UserName == "admin@admin.com")),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(2),
                Status = AppointmentStatus.Pending
            };

            // Act
            IHttpActionResult result = controller.Post("admin@admin.com", testAppointment);
            var contentResult = result as OkNegotiatedContentResult<AppointmentDTO>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(testAppointment.StartDate, contentResult.Content.StartDate);
        }
    }
}
