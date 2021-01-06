using Cult.SimMetrics.Api;

// ReSharper disable All 
namespace Cult.SimMetrics.Utility
{
    internal sealed class AffineGapRange5To0Multiplier1 : AbstractAffineGapCost
    {
        private const int CharExactMatchScore = 5;
        private const int CharMismatchMatchScore = 0;

        public override double GetCost(string textToGap, int stringIndexStartGap, int stringIndexEndGap)
        {
            if (stringIndexStartGap >= stringIndexEndGap)
            {
                return 0.0;
            }
            return (double) (5 + ((stringIndexEndGap - 1) - stringIndexStartGap));
        }

        public override double MaxCost
        {
            get
            {
                return 5.0;
            }
        }

        public override double MinCost
        {
            get
            {
                return 0.0;
            }
        }

        public override string ShortDescriptionString
        {
            get
            {
                return "AffineGapRange5To0Multiplier1";
            }
        }
    }
}

