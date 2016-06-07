using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityDb.Session;
using VelocityDb;

namespace WeaponDB.Data
{
    /// <summary>
    /// Used to save data to and get data from the databae.
    /// </summary>
    static class WorkWithData
    {
        /// <summary>
        /// Updates a database specified by the session object by saving a list of objects.
        /// </summary>
        /// <param name="session">DB sesion to update data to</param>
        /// <param name="toSave">A list of objects to save.</param>
        public static void UpdateDB(SessionNoServer session, IEnumerable<object> toSave)
        {
            using (session = new SessionNoServer(Constants.SystemDir))
            {               
                session.BeginUpdate();
                foreach(object obj in toSave)
                {
                    session.Persist(obj);
                }
                session.Commit();
            } 
        }

        /// <summary>
        /// Updates a database specified by the session object by saving a single object.
        /// </summary>
        /// <param name="session">DB sesion to update data to.</param>
        /// <param name="toSave">An object to save.</param>
        public static void UpdateDB(SessionNoServer session, object toSave)
        {
            using (session = new SessionNoServer(Constants.SystemDir))
            {
                session.BeginUpdate();
                session.Persist(toSave);
                session.Commit();
            }
        }

        /// <summary>
        /// Retrieves all objects of specified type from the DB.
        /// </summary>
        /// <typeparam name="T">Type of objects to retrieve.</typeparam>
        /// <param name="session">DB sesion to retrieve data from.</param>
        /// <returns>All retrieved objects.</returns>
        public static IEnumerable<T> Retrieve<T>(SessionNoServer session)
        {
            IEnumerable<T> res = new List<T>();
            using (session = new SessionNoServer(Constants.SystemDir))
            {
                session.BeginRead();
                bool b = session.InTransaction;
                res = session.AllObjects<T>();
                session.Commit();
                b = session.InTransaction;
            }

            return res;
        }
    }
}
