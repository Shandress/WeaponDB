using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VelocityDb.Session;
using VelocityDb;
using System.Diagnostics;
using WeaponDB.Data;
using WeaponDB.Data.Weapon_stuff;
using System.Drawing;
using WeaponDB.Logic;


namespace WeaponDB
{
    class Test
    {
        public static void test()
        {
            using (SessionNoServer session = new SessionNoServer(Constants.SystemDir))
            {
                HTMLParser p = new HTMLParser(@"/wiki/Lists_of_weapons", @"https://en.wikipedia.org");
                var x = p.ProcessData();
           
                session.BeginUpdate();
                foreach(Weapon w in x)
                {
                    session.Persist(w);
                }
                session.Commit();
            }
        }
       
  }
}