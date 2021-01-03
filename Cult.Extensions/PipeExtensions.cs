using System;
// ReSharper disable UnusedMember.Global

namespace Cult.Extensions
{
    public static class PipeExtensions
    {
        public static TR Pipe<T, TR>(this T target, Func<T, TR> func)
        {
            return func(target);
        }

        public static TR Pipe<T, T1, TR>(this T target, T1 arg1, Func<T1, T, TR> func)
        {
            return func(arg1, target);
        }

        public static TR Pipe<T, T1, T2, TR>(this T target, T1 arg1, T2 arg2, Func<T1, T2, T, TR> func)
        {
            return func(arg1, arg2, target);
        }

        public static TR Pipe<T, T1, T2, T3, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, Func<T1, T2, T3, T, TR> func)
        {
            return func(arg1, arg2, arg3, target);
        }

        public static TR Pipe<T, T1, T2, T3, T4, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<T1, T2, T3, T4, T, TR> func)
        {
            return func(arg1, arg2, arg3, arg4, target);
        }

        public static TR Pipe<T, T1, T2, T3, T4, T5, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<T1, T2, T3, T4, T5, T, TR> func)
        {
            return func(arg1, arg2, arg3, arg4, arg5, target);
        }

        public static TR Pipe<T, T1, T2, T3, T4, T5, T6, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<T1, T2, T3, T4, T5, T6, T, TR> func)
        {
            return func(arg1, arg2, arg3, arg4, arg5, arg6, target);
        }

        public static TR Pipe<T, T1, T2, T3, T4, T5, T6, T7, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, Func<T1, T2, T3, T4, T5, T6, T7, T, TR> func)
        {
            return func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, target);
        }

        public static TR Pipe<T, T1, T2, T3, T4, T5, T6, T7, T8, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, Func<T1, T2, T3, T4, T5, T6, T7, T8, T, TR> func)
        {
            return func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, target);
        }

        public static TR Pipe<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T, TR> func)
        {
            return func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, target);
        }

        public static TR Pipe<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T, TR> func)
        {
            return func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, target);
        }

        public static TR Pipe<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T, TR> func)
        {
            return func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, target);
        }

        public static TR Pipe<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T, TR> func)
        {
            return func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, target);
        }

        public static TR Pipe<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T, TR> func)
        {
            return func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, target);
        }

        public static TR Pipe<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T, TR> func)
        {
            return func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, target);
        }

        public static TR Pipe<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T, TR> func)
        {
            return func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15, target);
        }


        public static TR PipeBackward<T, TR>(this T target, Func<T, TR> func)
        {
            return func(target);
        }

        public static TR PipeBackward<T, T1, TR>(this T target, T1 arg1, Func<T, T1, TR> func)
        {
            return func(target, arg1);
        }

        public static TR PipeBackward<T, T1, T2, TR>(this T target, T1 arg1, T2 arg2, Func<T, T1, T2, TR> func)
        {
            return func(target, arg1, arg2);
        }

        public static TR PipeBackward<T, T1, T2, T3, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, Func<T, T1, T2, T3, TR> func)
        {
            return func(target, arg1, arg2, arg3);
        }

        public static TR PipeBackward<T, T1, T2, T3, T4, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Func<T, T1, T2, T3, T4, TR> func)
        {
            return func(target, arg1, arg2, arg3, arg4);
        }

        public static TR PipeBackward<T, T1, T2, T3, T4, T5, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, Func<T, T1, T2, T3, T4, T5, TR> func)
        {
            return func(target, arg1, arg2, arg3, arg4, arg5);
        }

        public static TR PipeBackward<T, T1, T2, T3, T4, T5, T6, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, Func<T, T1, T2, T3, T4, T5, T6, TR> func)
        {
            return func(target, arg1, arg2, arg3, arg4, arg5, arg6);
        }

        public static TR PipeBackward<T, T1, T2, T3, T4, T5, T6, T7, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, Func<T, T1, T2, T3, T4, T5, T6, T7, TR> func)
        {
            return func(target, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }

        public static TR PipeBackward<T, T1, T2, T3, T4, T5, T6, T7, T8, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, Func<T, T1, T2, T3, T4, T5, T6, T7, T8, TR> func)
        {
            return func(target, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }

        public static TR PipeBackward<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, Func<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, TR> func)
        {
            return func(target, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
        }

        public static TR PipeBackward<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, Func<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TR> func)
        {
            return func(target, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
        }

        public static TR PipeBackward<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, Func<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TR> func)
        {
            return func(target, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11);
        }

        public static TR PipeBackward<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, Func<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TR> func)
        {
            return func(target, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12);
        }

        public static TR PipeBackward<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, Func<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TR> func)
        {
            return func(target, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13);
        }

        public static TR PipeBackward<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, Func<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TR> func)
        {
            return func(target, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14);
        }

        public static TR PipeBackward<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TR>(this T target, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9, T10 arg10, T11 arg11, T12 arg12, T13 arg13, T14 arg14, T15 arg15, Func<T, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TR> func)
        {
            return func(target, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, arg11, arg12, arg13, arg14, arg15);
        }
    }
}
