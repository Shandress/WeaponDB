using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using WeaponDB.Data;
using WeaponDB.Data.Weapon_stuff;
using System.Drawing;
using WeaponDB.Logic;
using Wintellect.Sterling.Database;
using Wintellect.Sterling;
using Wintellect.Sterling.Server.FileSystem;
using System.Windows.Media.Imaging;
using System.Windows;



namespace WeaponDB
{
    class Test
    {
        public static void test()
        {
            SterlingEngine engine = new SterlingEngine();
            engine.Activate();
            var dbInstance = engine.SterlingDatabase.RegisterDatabase<WeaponDBInstance>(
                new FileSystemDriver("weaponDB/"));
            dbInstance.Purge();
            HTMLParser p = new HTMLParser(@"/wiki/List_of_battle_rifles", @"https://en.wikipedia.org");
            var x = p.ProcessArticle().ToList();


            x.ForEach(w => 
            { 
                dbInstance.Save<Weapon>(w);
 
            });

            var data = dbInstance.Query<Weapon, string>();
            //Weapon weapon = dbInstance.Load<Weapon>("AK-72");
        }

      
        public static BitmapSource CreateBitmapSourceFromBitmap(Bitmap bitmap)
            {
                if (bitmap == null)
                    throw new ArgumentNullException("bitmap");

                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    bitmap.GetHbitmap(),
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
    }
    
}