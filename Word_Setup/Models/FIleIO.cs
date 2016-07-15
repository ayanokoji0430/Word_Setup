using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Word_Setup.ViewModels;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace Word_Setup.Models
{
    class FIleIO
    {
        private string target_dir="./images/";
        private string filename_template = "word_img";


        public void File_Export(ObservableCollection<Word> word_List){

            DirectoryInfo di = new DirectoryInfo(target_dir);
            if (!di.Exists) di=Directory.CreateDirectory(target_dir);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            using (StreamWriter sw = new StreamWriter("hage.txt"))
            {
                for (int i = 0; i < word_List.Count; i++)
                {
                    string word = word_List[i].wordStr;
                    sw.WriteLine(word+","+filename_template+i+".png");
                }

            }

            for (int i = 0; i < word_List.Count; i++)
            {

                using (FileStream stream = new FileStream(target_dir + filename_template + i + ".png", FileMode.Create))
                {

                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(word_List[i].wordImage));
                    encoder.Save(stream);
                    stream.Close();                   

                }

            }

            

        }
    }
}
