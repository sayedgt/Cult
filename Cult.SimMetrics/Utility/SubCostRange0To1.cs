using Cult.SimMetrics.Api;

// ReSharper disable All 
namespace Cult.SimMetrics.Utility
{
    internal sealed class SubCostRange0To1 : AbstractSubstitutionCost
    {
        private const int CharExactMatchScore = 1;
        private const int CharMismatchMatchScore = 0;

        public override double GetCost(string firstWord, int firstWordIndex, string secondWord, int secondWordIndex)
        {
            if ((firstWord != null) && (secondWord != null))
            {
                return ((firstWord[firstWordIndex] != secondWord[secondWordIndex]) ? ((double) 1) : ((double) 0));
            }
            return 0.0;
        }

        public override double MaxCost
        {
            get
            {
                return 1.0;
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
                return "SubCostRange0To1";
            }
        }
    }
}

