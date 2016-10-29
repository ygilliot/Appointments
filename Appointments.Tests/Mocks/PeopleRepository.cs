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
    /// Gives a mock for repository of Person
    /// </summary>
    public class PeopleRepository {

        #region Properties
        private Mock<IRepository<Person>> repo { get; set; }
        /// <summary>
        /// The Mocked repository
        /// </summary>
        public IRepository<Person> Repo { get { return repo.Object; } }

        internal static IQueryable<Person> basePeople = new List<Person>() {
            new Person() { Id = "9b38d159-bb9c-41b5-b8cd-d374e9b2ee2b", FirstName ="Super", LastName ="Admin", Gender= "Mr.", Address = new UserAddress() { Id = "9b38d159-bb9c-41b5-b8cd-d374e9b2ee2b", City = "Monaco", Country ="Monaco",  }, ApplicationUser = new ApplicationUser() { Id="9b38d159-bb9c-41b5-b8cd-d374e9b2ee2b", UserName = "admin@admin.com", Email = "admin@admin.com", PhoneNumber = "012345678" } },
            new Person() { Id = "6077aed6-f2e0-4717-9a62-ee80d201d580", FirstName ="Jean", LastName ="Dujardin", Gender= "Mr.", Address = new UserAddress() { Id = "6077aed6-f2e0-4717-9a62-ee80d201d580", City = "Monaco", Country ="Monaco",  }, ApplicationUser = new ApplicationUser() { Id="6077aed6-f2e0-4717-9a62-ee80d201d580", UserName = "jean@dujard.in", Email = "jean@dujard.in", PhoneNumber = "012345678" } },
            new Person() { Id = "ed980470-9c0f-47f7-a967-0adc9eb2325e", FirstName ="Pablo Emilio", LastName ="Escobar Gaviria", Gender= "Mr.", Address = new UserAddress() { Id = "ed980470-9c0f-47f7-a967-0adc9eb2325e", City = "Medellín", Country ="Colombia",  }, ApplicationUser = new ApplicationUser() { Id="ed980470-9c0f-47f7-a967-0adc9eb2325e", UserName = "pablo.escobar@coca.in", Email = "pablo.escobar@coca.in", PhoneNumber = "012345678" } },
            new Person() { Id = "71c2aa2f-33ee-4616-a4fb-6a06b65c3d1e", FirstName ="Walter", LastName ="White", Gender= "Mr.", Address = new UserAddress() { Id = "71c2aa2f-33ee-4616-a4fb-6a06b65c3d1e", City = "Albuquerque", State="New Mexico", Country ="United States",  }, ApplicationUser = new ApplicationUser() { Id="71c2aa2f-33ee-4616-a4fb-6a06b65c3d1e", UserName = "walter.white@heisen.brg", Email = "walter.white@heisen.brg", PhoneNumber = "012345678" } },
            new Person() { Id = "c88cde2d-75ed-4c24-afbe-0b77136ceac1", FirstName ="Jesse", LastName ="Pinkman", Gender= "Mr.", Address = new UserAddress() { Id = "c88cde2d-75ed-4c24-afbe-0b77136ceac1", City = "Albuquerque", State="New Mexico", Country ="United States",  }, ApplicationUser = new ApplicationUser() { Id="c88cde2d-75ed-4c24-afbe-0b77136ceac1", UserName = "jesse@pink.man", Email = "jesse@pink.man", PhoneNumber = "012345678" } },
            new Person() { Id = "57941c5a-4ce0-4d50-98d7-581da19f636a", FirstName ="Enoch", LastName ="Thompson", Gender= "Mr.", Address = new UserAddress() { Id = "57941c5a-4ce0-4d50-98d7-581da19f636a", City = "Atlantic City", State="New Jersey", Country ="United States",  }, ApplicationUser = new ApplicationUser() { Id="57941c5a-4ce0-4d50-98d7-581da19f636a", UserName = "nucky@thomps.on", Email = "nucky@thomps.on", PhoneNumber = "012345678" } },
            new Person() { Id = "a3fafdb0-44e4-4644-89db-0a70f64fa55a", FirstName ="Tony", LastName ="Soprano", Gender= "Mr.", Address = new UserAddress() { Id = "a3fafdb0-44e4-4644-89db-0a70f64fa55a", City = "New York City", State="New Jersey", Country ="United States",  }, ApplicationUser = new ApplicationUser() { Id="a3fafdb0-44e4-4644-89db-0a70f64fa55a", UserName = "tony@sopra.no", Email = "tony@sopra.no", PhoneNumber = "012345678" } }
        }.AsQueryable();
        #endregion

        #region Constructors
        /// <summary>
        /// Initialize Repository
        /// </summary>
        public PeopleRepository() {
            this.repo = new Mock<IRepository<Person>>();

            //Mock All() method
            this.repo.Setup(x => x.All()).Returns(basePeople);

            //Mock Add() method
            this.repo.Setup(x => x.Add(It.IsAny<Person>()));
        }
        #endregion

    }
}
