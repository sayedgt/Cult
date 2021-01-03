using System;
using Cult.SimMetrics.Api;
using Cult.SimMetrics.Utility;

namespace Cult.SimMetrics.Metric
{
    public sealed class NeedlemanWunch : AbstractStringMetric
    {
        private AbstractSubstitutionCost _dCostFunction;
        private const double DefaultGapCost = 2.0;
        private const double DefaultMismatchScore = 0.0;
        private const double DefaultPerfectMatchScore = 1.0;
        private double _estimatedTimingConstant;
        private double _gapCost;

        public NeedlemanWunch() : this(2.0, new SubCostRange0To1())
        {
        }

        public NeedlemanWunch(AbstractSubstitutionCost costFunction) : this(2.0, costFunction)
        {
        }

        public NeedlemanWunch(double costG) : this(costG, new SubCostRange0To1())
        {
        }

        public NeedlemanWunch(double costG, AbstractSubstitutionCost costFunction)
        {
            this._estimatedTimingConstant = 0.00018420000560581684;
            this._gapCost = costG;
            this._dCostFunction = costFunction;
        }

        public override double GetSimilarity(string firstWord, string secondWord)
        {
            if ((firstWord == null) || (secondWord == null))
            {
                return 0.0;
            }
            double unnormalisedSimilarity = this.GetUnnormalisedSimilarity(firstWord, secondWord);
            double num2 = Math.Max(firstWord.Length, secondWord.Length);
            double num3 = num2;
            if (this._dCostFunction.MaxCost > this._gapCost)
            {
                num2 *= this._dCostFunction.MaxCost;
            }
            else
            {
                num2 *= this._gapCost;
            }
            if (this._dCostFunction.MinCost < this._gapCost)
            {
                num3 *= this._dCostFunction.MinCost;
            }
            else
            {
                num3 *= this._gapCost;
            }
            if (num3 < 0.0)
            {
                num2 -= num3;
                unnormalisedSimilarity -= num3;
            }
            if (num2 == 0.0)
            {
                return 1.0;
            }
            return (1.0 - (unnormalisedSimilarity / num2));
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
            if ((firstWord == null) || (secondWord == null))
            {
                return 0.0;
            }
            int length = firstWord.Length;
            int index = secondWord.Length;
            if (length == 0)
            {
                return (double) index;
            }
            if (index == 0)
            {
                return (double) length;
            }
            double[][] numArray = new double[length + 1][];
            for (int i = 0; i < (length + 1); i++)
            {
                numArray[i] = new double[index + 1];
            }
            for (int j = 0; j <= length; j++)
            {
                numArray[j][0] = j;
            }
            for (int k = 0; k <= index; k++)
            {
                numArray[0][k] = k;
            }
            for (int m = 1; m <= length; m++)
            {
                for (int n = 1; n <= index; n++)
                {
                    double num8 = this._dCostFunction.GetCost(firstWord, m - 1, secondWord, n - 1);
                    numArray[m][n] = MathFunctions.MinOf3((double) (numArray[m - 1][n] + this._gapCost), (double) (numArray[m][n - 1] + this._gapCost), (double) (numArray[m - 1][n - 1] + num8));
                }
            }
            return numArray[length][index];
        }

        public AbstractSubstitutionCost DCostFunction
        {
            get
            {
                return this._dCostFunction;
            }
            set
            {
                this._dCostFunction = value;
            }
        }

        public double GapCost
        {
            get
            {
                return this._gapCost;
            }
            set
            {
                this._gapCost = value;
            }
        }

        public override string LongDescriptionString
        {
            get
            {
                return "Implements the Needleman-Wunch algorithm providing an edit distance based similarity measure between two strings";
            }
        }

        public override string ShortDescriptionString
        {
            get
            {
                return "NeedlemanWunch";
            }
        }
    }
}

