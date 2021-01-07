using System;
using System.Collections.Generic;
// ReSharper disable All
namespace Cult.Functional
{
    public interface IResult<T>
    {
        ResultStatus Status { get; }
        ICollection<string> Errors { get; }
        ICollection<ValidationError> ValidationErrors { get; }
        Type ValueType { get; }
        T Value { get; }
    }
}
