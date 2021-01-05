using System;
using System.Collections.Generic;
using System.IO;
namespace Cult.ParserKit
{
    public abstract class BaseLexer<TToken> where TToken : Enum
    {
        private readonly string _input;
        protected int Column { get; set; }
        protected int Line { get; set; }
        protected int Position { get; set; }
        private StringReader Reader { get; }
        protected BaseLexer(string input)
        {
            Reader = new StringReader(input);
            Position = 0;
            Line = 1;
            Column = 0;
            _input = input;
        }
        protected bool IsEndOfFile()
        {
            return Reader.Peek() == -1;
        }
        protected bool IsMatch(char ch)
        {
            if (!IsEndOfFile())
            {
                return ch == Peek();
            }
            return false;
        }
        protected char Peek()
        {
            return (char)Reader.Peek();
        }
        protected char? Read()
        {
            var value = Reader.Read();
            ++Position;

            if (value == -1)
                return null;

            var ch = (char)value;

            if (ch == '\n')
            {
                Line++;
                Column = 0;
            }
            else
            {
                ++Column;
            }
            return ch;
        }
        protected string ReadEscaped(char ch, char indicator)
        {
            var escaped = false;
            var result = "";
            Read();
            while (!IsEndOfFile())
            {
                var c = Read();
                if (escaped)
                {
                    result += c;
                    escaped = false;
                }
                else if (c == indicator)
                {
                    escaped = true;
                }
                else if (c == ch)
                {
                    break;
                }
                else
                {
                    result += c;
                }
            }
            return result;
        }
        protected virtual string ReadFloatNumber()
        {
            var hasDot = false;
            return ReadWhile(ch =>
            {
                if (ch == '.')
                {
                    if (hasDot) return false;
                    hasDot = true;
                    return true;
                }
                return ch.IsDigit();
            });
        }
        protected virtual string ReadIntegerNumber()
        {
            return ReadWhile(ch => ch.IsDigit());
        }
        protected string ReadLine(bool movePointerToNextLine = false)
        {
            var result = ReadWhile(ch => ch != '\n');
            if (movePointerToNextLine)
            {
                Skip();
            }
            return result;
        }
        protected abstract Token<TToken> ReadToken();
        protected string ReadWhile(Func<char, bool> predicate)
        {
            var result = "";
            while (!IsEndOfFile() && predicate(Peek()))
            {
                result += Read();
            }
            return result;
        }
        protected void Skip()
        {
            Read();
        }
        protected void SkipLine(bool movePointerToNextLine = false)
        {
            if (movePointerToNextLine)
                ReadLine(true);
            else
                ReadLine();
        }
        protected void SkipWhile(Func<char, bool> predicate)
        {
            ReadWhile(predicate);
        }
        public LexerResult<TToken> Tokenize()
        {
            var tokens = new List<Token<TToken>>();
            while (true)
            {
                if (IsEndOfFile()) break;
                var token = ReadToken();
                if (token == null) break;
                tokens.Add(token);
            }
            return new LexerResult<TToken> { Input = _input, Tokens = tokens };
        }
    }
}
