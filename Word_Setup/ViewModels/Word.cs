using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Word_Setup.ViewModels
{
    class Word:ViewModelBase
    {
        private RelayCommand delete_command;
        public RelayCommand delete_Command
        {
            get { return delete_command = delete_command ?? new RelayCommand(Word_Item_Remove); }
        }

        private RelayCommand browse_command;
        public RelayCommand browse_Command
        {
            get { return browse_command = browse_command ?? new RelayCommand(browse); }
        }

        ViewModel viewModel;
        public ViewModel ViewModel
        {
            set
            {
                viewModel = value;
            }
        }

        private string wordstr;
        private BitmapImage wordimage;

        public string wordStr
        {
            get
            {
                return wordstr;
            }
            set
            {            
                wordstr = value;
                NotifyPropertyChanged("wordStr");
            }
        }
        
        public BitmapImage wordImage
        {
            get
            {
                return wordimage;
            }
            set
            {
                wordimage = value;
                NotifyPropertyChanged("wordImage");
            }
        }       


        public void Word_Item_Remove()
        {
            viewModel.WordList.Remove(this);
            
        }

        public void browse()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.DefaultExt = "*.*";
            ofd.Filter = "画像ファイル|*.bmp;*.gif;*.png;*.jpg";
            BitmapImage m_bitmap = null;
            if (ofd.ShowDialog() == true)
            {
                string filename = ofd.FileName;
                if (m_bitmap != null)
                {
                    m_bitmap = null;
                }

                m_bitmap = new BitmapImage();
                m_bitmap.BeginInit();
                m_bitmap.UriSource = new Uri(filename);
                m_bitmap.EndInit();

                wordImage = m_bitmap;
            }
        }
    }
}
