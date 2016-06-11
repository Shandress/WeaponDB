using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WeaponDB.Data.Weapon_stuff
{
    /// <summary>
    /// Represents some info about a country: it's name and a flag image.
    /// </summary>
    class Country
    {
        /// <summary>
        /// Creates a new Country instance with specified name having a specified flag image.
        /// </summary>
        /// <param name="name">Name of the country.</param>
        /// <param name="flagRef">Flag of the country.</param>
        public Country(string name, string flagRef)
        {
            this.Name = name;
            this.FlagReference = flagRef;
        }

        public Country() : this(string.Empty, string.Empty) { }

        /// <summary>
        /// Name of the country.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A path to the flag of the country.
        /// </summary>
        public string FlagReference { get; set; }

    }
}
