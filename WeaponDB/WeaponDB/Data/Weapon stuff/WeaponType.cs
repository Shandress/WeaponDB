using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeaponDB.Data.Weapon_stuff;
using System.Drawing;

namespace WeaponDB.Data.Weapon_stuff
{
    /// <summary>
    /// A weapon type e.g. handgun, submachine gun, sniper rifle etc.
    /// </summary>
    class WeaponType
    {

        /// <summary>
        /// Creates a new WeaponType with a specified name.
        /// </summary>
        /// <param name="name">Weapons type.</param>
        public WeaponType(string name) 
        {
            this.Name = name;
            this.ActionTypes = new List<string>();
        }

        public WeaponType() : this(string.Empty) { }

        /// <summary>
        /// Creates a Weapon type that is a copy of a a specified WeaponType.
        /// </summary>
        /// <param name="other">WeaponType to copy.</param>
        public WeaponType(WeaponType other)
        {
            this.Name = other.Name;
            this.ActionTypes = other.ActionTypes;
            this.SubType = other.SubType;
        }

        /// <summary>
        ///  Weapon's action types e.g. Automatic, Semi-automatic or both.;
        /// </summary>
        public List<string> ActionTypes { get; set; }
        

        /// <summary>
        /// Weapon's type e.g. handgun, submachine gun, sniper rifle.
        /// </summary>
        public string Name { get; set; }
        

        /// <summary>
        /// Weapon's subType e.g. Anti-Aircraft gun, Heavy machinegun
        /// </summary>
        public string SubType { get; set; }
       
    }
}
