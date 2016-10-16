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

        private static IQueryable<Person> basePeople = new List<Person>() {
            new Person() { Id = "admin@admin.com", FirstName ="Super", LastName ="Admin", Gender= "Mr.", Address = new UserAddress() { Id = "admin@admin.com", City = "Monaco", Country ="Monaco",  } },
            new Person() { Id = "jean@dujard.in", FirstName ="Jean", LastName ="Dujardin", Gender= "Mr.", Address = new UserAddress() { Id = "jean@dujard.in", City = "Monaco", Country ="Monaco",  } },
            new Person() { Id = "pablo.escobar@coca.in", FirstName ="Pablo Emilio", LastName ="Escobar Gaviria", Gender= "Mr.", Address = new UserAddress() { Id = "pablo.escobar@coca.in", City = "Medellín", Country ="Colombia",  } },
            new Person() { Id = "walter.white@heisen.brg", FirstName ="Walter", LastName ="White", Gender= "Mr.", Address = new UserAddress() { Id = "walter.white@heisen.brg", City = "Albuquerque", State="New Mexico", Country ="United States",  } },
            new Person() { Id = "jesse@pink.man", FirstName ="Jesse", LastName ="Pinkman", Gender= "Mr.", Address = new UserAddress() { Id = "jesse@pink.man", City = "Albuquerque", State="New Mexico", Country ="United States",  } },
            new Person() { Id = "nucky@thomps.on", FirstName ="Enoch", LastName ="Thompson", Gender= "Mr.", Address = new UserAddress() { Id = "nucky@thomps.on", City = "Atlantic City", State="New Jersey", Country ="United States",  } },
            new Person() { Id = "tony@sopra.no", FirstName ="Tony", LastName ="Soprano", Gender= "Mr.", Address = new UserAddress() { Id = "tony@sopra.no", City = "New York City", State="New Jersey", Country ="United States",  } }
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
