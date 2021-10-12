﻿using System;

// ReSharper disable All 
namespace Cult.Toolkit.SimMetrics.Utility
{
    internal class TokeniserSGram3 : TokeniserQGram3
    {
        public TokeniserSGram3()
        {
            CharacterCombinationIndex = 1;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(SuppliedWord))
            {
                return string.Format("{0} : not word passed for tokenising yet.", this.ShortDescriptionString);
            }
            if (CharacterCombinationIndex == 0)
            {
                return string.Format("{0} - currently holding : {1}.{2}The method is using a QGram length of {3}.", new object[] { this.ShortDescriptionString, SuppliedWord, Environment.NewLine, Convert.ToInt32(QGramLength) });
            }
            return string.Format("{0} - currently holding : {1}.{2}The method is using a character combination index of {3} and {4}a QGram length of {5}.", new object[] { this.ShortDescriptionString, SuppliedWord, Environment.NewLine, Convert.ToInt32(CharacterCombinationIndex), Environment.NewLine, Convert.ToInt32(QGramLength) });
        }

        public override string ShortDescriptionString
        {
            get
            {
                return "TokeniserSGram3";
            }
        }
    }
}

