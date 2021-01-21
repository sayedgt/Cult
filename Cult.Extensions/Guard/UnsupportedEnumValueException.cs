using System;
using System.Runtime.Serialization;

namespace Cult.Extensions.Guard
{
    public class UnsupportedEnumValueException<TEnum> : Exception
        where TEnum : Enum
    {
        public UnsupportedEnumValueException(string message)
            : base(message) { }
        public UnsupportedEnumValueException(string message, Exception inner)
            : base(message, inner) { }
        protected UnsupportedEnumValueException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        public UnsupportedEnumValueException(TEnum enumValue)
            : base($"Value {enumValue} of enum {typeof(TEnum).Name} is not supported.")
        { }
        public UnsupportedEnumValueException()
        { }
    }
}
