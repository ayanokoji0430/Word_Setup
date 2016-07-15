using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Word_Setup.Models
{
    class Image_Generate
    {
         
        public BitmapImage bmp_gen(string filePath){
            BitmapImage bmp=new BitmapImage();
            bmp.BeginInit();
            bmp.UriSource = new Uri("../" + filePath, UriKind.Relative);
            bmp.EndInit();
            return bmp;
        } 

    }
}
