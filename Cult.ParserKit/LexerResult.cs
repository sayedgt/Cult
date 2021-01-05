using System;
using System.Collections.Generic;
// ReSharper disable All 
namespace Cult.ParserKit
{
    public sealed class LexerResult<TToken> where TToken : Enum
    {
        public string Input { get; internal set; }
        public IEnumerable<Token<TToken>> Tokens { get; internal set; }
        public LexerResult()
        {
            Input = "";
            Tokens = new List<Token<TToken>>();
        }
    }
}
