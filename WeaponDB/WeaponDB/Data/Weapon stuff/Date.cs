using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeaponDB.Exceptions;

namespace WeaponDB.Data.Weapon_stuff
{
    /// <summary>
    /// Represents a year or a decade e.g. 1890 or 1980s.
    /// </summary>
    class Date
    {
        private uint year;
        private bool decade;

        /// <summary>
        /// Creates a Date object from a string.
        /// </summary>
        /// <param name="input">String representing a year or decade.</param>
        public Date(string input)
        {
            Decade = input.EndsWith("s");
            try
            {
                int idx = input.Count() - 1;
                Year = (Decade)
                    ? UInt32.Parse(input.Remove(idx))
                    : UInt32.Parse(input);
            }
            catch(ArgumentException)
            {
                throw new UnexpectedInputException("Invalid date string.");
            }

        }
        public Date() : this(string.Empty) { }

        public uint Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
            }
        }

        public bool Decade
        {
            get
            {
                return decade;
            }
            set
            {
                decade = value;
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
