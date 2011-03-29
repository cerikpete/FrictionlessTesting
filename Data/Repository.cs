using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace Data
{
    public class Repository : IRepository, IDisposable
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public IQueryable<T> All<T>()
        {
            return _session.Linq<T>();
        }

        /// <summary>
        /// Saves the specified object to the database.
        /// </summary>
        /// <typeparam name="T">They type of object (should be infered)</typeparam>
        /// <param name="entity">The object to save.</param>
        public T Save<T>(T entity)
        {
            _session.Save(entity);
            _session.Flush();
            return entity;
        }

        /// <summary> Deletes the specified object from the database.</summary>
        /// <typeparam name="T">They type of object (should be infered)</typeparam>
        /// <param name="entity">Object to delete</param>
        public void Delete<T>(T entity)
        {
            _session.Delete(entity);
            _session.Flush();
        }

        /// <summary>Retrieves an object from the database using the specified Id.</summary>
        /// <typeparam name="T">They type of object</typeparam>
        /// <param name="id">The id.</param>
        /// <returns> An instance of the specified object with the specified Id.</returns>
        /// <remarks>This method will not return a proxy object; it always returns the real object.</remarks>
        public T Get<T>(object id)
        {
            return _session.Get<T>(id);
        }

        /// <summary>Retrieves an object from the database using the specified Id.</summary>
        /// <typeparam name="T">They type of object</typeparam>
        /// <param name="id">The id of the object to retrieve</param>
        /// <returns>An instance of the specified object with the specified Id.</returns>
        /// <remarks>This method will return a proxy if the object is not already loaded  in the session.</remarks>
        public T Load<T>(object id)
        {
            return _session.Load<T>(id);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            _session.Dispose();
        }
    }
}