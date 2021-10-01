﻿// ReSharper disable UnusedMember.Global
// ReSharper disable CheckNamespace

using System.Linq;

namespace Cult.Toolkit
{
    public class Position
    {
        private readonly string _value;
        public Position(string value, int position, int line, int column)
        {
            Line = line;
            Column = column;
            _value = value;
            End = position;
        }
        public int Line { get; }
        public int Column { get; }
        public int Length => _value.Length;
        public int End { get; }
        public int Start => End - Length;
        public bool IsMultiLine => _value.Contains('\n');

    }
}
