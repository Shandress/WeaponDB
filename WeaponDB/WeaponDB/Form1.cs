using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VelocityDb.Session;
using VelocityDb;
using System.Net;
using System.IO;
using WeaponDB.Data.Weapon_stuff;
using WeaponDB.Data;

namespace WeaponDB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (SessionNoServer session = new SessionNoServer(Constants.SystemDir))
            {
                IEnumerable<WeaponType> weaponTypes = WorkWithData.Retrieve<WeaponType>(session);
                IEnumerable<Weapon> weapons = WorkWithData.Retrieve<Weapon>(session);
            }
        }

       
    }
}
