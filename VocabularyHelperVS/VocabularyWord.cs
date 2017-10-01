using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyHelperVS
{
    public class VocabularyWord : Notifier
    {
        private string wordContent;

        public string WordContent
        {
            get { return wordContent; }
            set
            {
                wordContent = value;
                RaisePropertyChanged();
            }
        }

        private string additionalContent;

        public string AdditionalContent
        {
            get { return additionalContent; }
            set
            {
                additionalContent = value;
                RaisePropertyChanged();
            }
        }

        private string translation;

        public string Translation
        {
            get { return translation; }
            set
            {
                translation = value;
                RaisePropertyChanged();
            }
        }


        private string _vocabularyWordType = Constantes.WordType.Noun;

        public string VocabularyWordType
        {
            get { return _vocabularyWordType; }
            set
            {
                _vocabularyWordType = value;
                RaisePropertyChanged();
            }
        }
    }
}
