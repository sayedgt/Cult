using System;
using System.Collections.ObjectModel;
using Cult.Toolkit.SimMetrics.Api;
using Cult.Toolkit.SimMetrics.Utility;

// ReSharper disable All 
namespace Cult.Toolkit.SimMetrics.Metric
{
    internal sealed class MatchingCoefficient : AbstractStringMetric
    {
        private const double DefaultMismatchScore = 0.0;
        private readonly double _estimatedTimingConstant;
        private readonly ITokeniser _tokeniser;
        private readonly TokeniserUtilities<string> _tokenUtilities;

        public MatchingCoefficient() : this(new TokeniserWhitespace())
        {
        }

        public MatchingCoefficient(ITokeniser tokeniserToUse)
        {
            this._estimatedTimingConstant = 0.00019999999494757503;
            this._tokeniser = tokeniserToUse;
            this._tokenUtilities = new TokeniserUtilities<string>();
        }

        private double GetActualSimilarity(Collection<string> firstTokens, Collection<string> secondTokens)
        {
            this._tokenUtilities.CreateMergedList(firstTokens, secondTokens);
            int num = 0;
            foreach (string str in firstTokens)
            {
                if (secondTokens.Contains(str))
                {
                    num++;
                }
            }
            return (double) num;
        }

        public override double GetSimilarity(string firstWord, string secondWord)
        {
            if ((firstWord != null) && (secondWord != null))
            {
                double unnormalisedSimilarity = this.GetUnnormalisedSimilarity(firstWord, secondWord);
                int num2 = Math.Max(this._tokenUtilities.FirstTokenCount, this._tokenUtilities.SecondTokenCount);
                return (unnormalisedSimilarity / ((double) num2));
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
                return ((num2 * count) * this._estimatedTimingConstant);
            }
            return 0.0;
        }

        public override double GetUnnormalisedSimilarity(string firstWord, string secondWord)
        {
            Collection<string> firstTokens = this._tokeniser.Tokenize(firstWord);
            Collection<string> secondTokens = this._tokeniser.Tokenize(secondWord);
            return this.GetActualSimilarity(firstTokens, secondTokens);
        }

        public override string LongDescriptionString
        {
            get
            {
                return "Implements the Matching Coefficient algorithm providing a similarity measure between two strings";
            }
        }

        public override string ShortDescriptionString
        {
            get
            {
                return "MatchingCoefficient";
            }
        }
    }
}

