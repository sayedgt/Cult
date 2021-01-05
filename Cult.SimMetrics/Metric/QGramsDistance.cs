using System;
using System.Collections.ObjectModel;
using Cult.SimMetrics.Api;
using Cult.SimMetrics.Utility;

// ReSharper disable All 
namespace Cult.SimMetrics.Metric
{
    public sealed class QGramsDistance : AbstractStringMetric
    {
        private const double DefaultMismatchScore = 0.0;
        private double _estimatedTimingConstant;
        private ITokeniser _tokeniser;
        private TokeniserUtilities<string> _tokenUtilities;

        public QGramsDistance() : this(new TokeniserQGram3Extended())
        {
        }

        public QGramsDistance(ITokeniser tokeniserToUse)
        {
            this._estimatedTimingConstant = 0.0001340000017080456;
            this._tokeniser = tokeniserToUse;
            this._tokenUtilities = new TokeniserUtilities<string>();
        }

        private double GetActualSimilarity(Collection<string> firstTokens, Collection<string> secondTokens)
        {
            Collection<string> collection = this._tokenUtilities.CreateMergedSet(firstTokens, secondTokens);
            int num = 0;
            foreach (string str in collection)
            {
                int num2 = 0;
                for (int i = 0; i < firstTokens.Count; i++)
                {
                    if (firstTokens[i].Equals(str))
                    {
                        num2++;
                    }
                }
                int num4 = 0;
                for (int j = 0; j < secondTokens.Count; j++)
                {
                    if (secondTokens[j].Equals(str))
                    {
                        num4++;
                    }
                }
                if (num2 > num4)
                {
                    num += num2 - num4;
                }
                else
                {
                    num += num4 - num2;
                }
            }
            return (double) num;
        }

        public override double GetSimilarity(string firstWord, string secondWord)
        {
            if ((firstWord != null) && (secondWord != null))
            {
                double unnormalisedSimilarity = this.GetUnnormalisedSimilarity(firstWord, secondWord);
                int num2 = this._tokenUtilities.FirstTokenCount + this._tokenUtilities.SecondTokenCount;
                if (num2 != 0)
                {
                    return ((num2 - unnormalisedSimilarity) / ((double) num2));
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
                double length = firstWord.Length;
                double num2 = secondWord.Length;
                return ((length * num2) * this._estimatedTimingConstant);
            }
            return 0.0;
        }

        public override double GetUnnormalisedSimilarity(string firstWord, string secondWord)
        {
            Collection<string> firstTokens = this._tokeniser.Tokenize(firstWord);
            Collection<string> secondTokens = this._tokeniser.Tokenize(secondWord);
            this._tokenUtilities.CreateMergedList(firstTokens, secondTokens);
            return this.GetActualSimilarity(firstTokens, secondTokens);
        }

        public override string LongDescriptionString
        {
            get
            {
                return "Implements the Q Grams Distance algorithm providing a similarity measure between two strings using the qGram approach check matching qGrams/possible matching qGrams";
            }
        }

        public override string ShortDescriptionString
        {
            get
            {
                return "QGramsDistance";
            }
        }
    }
}

