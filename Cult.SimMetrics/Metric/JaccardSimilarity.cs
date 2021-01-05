using System;
using System.Collections.ObjectModel;
using Cult.SimMetrics.Api;
using Cult.SimMetrics.Utility;

// ReSharper disable All 
namespace Cult.SimMetrics.Metric
{
    public sealed class JaccardSimilarity : AbstractStringMetric
    {
        private const double DefaultMismatchScore = 0.0;
        private readonly double _estimatedTimingConstant;
        private readonly ITokeniser _tokeniser;
        private readonly TokeniserUtilities<string> _tokenUtilities;

        public JaccardSimilarity() : this(new TokeniserWhitespace())
        {
        }

        public JaccardSimilarity(ITokeniser tokeniserToUse)
        {
            this._estimatedTimingConstant = 0.00014000000373926014;
            this._tokeniser = tokeniserToUse;
            this._tokenUtilities = new TokeniserUtilities<string>();
        }

        public override double GetSimilarity(string firstWord, string secondWord)
        {
            if ((firstWord != null) && (secondWord != null))
            {
                Collection<string> collection = this._tokenUtilities.CreateMergedSet(this._tokeniser.Tokenize(firstWord), this._tokeniser.Tokenize(secondWord));
                if (collection.Count > 0)
                {
                    return (((double) this._tokenUtilities.CommonSetTerms()) / ((double) collection.Count));
                }
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
                return "Implements the Jaccard Similarity algorithm providing a similarity measure between two strings";
            }
        }

        public override string ShortDescriptionString
        {
            get
            {
                return "JaccardSimilarity";
            }
        }
    }
}

