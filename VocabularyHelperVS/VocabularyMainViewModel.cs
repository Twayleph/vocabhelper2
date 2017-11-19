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
            var words = LoadWords(multiFile: false);

            if (words != null && words.Count == 1)
            {
                var edit = new VocabularyInputWindow();
                edit.DataContext = new VocabularyInputViewModel(words[0].Item2, words[0].Item1);
                edit.ShowDialog();
            }
        }

        public static string GetFileDirectory()
        {
            var rootFolder = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName;
            return Path.Combine(rootFolder, "Data");
        }

        private string[] GetLoadFilePaths(bool multiFile)
        {
            var dialog = new OpenFileDialog()
            {
                InitialDirectory = GetFileDirectory(),
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                DefaultExt = "xml",
                Multiselect = multiFile,
            };

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileNames;
            }

            return null;
        }

        private IList<Tuple<string, ObservableCollection<VocabularyWord>>> LoadWords(bool multiFile)
        {
            var list = new List<Tuple<string, ObservableCollection<VocabularyWord>>>();

            try
            {
                var filePaths = GetLoadFilePaths(multiFile);

                if (filePaths != null && filePaths.Length > 0)
                {
                    var ser = new XmlSerializer(typeof(ObservableCollection<VocabularyWord>));
                    foreach (var filePath in filePaths)
                    {
                        using (var reader = new StreamReader(filePath))
                        {
                            list.Add(new Tuple<string, ObservableCollection<VocabularyWord>>(filePath, (ObservableCollection<VocabularyWord>)ser.Deserialize(reader)));
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error has occured:" + ex.ToString());
            }

            return list;
        }

        private void Quiz()
        {
            var words = LoadWords(multiFile: true);

            if (words != null)
            {
                var quizWindow = new QuizWindow();
                quizWindow.DataContext = new QuizViewModel(words.SelectMany(w => w.Item2).ToList());
                quizWindow.ShowDialog();
            }
        }
    }
}
