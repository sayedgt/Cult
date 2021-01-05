using System;
using System.Linq;
namespace Cult.ParserKit
{
    public class Token<TToken> where TToken : Enum
    {
        public int Column { get; }
        public int End { get; }
        public bool IsMultiLine => Value.Contains('\n');
        public int Length => Value.Length;
        public int Line { get; }
        public int Start => End - Length;
        public TToken Type { get; }
        public string Value { get; }
        public Token(TToken type, string value, int position, int line, int column)
        {
            Type = type;
            Value = value;
            End = position;
            Line = line;
            Column = column;
        }
    }
}
