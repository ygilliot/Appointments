using Appointments.Api.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Appointments.Api.Repositories {
    public class GenericRepository<T> : IDisposable, IRepository<T> where T : class {
        #region Members
        /// <summary>
        /// Entity context to use
        /// </summary>
        protected ApplicationDbContext Context { get; set; }
        /// <summary>
        /// Current Entity DbSet
        /// </summary>
        protected DbSet<T> DbSet { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor of the repository
        /// </summary>
        /// <param name="dbContext">Context to use for Entity Framework</param>
        public GenericRepository(ApplicationDbContext dbContext) {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            Context = dbContext;
            DbSet = Context.Set<T>();
        }
        #endregion

        #region Methods implementation
        /// <summary>
        /// Insert a new element in Repository
        /// </summary>
        /// <param name="entity">element to add</param>
        public void Add(T entity) {
            DbSet.Add(entity);
        }

        /// <summary>
        /// Delete an element from Repository
        /// </summary>
        /// <param name="entity">element to delete</param>
        public void Delete(T entity) {
            DbSet.Remove(entity);
        }

        /// <summary>
        /// Delete an element by its identifier
        /// </summary>
        /// <param name="id">identifier of the element</param>
        public void Delete(dynamic id) {
            var entity = DbSet.Find(id);
            DbSet.Remove(entity);
        }

        /// <summary>
        /// Updates an existing element
        /// </summary>
        /// <param name="entity">element with updated values</param>
        public void Update(T entity) {
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Finds an entity with the given primary key values. If an entity with the given
        ///     primary key values exists in the context, then it is returned immediately without
        ///     making a request to the store. Otherwise, a request is made to the store for
        ///     an entity with the given primary key values and this entity, if found, is attached
        ///     to the context and returned. If no entity is found in the context or the store,
        ///     then null is returned.
        /// </summary>
        /// <param name="Id">The value of the primary key for the entity to be found</param>
        /// <returns>The entity found, or null</returns>
        public T GetById(dynamic Id) {
            return DbSet.Find(Id);
        }

        /// <summary>
        /// Returns all entities
        /// </summary>
        /// <returns>Queryable list of entities</returns>
        public IQueryable<T> All() {
            return DbSet.AsQueryable();
        }

        /// <summary>
        /// Finds all elements matching the predicate
        /// </summary>
        /// <param name="predicate">predicate to filter entities</param>
        /// <returns></returns>
        public IEnumerable<T> Find(Func<T, bool> predicate) {
            return DbSet.Where(predicate);
        }

        /// <summary>
        /// Save Changes in database
        /// </summary>
        public void Save() {
            try {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) {
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Dispose the repository
        /// </summary>
        public void Dispose() {
            ((IDisposable)Context).Dispose();
        }
    }
}