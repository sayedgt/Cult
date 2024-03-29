﻿// ReSharper disable All 
namespace Cult.Toolkit.SimMetrics.Api
{
    internal abstract class AbstractAffineGapCost : IAffineGapCost
    {
        protected AbstractAffineGapCost()
        {
        }

        public abstract double GetCost(string textToGap, int stringIndexStartGap, int stringIndexEndGap);

        public abstract double MaxCost { get; }

        public abstract double MinCost { get; }

        public abstract string ShortDescriptionString { get; }
    }
}

