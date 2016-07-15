using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Word_Setup.Models;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace Word_Setup.ViewModels
{
    class ViewModel : ViewModelBase
    {
        private ObservableCollection<Word> wordlist;
        public  ObservableCollection<Word> WordList{
            get { return wordlist; }
            set
            {
                wordlist = value;
                NotifyPropertyChanged("WordList");
            }            
        }

        private RelayCommand add_command;
        public RelayCommand add_Command
        {
            get { return add_command = add_command ?? new RelayCommand(Word_Item_Add); }
        }             

        private RelayCommand show_command;
        public RelayCommand show_Command
        {
            get { return show_command = show_command ?? new RelayCommand(Word_Item_Num); }
        }

        public ViewModel()
        {
            wordlist = new ObservableCollection<Word>();
        }

        public void Word_Item_Add()
        {
            var image_gen=new Image_Generate();
            WordList.Add(new Word { wordStr = "",ViewModel=this,wordImage=image_gen.bmp_gen("no_image.jpg")});
        }

        public void Word_Item_Add(string s,BitmapImage image)
        {
            WordList.Add(new Word { wordStr = s, ViewModel = this ,wordImage=image});
        }

        public void Word_Item_Num()
        {
            FIleIO f_io=new FIleIO();

            foreach (var word in WordList.ToList())
            {
                if (word.wordStr == "") WordList.Remove(word);
            }
            
            f_io.File_Export(WordList);
            MessageBox.Show("設定が保存されました");
        }
    }

}
