using System;
using System.Collections.ObjectModel;
using Cult.SimMetrics.Api;
using Cult.SimMetrics.Utility;

// ReSharper disable All 
namespace Cult.SimMetrics.Metric
{
    public class MongeElkan : AbstractStringMetric
    {
        private const double DefaultMismatchScore = 0.0;
        private readonly double _estimatedTimingConstant;
        private readonly AbstractStringMetric _internalStringMetric;
        internal ITokeniser Tokeniser;

        public MongeElkan() : this(new TokeniserWhitespace())
        {
        }

        public MongeElkan(AbstractStringMetric metricToUse)
        {
            this._estimatedTimingConstant = 0.034400001168251038;
            this.Tokeniser = new TokeniserWhitespace();
            this._internalStringMetric = metricToUse;
        }

        public MongeElkan(ITokeniser tokeniserToUse)
        {
            this._estimatedTimingConstant = 0.034400001168251038;
            this.Tokeniser = tokeniserToUse;
            this._internalStringMetric = new SmithWatermanGotoh();
        }

        public MongeElkan(ITokeniser tokeniserToUse, AbstractStringMetric metricToUse)
        {
            this._estimatedTimingConstant = 0.034400001168251038;
            this.Tokeniser = tokeniserToUse;
            this._internalStringMetric = metricToUse;
        }

        public override double GetSimilarity(string firstWord, string secondWord)
        {
            if ((firstWord == null) || (secondWord == null))
            {
                return 0.0;
            }
            Collection<string> collection = this.Tokeniser.Tokenize(firstWord);
            Collection<string> collection2 = this.Tokeniser.Tokenize(secondWord);
            double num = 0.0;
            for (int i = 0; i < collection.Count; i++)
            {
                string str = collection[i];
                double num3 = 0.0;
                for (int j = 0; j < collection2.Count; j++)
                {
                    string str2 = collection2[j];
                    double similarity = this._internalStringMetric.GetSimilarity(str, str2);
                    if (similarity > num3)
                    {
                        num3 = similarity;
                    }
                }
                num += num3;
            }
            return (num / ((double) collection.Count));
        }

        public override string GetSimilarityExplained(string firstWord, string secondWord)
        {
            throw new NotImplementedException();
        }

        public override double GetSimilarityTimingEstimated(string firstWord, string secondWord)
        {
            if ((firstWord != null) && (secondWord != null))
            {
                double count = this.Tokeniser.Tokenize(firstWord).Count;
                double num2 = this.Tokeniser.Tokenize(secondWord).Count;
                return ((((count + num2) * count) + ((count + num2) * num2)) * this._estimatedTimingConstant);
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
                return "Implements the Monge Elkan algorithm providing an matching style similarity measure between two strings";
            }
        }

        public override string ShortDescriptionString
        {
            get
            {
                return "MongeElkan";
            }
        }
    }
}

