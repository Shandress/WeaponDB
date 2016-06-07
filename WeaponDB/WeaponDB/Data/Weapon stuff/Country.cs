using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityDb.Session;
using VelocityDb;
using System.Drawing;

namespace WeaponDB.Data.Weapon_stuff
{
    /// <summary>
    /// Represents some info about a country: it's name and a flag image.
    /// </summary>
    class Country: OptimizedPersistable
    {
        string name;
        Image flag;

        /// <summary>
        /// Creates a new Country instance with specified name having a specified flag image.
        /// </summary>
        /// <param name="name">Name of the country.</param>
        /// <param name="flag">Flag of the country.</param>
        public Country(string name, Image flag)
        {
            this.Name = name;
            this.Flag = flag;
        }

        /// <summary>
        /// Name of the country.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { Update(); name = value; }
        }
        /// <summary>
        /// Flag of the country.
        /// </summary>
        public Image Flag
        {
            get { return flag; }
            set { Update(); flag = (Image)value.Clone(); }
        }
    }
}
