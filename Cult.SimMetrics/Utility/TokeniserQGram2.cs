using System;
using System.Collections.ObjectModel;
using Cult.SimMetrics.Api;

// ReSharper disable All 
namespace Cult.SimMetrics.Utility
{
    public class TokeniserQGram2 : AbstractTokeniserQGramN
    {
        public TokeniserQGram2()
        {
            StopWordHandler = new DummyStopTermHandler();
            TokenUtilities = new TokeniserUtilities<string>();
            CharacterCombinationIndex = 0;
            QGramLength = 2;
        }

        public override Collection<string> Tokenize(string word)
        {
            return Tokenize(word, false, QGramLength, CharacterCombinationIndex);
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
                return "TokeniserQGram2";
            }
        }
    }
}

