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
    public class QuizViewModel : Notifier
    {
        public IList<VocabularyWord> Words { get; private set; }

        public DelegateCommand UserAccept => new DelegateCommand(GoToNext);

        private bool isTranslated;
        public bool IsTranslated
        {
            get { return isTranslated; }
            set
            {
                isTranslated = value;
                RaisePropertyChanged();
            }
        }

        private int currentWordIndex;
        public int CurrentWordIndex
        {
            get { return currentWordIndex; }
            set
            {
                currentWordIndex = value;
                if (CurrentWordIndex >= 0 && CurrentWordIndex < Words.Count)
                {
                    CurrentWord = Words[CurrentWordIndex];
                }
                else
                {
                    CurrentWord = null;
                }
            }
        }


        private VocabularyWord currentWord;
        public VocabularyWord CurrentWord
        {
            get { return currentWord; }
            set
            {
                currentWord = value;
                RaisePropertyChanged();
            }
        }

        private string response;
        public string Response
        {
            get { return response; }
            set
            {
                response = value;
                RaisePropertyChanged();
            }
        }


        // TODO to allow fix words during quiz
        public string FilePath;

        public QuizViewModel(IList<VocabularyWord> words)
        {
            Shuffle(words);
            Words = words.ToList();
            CurrentWordIndex = 0;
        }

        private static void Shuffle<T>(IList<T> list)
        {
            var random = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void GoToNext()
        {
            // If already translated, go to next word. Otherwise, translate
            if (IsTranslated)
            {
                CurrentWordIndex++;
                Response = null;
                if (CurrentWordIndex >= Words.Count)
                {
                    MessageBox.Show("Finished the quiz! Restarting...");
                    Shuffle(Words);
                    CurrentWordIndex = 0;
                }
            }

            IsTranslated = !IsTranslated;
        }
    }
}
