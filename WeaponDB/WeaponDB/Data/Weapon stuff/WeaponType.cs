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

namespace WeaponDB.Data.Weapon_stuff
{
    /// <summary>
    /// A weapon type e.g. handgun, submachine gun, sniper rifle etc.
    /// </summary>
    class WeaponType: OptimizedPersistable
    {
        
        private string name;
        private List<string> actionTypes;
        private string subType;
        

        /// <summary>
        /// Creates a new WeaponType with a specified name.
        /// </summary>
        /// <param name="name">Weapons type.</param>
        public WeaponType(string name) 
        {
            this.Name = name;
            this.ActionTypes = new List<string>();
        }

        /// <summary>
        /// Creates a new Weapontype.
        /// </summary>
        public WeaponType() 
        {
            this.ActionTypes = new List<string>();
        }

        /// <summary>
        /// Creates a Weapon type that is a copy of a a specified WeaponType.
        /// </summary>
        /// <param name="other">WeaponType to copy.</param>
        public WeaponType(WeaponType other)
        {
            this.Name = other.name;
            this.actionTypes = new List<string>();
            this.ActionTypes = other.ActionTypes;
            this.subType = other.subType;
        }

        /// <summary>
        ///  Weapon's action types e.g. Automatic, Semi-automatic or both.;
        /// </summary>
        public List<string> ActionTypes
        {
            get 
            { 
                return actionTypes; 
            }
            set 
            { 
                Update(); 
                actionTypes = new List<string>(value); 
            }
        }

        /// <summary>
        /// Weapon's type e.g. handgun, submachine gun, sniper rifle.
        /// </summary>
        public string Name
        {
            get 
            { 
                return name; 
            }
            set 
            { 
                Update(); 
                name = value; 
            }
        }

        /// <summary>
        /// Weapon's subType e.g. Anti-Aircraft gun, Heavy machinegun
        /// </summary>
        public string Subtype
        {
            get 
            { 
                return subType; 
            }
            set 
            { 
                Update(); 
                subType = value; 
            }
        }
    }
}
