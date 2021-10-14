﻿
// ReSharper disable All

using System.ComponentModel;

#if NETSTANDARD2_1
using System;
using System.Runtime.CompilerServices;
#endif

namespace Cult.Toolkit.ExtraEnumerator
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class RangeExtensions
    {
        public struct RangeEnumerator
        {
            public RangeEnumerator(int start, int end) => (Current, _end) = (start - 1, end);
            public int Current { get; private set; }
            private readonly int _end;
            public bool MoveNext() => ++Current < _end;
        }
        #if NETSTANDARD2_1
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RangeEnumerator GetEnumerator(this System.Range range)
        {
            if (range.Start.IsFromEnd || range.End.IsFromEnd)
                ThrowIsFromEnd();

            if (range.Start.Value > range.End.Value)
                ThrowStartIsGreaterThanEnd();

            return new RangeEnumerator(range.Start.Value, range.End.Value);

            static void ThrowIsFromEnd() => throw new ArgumentException("range start and end must not be from end");
            static void ThrowStartIsGreaterThanEnd() => throw new ArgumentException("start is greater than end");
        }
        #endif
    }
}
