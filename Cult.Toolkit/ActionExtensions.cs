using System;
// ReSharper disable All 
namespace Cult.Toolkit.ExtraAction
{
    public static class ActionExtensions
    {
        public static Action<object> ToActionObject<T>(this Action<T> actionT)
        {
            return actionT == null ? null : new Action<object>(o => actionT((T)o));
        }

        public static Func<ValueTuple> ToFunc(this Action @this)
            => () =>
            {
                @this();
                return new ValueTuple();
            };

        public static Func<T, ValueTuple> ToFunc<T>(this Action<T> @this)
            => t =>
            {
                @this(t);
                return new ValueTuple();
            };

        public static Func<T1, T2, ValueTuple> ToFunc<T1, T2>(this Action<T1, T2> @this)
            => (t1, t2) =>
            {
                @this(t1, t2);
                return new ValueTuple();
            };

        public static Func<T1, T2, T3, ValueTuple> ToFunc<T1, T2, T3>(this Action<T1, T2, T3> @this)
            => (t1, t2, t3) =>
            {
                @this(t1, t2, t3);
                return new ValueTuple();
            };

        public static Func<T1, T2, T3, T4, ValueTuple> ToFunc<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> @this)
            => (t1, t2, t3, t4) =>
            {
                @this(t1, t2, t3, t4);
                return new ValueTuple();
            };

        public static Func<T1, T2, T3, T4, T5, ValueTuple> ToFunc<T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> @this)
            => (t1, t2, t3, t4, t5) =>
            {
                @this(t1, t2, t3, t4, t5);
                return new ValueTuple();
            };

        public static Func<T1, T2, T3, T4, T5, T6, ValueTuple> ToFunc<T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6> @this)
            => (t1, t2, t3, t4, t5, t6) =>
            {
                @this(t1, t2, t3, t4, t5, t6);
                return new ValueTuple();
            };

        public static Func<T1, T2, T3, T4, T5, T6, T7, ValueTuple> ToFunc<T1, T2, T3, T4, T5, T6, T7>(this Action<T1, T2, T3, T4, T5, T6, T7> @this)
            => (t1, t2, t3, t4, t5, t6, t7) =>
            {
                @this(t1, t2, t3, t4, t5, t6, t7);
                return new ValueTuple();
            };

        public static Func<T1, T2, T3, T4, T5, T6, T7, T8, ValueTuple> ToFunc<T1, T2, T3, T4, T5, T6, T7, T8>(this Action<T1, T2, T3, T4, T5, T6, T7, T8> @this)
            => (t1, t2, t3, t4, t5, t6, t7, t8) =>
            {
                @this(t1, t2, t3, t4, t5, t6, t7, t8);
                return new ValueTuple();
            };
    }
}
