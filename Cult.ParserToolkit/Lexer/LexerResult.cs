using System;
using System.Collections.Generic;

namespace Cult.ParserToolkit.Lexer
{
    public sealed class LexerResult<TToken>
        where TToken : Enum
    {
        public string Input { get; internal set; }
        public IList<Token<TToken>> Tokens { get; internal set; }

        public LexerResult()
        {
            Input = "";
            Tokens = new List<Token<TToken>>();
        }
    }
}
