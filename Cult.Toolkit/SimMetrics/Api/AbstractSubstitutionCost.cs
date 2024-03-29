﻿// ReSharper disable All 
namespace Cult.Toolkit.SimMetrics.Api
{
    internal abstract class AbstractSubstitutionCost : ISubstitutionCost
    {
        protected AbstractSubstitutionCost()
        {
        }

        public abstract double GetCost(string firstWord, int firstWordIndex, string secondWord, int secondWordIndex);

        public abstract double MaxCost { get; }

        public abstract double MinCost { get; }

        public abstract string ShortDescriptionString { get; }
    }
}

