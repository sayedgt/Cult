using System;
using Cult.SimMetrics.Api;
using Cult.SimMetrics.Utility;

// ReSharper disable All 
namespace Cult.SimMetrics.Metric
{
    internal sealed class DiceSimilarity : AbstractStringMetric
    {
        private readonly double _estimatedTimingConstant;
        private readonly ITokeniser _tokeniser;
        private readonly TokeniserUtilities<string> _tokenUtilities;

        public DiceSimilarity() : this(new TokeniserWhitespace())
        {
        }

        public DiceSimilarity(ITokeniser tokeniserToUse)
        {
            this._estimatedTimingConstant = 3.4457139008736704E-07;
            this._tokeniser = tokeniserToUse;
            this._tokenUtilities = new TokeniserUtilities<string>();
        }

        public override double GetSimilarity(string firstWord, string secondWord)
        {
            if (((firstWord != null) && (secondWord != null)) && (this._tokenUtilities.CreateMergedSet(this._tokeniser.Tokenize(firstWord), this._tokeniser.Tokenize(secondWord)).Count > 0))
            {
                return ((2.0 * this._tokenUtilities.CommonSetTerms()) / ((double) (this._tokenUtilities.FirstSetTokenCount + this._tokenUtilities.SecondSetTokenCount)));
            }
            return 0.0;
        }

        public override string GetSimilarityExplained(string firstWord, string secondWord)
        {
            throw new NotImplementedException();
        }

        public override double GetSimilarityTimingEstimated(string firstWord, string secondWord)
        {
            if ((firstWord != null) && (secondWord != null))
            {
                double length = firstWord.Length;
                double num2 = secondWord.Length;
                return ((length + num2) * ((length + num2) * this._estimatedTimingConstant));
            }
            return 0.0;
        }

        public override double GetUnnormalisedSimilarity(string firstWord, string secondWord)
        {
            return this.GetSimilarity(firstWord, secondWord);
        }

        public override string LongDescriptionString
        {
            get
            {
                return "Implements the DiceSimilarity algorithm providing a similarity measure between two strings using the vector space of present terms";
            }
        }

        public override string ShortDescriptionString
        {
            get
            {
                return "DiceSimilarity";
            }
        }
    }
}

