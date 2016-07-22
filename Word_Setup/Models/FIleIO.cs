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
        private string text_path="word_image.txt";


        public void File_Export(List<Word> word_List){

            DirectoryInfo di = new DirectoryInfo(target_dir);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            using (StreamWriter sw = new StreamWriter(text_path))
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

        public List<Word> File_Import()
        {
            var Import_Collection = new List<Word>();
            var wordlist = new List<string>();
            var img_path = new List<string>();
            var imagelsit = new List<BitmapImage>();


            DirectoryInfo di = new DirectoryInfo(target_dir);
            if (!di.Exists) di = Directory.CreateDirectory(target_dir);

            StreamReader sr = null;
            try
            {
                sr = new StreamReader(text_path);
                while (sr.Peek() >= 0)
                {
                    var str = sr.ReadLine().Split(',');
                    wordlist.Add(str[0]);
                    img_path.Add(str[1]);
                }
            }
            catch
            {
                using (FileStream fs = File.Create("./" + text_path))
                {
                    fs.Close();
                }
                return null;
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }

            foreach (var path in img_path)
            {
                var ig = new Image_Generate();

                if (File.Exists(target_dir + path))
                {
                    FileStream fs = new FileStream(target_dir + path, FileMode.Open, FileAccess.Read);
                    imagelsit.Add(ig.stream_bmp(fs));
                    fs.Close();
                }
                else
                {                 
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    File.Delete(text_path);
                    using (FileStream fs = File.Create("./"+text_path))
                    {
                        fs.Close();
                    }
                    return null;
                }
            }

            for (int i = 0; i < wordlist.Count; i++)
            {
                Import_Collection.Add(new Word {wordStr=wordlist[i],wordImage=imagelsit[i] });
            }

            return Import_Collection;
        }

    }
}
