using System.Collections.ObjectModel;
using Cult.SimMetrics.Api;

// ReSharper disable All 
namespace Cult.SimMetrics.Utility
{
    internal sealed class SubCostRange5ToMinus3 : AbstractSubstitutionCost
    {
        private readonly Collection<string>[] _approx = new Collection<string>[7];
        private const int CharApproximateMatchScore = 3;
        private const int CharExactMatchScore = 5;
        private const int CharMismatchMatchScore = -3;

        public SubCostRange5ToMinus3()
        {
            this._approx[0] = new Collection<string>();
            this._approx[0].Add("d");
            this._approx[0].Add("t");
            this._approx[1] = new Collection<string>();
            this._approx[1].Add("g");
            this._approx[1].Add("j");
            this._approx[2] = new Collection<string>();
            this._approx[2].Add("l");
            this._approx[2].Add("r");
            this._approx[3] = new Collection<string>();
            this._approx[3].Add("m");
            this._approx[3].Add("n");
            this._approx[4] = new Collection<string>();
            this._approx[4].Add("b");
            this._approx[4].Add("p");
            this._approx[4].Add("v");
            this._approx[5] = new Collection<string>();
            this._approx[5].Add("a");
            this._approx[5].Add("e");
            this._approx[5].Add("i");
            this._approx[5].Add("o");
            this._approx[5].Add("u");
            this._approx[6] = new Collection<string>();
            this._approx[6].Add(",");
            this._approx[6].Add(".");
        }

        public override double GetCost(string firstWord, int firstWordIndex, string secondWord, int secondWordIndex)
        {
            if ((firstWord != null) && (secondWord != null))
            {
                if ((firstWord.Length <= firstWordIndex) || (firstWordIndex < 0))
                {
                    return -3.0;
                }
                if ((secondWord.Length <= secondWordIndex) || (secondWordIndex < 0))
                {
                    return -3.0;
                }
                if (firstWord[firstWordIndex] == secondWord[secondWordIndex])
                {
                    return 5.0;
                }
                char ch = firstWord[firstWordIndex];
                string item = ch.ToString().ToLowerInvariant();
                char ch2 = secondWord[secondWordIndex];
                string str2 = ch2.ToString().ToLowerInvariant();
                for (int i = 0; i < this._approx.Length; i++)
                {
                    if (this._approx[i].Contains(item) && this._approx[i].Contains(str2))
                    {
                        return 3.0;
                    }
                }
            }
            return -3.0;
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
                return -3.0;
            }
        }

        public override string ShortDescriptionString
        {
            get
            {
                return "SubCostRange5ToMinus3";
            }
        }
    }
}

