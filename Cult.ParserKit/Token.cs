using System;
using System.Linq;

// ReSharper disable UnusedMember.Global

namespace Cult.ParserKit
{
    public class Token<TToken> where TToken : Enum
    {
        public Token(TToken type, string value, int position, int line, int column)
        {
            Type = type;
            Value = value;
            End = position;
            Line = line;
            Column = column;
        }

        public TToken Type { get; }
        public string Value { get; }
        public int End { get; }
        public int Line { get; }
        public int Column { get; }
        public int Start => End - Length;
        public int Length => Value.Length;
        public bool IsMultiLine => Value.Contains('\n');
    }
}
