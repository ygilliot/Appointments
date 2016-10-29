using Appointments.Api.Models;
using Appointments.Api.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointments.Tests.Mocks {
    /// <summary>
    /// Gives a mock for repository of Appointment
    /// </summary>
    public class AppointmentRepository {
        #region Properties
        private Mock<IRepository<Appointment>> repo { get; set; }

        /// <summary>
        /// The Mocked repository
        /// </summary>
        public IRepository<Appointment> Repo { get { return repo.Object; } }

        private static IQueryable<Appointment> baseAppointments = new List<Appointment>() {
            new Appointment() {
                Id = 1,
                ClientId = "6077aed6-f2e0-4717-9a62-ee80d201d580",
                Client = PeopleRepository.basePeople.FirstOrDefault(p=> p.ApplicationUser.UserName == "jean@dujard.in"),
                CollaboraterId = "9b38d159-bb9c-41b5-b8cd-d374e9b2ee2b",
                Collaborater = PeopleRepository.basePeople.FirstOrDefault(p=> p.ApplicationUser.UserName == "admin@admin.com"),
                StartDate = DateTime.UtcNow.AddHours(2),
                EndDate = DateTime.UtcNow.AddHours(3),
                Status = AppointmentStatus.Pending
            },
            new Appointment() {
                Id = 2,
                ClientId = "ed980470-9c0f-47f7-a967-0adc9eb2325e",
                Client = PeopleRepository.basePeople.FirstOrDefault(p=> p.ApplicationUser.UserName == "pablo.escobar@coca.in"),
                CollaboraterId = "9b38d159-bb9c-41b5-b8cd-d374e9b2ee2b",
                Collaborater = PeopleRepository.basePeople.FirstOrDefault(p=> p.ApplicationUser.UserName == "admin@admin.com"),
                StartDate = DateTime.UtcNow.AddHours(5),
                EndDate = DateTime.UtcNow.AddHours(7),
                Status = AppointmentStatus.Valid
            },
            new Appointment() {
                Id = 3,
                ClientId = "71c2aa2f-33ee-4616-a4fb-6a06b65c3d1e",
                Client = PeopleRepository.basePeople.FirstOrDefault(p=> p.ApplicationUser.UserName == "walter.white@heisen.brg"),
                CollaboraterId = "9b38d159-bb9c-41b5-b8cd-d374e9b2ee2b",
                Collaborater = PeopleRepository.basePeople.FirstOrDefault(p=> p.ApplicationUser.UserName == "admin@admin.com"),
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(1).AddHours(3),
                Status = AppointmentStatus.Valid
            },
            new Appointment() {
                Id = 4,
                ClientId = "c88cde2d-75ed-4c24-afbe-0b77136ceac1",
                Client = PeopleRepository.basePeople.FirstOrDefault(p=> p.ApplicationUser.UserName == "jesse@pink.man"),
                CollaboraterId = "9b38d159-bb9c-41b5-b8cd-d374e9b2ee2b",
                Collaborater = PeopleRepository.basePeople.FirstOrDefault(p=> p.ApplicationUser.UserName == "admin@admin.com"),
                StartDate = DateTime.UtcNow.AddDays(1),
                EndDate = DateTime.UtcNow.AddDays(1).AddHours(3),
                Status = AppointmentStatus.Removed
            },
            new Appointment() {
                Id = 5,
                ClientId = "57941c5a-4ce0-4d50-98d7-581da19f636a",
                Client = PeopleRepository.basePeople.FirstOrDefault(p=> p.ApplicationUser.UserName == "nucky@thomps.on"),
                CollaboraterId = "9b38d159-bb9c-41b5-b8cd-d374e9b2ee2b",
                Collaborater = PeopleRepository.basePeople.FirstOrDefault(p=> p.ApplicationUser.UserName == "admin@admin.com"),
                StartDate = DateTime.UtcNow.AddDays(2),
                EndDate = DateTime.UtcNow.AddDays(2).AddHours(2),
                Status = AppointmentStatus.Pending
            },
            new Appointment() {
                Id = 6,
                ClientId = "a3fafdb0-44e4-4644-89db-0a70f64fa55a",
                Client = PeopleRepository.basePeople.FirstOrDefault(p=> p.ApplicationUser.UserName == "tony@sopra.no"),
                CollaboraterId = "9b38d159-bb9c-41b5-b8cd-d374e9b2ee2b",
                Collaborater = PeopleRepository.basePeople.FirstOrDefault(p=> p.ApplicationUser.UserName == "admin@admin.com"),
                StartDate = DateTime.UtcNow.AddDays(3),
                EndDate = DateTime.UtcNow.AddDays(3).AddHours(4),
                Status = AppointmentStatus.Valid
            }
        }.AsQueryable();
        #endregion

        #region Constructors
        /// <summary>
        /// Initialize Repository
        /// </summary>
        public AppointmentRepository() {
            this.repo = new Mock<IRepository<Appointment>>();

            //Mock All() method
            this.repo.Setup(x => x.All()).Returns(baseAppointments);

            //Mock Add() method
            this.repo.Setup(x => x.Add(It.IsAny<Appointment>()));
        }
        #endregion
    }
}
