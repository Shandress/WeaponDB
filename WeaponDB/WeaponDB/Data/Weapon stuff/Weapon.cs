using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeaponDB.Data.Weapon_stuff;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Imaging;


namespace WeaponDB.Data.Weapon_stuff
{
    /// <summary>
    /// Represents a firearm.
    /// </summary>
    class Weapon
    {
        private Date year;

        public Weapon()
        {
            Cartridges = new List<string>();
        }
        public int ID { get; set; }

        /// <summary>
        /// A weapon's name e.g. AK-47.
        /// </summary>
        public string Name { get; set; }
        
        
        /// <summary>
        /// A weapon's country of origin. Represented by an object 
        /// that holds country's name and flag image,
        /// </summary>
        public Country Country { get; set; }

        public WriteableBitmap picture { get; set; }
       

        /// <summary>
        /// TO DO: make it a list bc there can be more than one.
        /// </summary>
        public List<string> Cartridges { get; set; }
        

        /// <summary>
        /// Weapon's year or decade of origin e.g. 2001 or 1990s.
        /// </summary>
        public string Year
        {
            get { return year.ToString(); }
            set 
            {
                year = new Date(value); 
            }
        }

        /// <summary>
        /// Path to an image of a weapon.
        /// </summary>
        public string ImageReference { get; set; }
       

        /// <summary>
        /// A weapon type e.g Assault rifle, Machine gun. 
        /// Contains info about weapon's 
        ///     1. Action type e.g. Automatic, Semi-automatic;
        ///     2. SubType e.g. Anti-Aircraft gun, Heavy machinegun
        /// </summary>
        public WeaponType WeaponType { get; set; }
       

        /// <summary>
        /// Weapon's manufacturing company.
        /// </summary>
        public string Manufacturer { get; set; }
        

    }
}
