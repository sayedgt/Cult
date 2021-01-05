using System;
using System.Collections.ObjectModel;

// ReSharper disable All 
namespace Cult.SimMetrics.Utility
{
    public class TokeniserQGram3Extended : TokeniserQGram3
    {
        public override Collection<string> Tokenize(string word)
        {
            return Tokenize(word, true, QGramLength, CharacterCombinationIndex);
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(SuppliedWord))
            {
                return string.Format("{0} : not word passed for tokenising yet.", this.ShortDescriptionString);
            }
            return string.Format("{0} - currently holding : {1}.{2}The method is using a QGram length of {3}.", new object[] { this.ShortDescriptionString, SuppliedWord, Environment.NewLine, Convert.ToInt32(QGramLength) });
        }

        public override string ShortDescriptionString
        {
            get
            {
                return "TokeniserQGram3Extended";
            }
        }
    }
}

