using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityDb.Session;
using VelocityDb;
using WeaponDB.Exceptions;

namespace WeaponDB.Data.Weapon_stuff
{
    /// <summary>
    /// Represents a year or a decade e.g. 1890 or 1980s.
    /// </summary>
    class Date : OptimizedPersistable
    {
        private uint year;
        private bool decade;

        /// <summary>
        /// Creates a Date object from a string.
        /// </summary>
        /// <param name="input">String representing a year or decade.</param>
        public Date(string input)
        {
            decade = input.EndsWith("s");
            try
            {
                int idx = input.Count() - 1;
                year = (decade)
                    ? UInt32.Parse(input.Remove(idx))
                    : UInt32.Parse(input);
            }
            catch(ArgumentException)
            {
                throw new UnexpectedInputException("Invalid date string.");
            }

        }

        public override string ToString()
        {
            return (decade)
                ? year.ToString() + "s"
                : year.ToString();
        }
    }
}
