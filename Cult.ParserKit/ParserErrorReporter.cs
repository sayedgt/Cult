using System.Collections.Generic;

// ReSharper disable UnusedMember.Global

namespace Cult.ParserKit
{
    public abstract class ParserErrorReporter
    {
        private readonly List<string> _errors = new List<string>();
        public IReadOnlyCollection<string> Errors => _errors;

        public bool HasError => Errors.Count > 0;

        public void AddError(string error)
        {
            _errors.Add(error);
        }
    }
}
