using System.Collections.ObjectModel;
using System.Text;
using Cult.Toolkit.SimMetrics.Utility;

// ReSharper disable All 
namespace Cult.Toolkit.SimMetrics.Api
{
    internal abstract class AbstractTokeniserQGramN : ITokeniser
    {
        private int _characterCombinationIndex;
        private const string DefaultEndPadCharacter = "#";
        private const string DefaultStartPadCharacter = "?";
        private int _qGramLength;
        private ITermHandler _stopWordHandler;
        private string _suppliedWord;
        private TokeniserUtilities<string> _tokenUtilities;

        protected AbstractTokeniserQGramN()
        {
        }

        public abstract Collection<string> Tokenize(string word);
        public Collection<string> Tokenize(string word, bool extended, int tokenLength, int characterCombinationIndexValue)
        {
            int num3;
            if (string.IsNullOrEmpty(word))
            {
                return null;
            }
            this.SuppliedWord = word;
            Collection<string> collection = new Collection<string>();
            int length = word.Length;
            int count = 0;
            if (tokenLength > 0)
            {
                count = tokenLength - 1;
            }
            StringBuilder builder = new StringBuilder(length + (2 * count));
            if (extended)
            {
                builder.Insert(0, "?", count);
            }
            builder.Append(word);
            if (extended)
            {
                builder.Insert(builder.Length, "#", count);
            }
            string str = builder.ToString();
            if (extended)
            {
                num3 = length + count;
            }
            else
            {
                num3 = (length - tokenLength) + 1;
            }
            for (int i = 0; i < num3; i++)
            {
                string termToTest = str.Substring(i, tokenLength);
                if (!this._stopWordHandler.IsWord(termToTest))
                {
                    collection.Add(termToTest);
                }
            }
            if (characterCombinationIndexValue != 0)
            {
                str = builder.ToString();
                num3--;
                for (int j = 0; j < num3; j++)
                {
                    string str3 = str.Substring(j, count) + str.Substring(j + tokenLength, 1);
                    if (!this._stopWordHandler.IsWord(str3) && !collection.Contains(str3))
                    {
                        collection.Add(str3);
                    }
                }
            }
            return collection;
        }

        public Collection<string> TokenizeToSet(string word)
        {
            if (!string.IsNullOrEmpty(word))
            {
                this.SuppliedWord = word;
                return this.TokenUtilities.CreateSet(this.Tokenize(word));
            }
            return null;
        }

        public int CharacterCombinationIndex
        {
            get
            {
                return this._characterCombinationIndex;
            }
            set
            {
                this._characterCombinationIndex = value;
            }
        }

        public string Delimiters
        {
            get
            {
                return string.Empty;
            }
        }

        public int QGramLength
        {
            get
            {
                return this._qGramLength;
            }
            set
            {
                this._qGramLength = value;
            }
        }

        public abstract string ShortDescriptionString { get; }

        public ITermHandler StopWordHandler
        {
            get
            {
                return this._stopWordHandler;
            }
            set
            {
                this._stopWordHandler = value;
            }
        }

        public string SuppliedWord
        {
            get
            {
                return this._suppliedWord;
            }
            set
            {
                this._suppliedWord = value;
            }
        }

        public TokeniserUtilities<string> TokenUtilities
        {
            get
            {
                return this._tokenUtilities;
            }
            set
            {
                this._tokenUtilities = value;
            }
        }
    }
}

