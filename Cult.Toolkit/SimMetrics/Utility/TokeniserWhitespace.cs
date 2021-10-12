using System.Collections.ObjectModel;
using Cult.Toolkit.SimMetrics.Api;

// ReSharper disable All 
namespace Cult.Toolkit.SimMetrics.Utility
{
    internal sealed class TokeniserWhitespace : ITokeniser
    {
        private readonly string _delimiters = "\r\n\t \x00a0";
        private ITermHandler _stopWordHandler = new DummyStopTermHandler();
        private readonly TokeniserUtilities<string> _tokenUtilities = new TokeniserUtilities<string>();

        public Collection<string> Tokenize(string word)
        {
            Collection<string> collection = new Collection<string>();
            if (word != null)
            {
                int length;
                for (int i = 0; i < word.Length; i = length)
                {
                    char c = word[i];
                    if (char.IsWhiteSpace(c))
                    {
                        i++;
                    }
                    length = word.Length;
                    for (int j = 0; j < this._delimiters.Length; j++)
                    {
                        int index = word.IndexOf(this._delimiters[j], i);
                        if ((index < length) && (index != -1))
                        {
                            length = index;
                        }
                    }
                    string termToTest = word.Substring(i, length - i);
                    if (!this._stopWordHandler.IsWord(termToTest))
                    {
                        collection.Add(termToTest);
                    }
                }
            }
            return collection;
        }

        public Collection<string> TokenizeToSet(string word)
        {
            if (word != null)
            {
                return this._tokenUtilities.CreateSet(this.Tokenize(word));
            }
            return null;
        }

        public string Delimiters
        {
            get
            {
                return this._delimiters;
            }
        }

        public string ShortDescriptionString
        {
            get
            {
                return "TokeniserWhitespace";
            }
        }

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
    }
}

