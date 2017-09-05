using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace VocabularyHelperVS
{
    public class VocabularyInputViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string FilePath;

        public ObservableCollection<VocabularyWord> Words { get; private set; }

        public VocabularyInputViewModel()
        {
            Words = new ObservableCollection<VocabularyWord>();
        }

        public VocabularyInputViewModel(ObservableCollection<VocabularyWord> words, string filePath)
        {
            Words = words;
            FilePath = filePath;
        }

        public DelegateCommand Save => new DelegateCommand(SaveWords);

        private void SaveWords()
        {
            var filePath = GetSaveFilePath();

            if (filePath != null)
            {
                var ser = new XmlSerializer(Words.GetType());
                using (var writer = new StreamWriter(filePath))
                {
                    ser.Serialize(writer, Words);
                }
            }
        }

        private string GetSaveFilePath()
        {
            if (FilePath == null)
            {
                var dialog = new SaveFileDialog()
                {
                    InitialDirectory = Environment.CurrentDirectory,
                    Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                    DefaultExt = "xml"
                };

                if (dialog.ShowDialog() == true)
                {
                    FilePath = dialog.FileName;
                }
            }

            return FilePath;
        }
    }
}
