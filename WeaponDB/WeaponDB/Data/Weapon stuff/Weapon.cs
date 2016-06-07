using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityDb.Session;
using VelocityDb;
using WeaponDB.Data.Weapon_stuff;
using System.Drawing;
using VelocityDb.Collection;
using System.Drawing.Imaging;

namespace WeaponDB.Data.Weapon_stuff
{
    /// <summary>
    /// Represents a firearm.
    /// </summary>
    class Weapon: OptimizedPersistable
    {
        private string name;
        private string cartridge; // TO DO: make it a list bc there can be more than one.
        private Country country;
        private Date year;
        private Image image;
        private WeaponType weaponType;
        private string manufacturer;

        /// <summary>
        /// Initializes a new instance of a Weapon object.
        /// </summary>
        public Weapon() { }
        

        /// <summary>
        /// A weapon's name e.g. AK-47.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { Update(); name = value; }
        }
        
        /// <summary>
        /// A weapon's country of origin. Represented by an object 
        /// that hold country's name and flag image,
        /// </summary>
        public Country Country
        {
            get { return country; }
            set { Update(); country = value; }
        }

        /// <summary>
        /// TO DO: make it a list bc there can be more than one.
        /// </summary>
        public string Cartridge
        {
            get { return cartridge; }
            set { Update(); cartridge = value;  }
        }

        /// <summary>
        /// Weapon's year or decade of origin e.g. 2001 or 1990s.
        /// </summary>
        public string Year
        {
            get { return year.ToString(); }
            set { Update(); year = new Date(value); }
        }

        /// <summary>
        /// An image of a weapon.
        /// </summary>
        public Image Image
        {
            get { return image; }
            set 
            { 
                Update();
                image = new Bitmap(value); 
            }
        }

        /// <summary>
        /// A weapon type e.g Assault rifle, Machine gun. 
        /// Contains info about weapon's 
        ///     1. Action type e.g. Automatic, Semi-automatic;
        ///     2. SubType e.g. Anti-Aircraft gun, Heavy machinegun
        /// </summary>
        public WeaponType WeaponType
        {
            get { return weaponType; }
            set { Update(); weaponType = new WeaponType(value); }
        }

        /// <summary>
        /// Weapon's manufacturing company.
        /// </summary>
        public string Manufacturer
        {
            get { return manufacturer; }
            set { Update(); manufacturer = value; }
        }

    }
}
