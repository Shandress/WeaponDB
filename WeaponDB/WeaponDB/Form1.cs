using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using WeaponDB.Data.Weapon_stuff;
using WeaponDB.Data;
using System.Windows.Media.Imaging;

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
            BitmapImage b = new BitmapImage(
                    new Uri(@"https://upload.wikimedia.org/wikipedia/commons/thumb/2/2c/Beretta_38.jpg/220px-Beretta_38.jpg"));
            
            WriteableBitmap wb = new WriteableBitmap(b);
            pictureBox1.Image = BitmapFromWriteableBitmap(wb);
        }


        private Bitmap BitmapFromWriteableBitmap(WriteableBitmap writeBmp)
        {
            Bitmap bmp;
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create((BitmapSource)writeBmp));
                enc.Save(outStream);
                bmp = new Bitmap(outStream);
            }
            return bmp;
        }

       
    }
}
