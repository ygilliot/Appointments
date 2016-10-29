using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appointments.Api.Repositories {
    /// <summary>
    /// Generic Repository Interface
    /// </summary>
    /// <typeparam name="T">Type of the repository</typeparam>
    public interface IRepository<T> where T : class {
        /// <summary>
        /// Insert a new element in Repository
        /// </summary>
        /// <param name="entity">element to add</param>
        void Add(T entity);

        /// <summary>
        /// Delete an element from Repository
        /// </summary>
        /// <param name="entity">element to delete</param>
        void Delete(T entity);

        /// <summary>
        /// Delete an element by its identifier
        /// </summary>
        /// <param name="id">identifier of the element</param>
        void Delete(int id);

        /// <summary>
        /// Updates an existing element
        /// </summary>
        /// <param name="entity">element with updated values</param>
        void Update(T entity);

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
        T GetById(dynamic Id);

        /// <summary>
        /// Returns all entities
        /// </summary>
        /// <returns>Queryable list of entities</returns>
        IQueryable<T> All();

        /// <summary>
        /// Finds all elements matching the predicate
        /// </summary>
        /// <param name="predicate">predicate to filter entities</param>
        /// <returns></returns>
        IEnumerable<T> Find(Func<T, bool> predicate);
    }
}