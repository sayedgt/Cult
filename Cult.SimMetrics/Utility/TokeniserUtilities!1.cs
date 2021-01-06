using System.Collections.ObjectModel;

// ReSharper disable All 
namespace Cult.SimMetrics.Utility
{
    internal class TokeniserUtilities<T>
    {
        private readonly Collection<T> _allTokens;
        private int _firstSetTokenCount;
        private int _firstTokenCount;
        private int _secondSetTokenCount;
        private int _secondTokenCount;
        private readonly Collection<T> _tokenSet;

        public TokeniserUtilities()
        {
            this._allTokens = new Collection<T>();
            this._tokenSet = new Collection<T>();
        }

        private void AddTokens(Collection<T> tokenList)
        {
            foreach (T local in tokenList)
            {
                this._allTokens.Add(local);
            }
        }

        private void AddUniqueTokens(Collection<T> tokenList)
        {
            foreach (T local in tokenList)
            {
                if (!this._tokenSet.Contains(local))
                {
                    this._tokenSet.Add(local);
                }
            }
        }

        private int CalculateUniqueTokensCount(Collection<T> tokenList)
        {
            Collection<T> collection = new Collection<T>();
            foreach (T local in tokenList)
            {
                if (!collection.Contains(local))
                {
                    collection.Add(local);
                }
            }
            return collection.Count;
        }

        public int CommonSetTerms()
        {
            return ((this.FirstSetTokenCount + this.SecondSetTokenCount) - this._tokenSet.Count);
        }

        public int CommonTerms()
        {
            return ((this.FirstTokenCount + this.SecondTokenCount) - this._allTokens.Count);
        }

        public Collection<T> CreateMergedList(Collection<T> firstTokens, Collection<T> secondTokens)
        {
            this._allTokens.Clear();
            this._firstTokenCount = firstTokens.Count;
            this._secondTokenCount = secondTokens.Count;
            this.MergeLists(firstTokens);
            this.MergeLists(secondTokens);
            return this._allTokens;
        }

        public Collection<T> CreateMergedSet(Collection<T> firstTokens, Collection<T> secondTokens)
        {
            this._tokenSet.Clear();
            this._firstSetTokenCount = this.CalculateUniqueTokensCount(firstTokens);
            this._secondSetTokenCount = this.CalculateUniqueTokensCount(secondTokens);
            this.MergeIntoSet(firstTokens);
            this.MergeIntoSet(secondTokens);
            return this._tokenSet;
        }

        public Collection<T> CreateSet(Collection<T> tokenList)
        {
            this._tokenSet.Clear();
            this.AddUniqueTokens(tokenList);
            this._firstTokenCount = this._tokenSet.Count;
            this._secondTokenCount = 0;
            return this._tokenSet;
        }

        public void MergeIntoSet(Collection<T> firstTokens)
        {
            this.AddUniqueTokens(firstTokens);
        }

        public void MergeLists(Collection<T> firstTokens)
        {
            this.AddTokens(firstTokens);
        }

        public int FirstSetTokenCount
        {
            get
            {
                return this._firstSetTokenCount;
            }
        }

        public int FirstTokenCount
        {
            get
            {
                return this._firstTokenCount;
            }
        }

        public Collection<T> MergedTokens
        {
            get
            {
                return this._allTokens;
            }
        }

        public int SecondSetTokenCount
        {
            get
            {
                return this._secondSetTokenCount;
            }
        }

        public int SecondTokenCount
        {
            get
            {
                return this._secondTokenCount;
            }
        }

        public Collection<T> TokenSet
        {
            get
            {
                return this._tokenSet;
            }
        }
    }
}

