using System;
using Cult.SimMetrics.Api;
using Cult.SimMetrics.Utility;

// ReSharper disable All 
namespace Cult.SimMetrics.Metric
{
    public sealed class OverlapCoefficient : AbstractStringMetric
    {
        private const double DefaultMismatchScore = 0.0;
        private double _estimatedTimingConstant;
        private ITokeniser _tokeniser;
        private TokeniserUtilities<string> _tokenUtilities;

        public OverlapCoefficient() : this(new TokeniserWhitespace())
        {
        }

        public OverlapCoefficient(ITokeniser tokeniserToUse)
        {
            this._estimatedTimingConstant = 0.00014000000373926014;
            this._tokeniser = tokeniserToUse;
            this._tokenUtilities = new TokeniserUtilities<string>();
        }

        public override double GetSimilarity(string firstWord, string secondWord)
        {
            if ((firstWord != null) && (secondWord != null))
            {
                this._tokenUtilities.CreateMergedSet(this._tokeniser.Tokenize(firstWord), this._tokeniser.Tokenize(secondWord));
                return (((double) this._tokenUtilities.CommonSetTerms()) / ((double) Math.Min(this._tokenUtilities.FirstSetTokenCount, this._tokenUtilities.SecondSetTokenCount)));
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
                double count = this._tokeniser.Tokenize(firstWord).Count;
                double num2 = this._tokeniser.Tokenize(secondWord).Count;
                return ((count * num2) * this._estimatedTimingConstant);
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
                return "Implements the Overlap Coefficient algorithm providing a similarity measure between two string where it is determined to what degree a string is a subset of another";
            }
        }

        public override string ShortDescriptionString
        {
            get
            {
                return "OverlapCoefficient";
            }
        }
    }
}

