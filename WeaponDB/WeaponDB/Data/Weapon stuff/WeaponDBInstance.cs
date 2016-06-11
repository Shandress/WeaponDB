using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.Sterling.Database;

namespace WeaponDB.Data.Weapon_stuff
{
    class WeaponDBInstance : BaseDatabaseInstance
    {
        public override string Name
        {
            get
            {
                return "Weapon DataBase";
            }
        }

        protected override List<ITableDefinition> RegisterTables()
        {
            return new List<ITableDefinition> { CreateTableDefinition<Weapon, string>(w => w.Name) };
        }
    }
}
