using System;
using System.Collections.Generic;
namespace Cult.ParserKit
{
    public abstract class BaseParser<TToken, TResult> where TToken : Enum
            where TResult : ParserErrorReporter, new()
    {
        private readonly IEnumerator<Token<TToken>> _tokens;
        protected BaseParser(LexerResult<TToken> lexerResult)
        {
            _tokens = lexerResult.Tokens.GetEnumerator();
            // _input = lexerResult.Input;
            Read(); // Start tokens processing, now 'Peek()' has a value and Current is not null.
        }
        protected virtual string Error(Token<TToken> currentToken, string expected, string message = "")
        {
            if (currentToken != null && !string.IsNullOrEmpty(expected))
            {
                return $"Expecting '{expected}' but got '{currentToken.Value}' ({currentToken.Line}:{currentToken.Start})"
                    + (string.IsNullOrEmpty(message) ? "" : Environment.NewLine + message);
            }

            if (currentToken == null)
            {
                throw new ArgumentNullException($"'{nameof(currentToken)}' argument is null.");
            }

            throw new ArgumentNullException($"'{nameof(expected)}' argument is null or empty.");
        }
        protected bool IsEndOfInput()
        {
            return _tokens.Current == null;
        }
        protected bool IsMatchType(TToken tokenType)
        {
            return !IsEndOfInput() && tokenType.Equals(Peek().Type);
        }
        protected bool IsMatchTypes(params TToken[] tokenTypes)
        {
            if (IsEndOfInput()) return false;
            foreach (var item in tokenTypes)
            {
                return item.Equals(Peek().Type);
            }
            return false;
        }
        protected bool IsMatchValue(string value, bool ignoreCase = true)
        {
            if (!IsEndOfInput())
            {
                return ignoreCase ? string.Equals(value, Peek().Value, StringComparison.OrdinalIgnoreCase) : value == Peek().Value;
            }
            return false;
        }
        protected bool IsMatchValues(params string[] values)
        {
            if (IsEndOfInput()) return false;

            foreach (var item in values)
            {
                return item == Peek().Value;
            }

            return false;
        }
        protected bool IsMatchValuesIgnoreCase(params string[] values)
        {
            if (!IsEndOfInput())
            {
                foreach (var item in values)
                {
                    return string.Equals(item, Peek().Value, StringComparison.OrdinalIgnoreCase);
                }
            }
            return false;
        }
        public abstract TResult Parse();
        protected Token<TToken> Peek()
        {
            return _tokens.Current;
        }
        protected bool Read()
        {
            return _tokens.MoveNext();
        }
        protected void Read<T>(Func<T> func)
        {
            var status = Read();
            if (!status) func();
        }
        protected void Skip()
        {
            Read();
        }
    }
}
