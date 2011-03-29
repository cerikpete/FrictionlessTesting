using System.Linq;

namespace Data
{
    public interface IRepository
    {
        /// <summary>
        /// Gets an IQueryable for the specified type
        /// </summary>
        /// <typeparam name="T">The type of object to query</typeparam>
        /// <returns>A query to start aggregating</returns>
        IQueryable<T> All<T>();

        /// <summary>
        /// Saves the specified object to the database.
        /// </summary>
        /// <typeparam name="T">They type of object (should be infered)</typeparam>
        /// <param name="entity">The object to save.</param>
        T Save<T>(T entity);

        /// <summary> Deletes the specified object from the database.</summary>
        /// <typeparam name="T">They type of object (should be infered)</typeparam>
        ///  <param name="entity">Object to delete</param>
        void Delete<T>(T entity);

        /// <summary>Retrieves an object from the database using the specified Id.</summary>
        /// <typeparam name="T">They type of object</typeparam>
        /// <param name="id">The id.</param>
        /// <returns> An instance of the specified object with the specified Id.</returns>
        /// <remarks>This method will not return a proxy object; it always returns the real object.</remarks>
        T Get<T>(object id);

        /// <summary>Retrieves an object from the database using the specified Id.</summary>
        /// <typeparam name="T">They type of object</typeparam>
        /// <param name="id">The id of the object to retrieve</param>
        /// <returns>An instance of the specified object with the specified Id.</returns>
        /// <remarks>This method will return a proxy if the object is not already loaded  in the session.</remarks>
        T Load<T>(object id);
    }
}