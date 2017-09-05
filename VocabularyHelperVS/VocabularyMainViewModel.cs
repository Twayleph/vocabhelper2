using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VocabularyHelperVS
{
    public class VocabularyMainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand CmdInputNewContent { get; }

        public VocabularyMainViewModel()
        {
            CmdInputNewContent = new DelegateCommand(InputNewContent);
        }

        private void InputNewContent()
        {
            var input = new VocabularyInputWindow();
            input.ShowDialog();
        }
    }
}
