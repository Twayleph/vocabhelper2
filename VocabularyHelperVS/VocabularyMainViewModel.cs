using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace VocabularyHelperVS
{
    public class VocabularyMainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DelegateCommand CmdInputNewContent { get; }
        public DelegateCommand CmdEditContent { get; }
        public DelegateCommand CmdQuiz { get; }

        public VocabularyMainViewModel()
        {
            CmdInputNewContent = new DelegateCommand(InputNewContent);
            CmdEditContent = new DelegateCommand(EditContent);
            CmdQuiz = new DelegateCommand(Quiz);
        }

        private void InputNewContent()
        {
            var input = new VocabularyInputWindow();
            input.DataContext = new VocabularyInputViewModel();
            input.ShowDialog();
        }

        private void EditContent()
        {
            var words = LoadWords();

            if (words != null)
            {
                var edit = new VocabularyInputWindow();
                edit.DataContext = new VocabularyInputViewModel(words.Item2, words.Item1);
                edit.ShowDialog();
            }
        }

        private string GetLoadFilePath()
        {
            var dialog = new OpenFileDialog()
            {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                DefaultExt = "xml"
            };

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }

            return null;
        }

        private Tuple<string, ObservableCollection<VocabularyWord>> LoadWords()
        {
            try
            {
                var filePath = GetLoadFilePath();

                if (filePath != null)
                {
                    var ser = new XmlSerializer(typeof(ObservableCollection<VocabularyWord>));
                    using (var reader = new StreamReader(filePath))
                    {
                        return new Tuple<string, ObservableCollection<VocabularyWord>>(filePath, (ObservableCollection<VocabularyWord>)ser.Deserialize(reader));
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error has occured:" + ex.ToString());
            }
            
            return null;
        }

        private void Quiz()
        {
            var words = LoadWords();

            if (words != null)
            {
                var quizWindow = new QuizWindow();
                quizWindow.DataContext = new QuizViewModel(words.Item2, words.Item1);
                quizWindow.ShowDialog();
            }
        }
    }
}
