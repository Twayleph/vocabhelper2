using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyHelperVS
{
    public class Constantes
    {
        public static class WordType
        {
            public const string Verb = "Verb";
            public const string Noun = "Noun";
            public const string Adjective = "Adjective";
        }
        public static readonly IEnumerable<string> AllWordTypes = new string[] { WordType.Noun, WordType.Adjective, WordType.Verb };
    }
}
