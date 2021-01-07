using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable All
namespace Cult.Functional
{
    public class Result<T> : IResult<T>
    {
        public ICollection<string> Errors { get; private set; } = new List<string>();
        T IResult<T>.Value => Value;
        public ResultStatus Status { get; }
        public string SuccessMessage { get; private set; } = string.Empty;
        public ICollection<ValidationError> ValidationErrors { get; private set; } = new List<ValidationError>();
        public T Value { get; }
        public Type ValueType { get; }
        public Result(T value)
        {
            Value = value;
            if (Value != null)
            {
                ValueType = Value.GetType();
            }
        }
        private Result(ResultStatus status)
        {
            Status = status;
        }
        public void AddErrorMessages(params string[] errorMessages)
        {
            foreach (var item in errorMessages)
            {
                Errors.Add(item);
            }
        }
        public void AddSuccessMessage(string successMessage)
        {
            SuccessMessage = successMessage;
        }
        public void AddValidationErrorMessages(params ValidationError[] validationErrors)
        {
            foreach (var item in validationErrors)
            {
                ValidationErrors.Add(item);
            }
        }
        public static Result<T> Error(params string[] errorMessages)
        {
            return new Result<T>(ResultStatus.Error) { Errors = errorMessages };
        }
        public static Result<T> Error(Exception exception)
        {
            return new Result<T>(ResultStatus.Error) { Errors = new List<string>() { exception.Message } };
        }
        public static Result<T> Forbidden()
        {
            return new Result<T>(ResultStatus.Forbidden);
        }
        public static Result<T> IfError(T value, Func<T, bool> predicate, params string[] errorMessages)
        {
            if (!predicate(value))
            {
                Error(errorMessages);
            }
            return Success(value);
        }
        public static Result<T> IfForbidden(T value, Func<T, bool> predicate)
        {
            if (!predicate(value))
            {
                Forbidden();
            }
            return Success(value);
        }
        public static Result<T> IfInvalid(T value, Func<T, bool> predicate, params ValidationError[] validationErrors)
        {
            if (!predicate(value))
            {
                Invalid(validationErrors.ToList());
            }
            return Success(value);
        }
        public static Result<T> IfNotFound(T value, Func<T, bool> predicate)
        {
            if (!predicate(value))
            {
                NotFound();
            }
            return Success(value);
        }
        public static implicit operator Result<T>(T value) => Success(value);
        public static implicit operator T(Result<T> result) => result.Value;
        public static Result<T> Invalid(List<ValidationError> validationErrors)
        {
            return new Result<T>(ResultStatus.Invalid) { ValidationErrors = validationErrors };
        }
        public static Result<T> NotFound()
        {
            return new Result<T>(ResultStatus.NotFound);
        }
        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }
        public static Result<T> Success(T value, string successMessage)
        {
            return new Result<T>(value) { SuccessMessage = successMessage };
        }
        public static Result<T> Success()
        {
            return Success(default);
        }
        public Result<T> ToError()
        {
            return new Result<T>(ResultStatus.Error) { Errors = Errors };
        }
        public Result<T> ToInvalid()
        {
            return new Result<T>(ResultStatus.Invalid) { ValidationErrors = ValidationErrors };
        }
        public Result<T> ToSuccess()
        {
            return new Result<T>(Value) { SuccessMessage = SuccessMessage };
        }
        public static Result<T> Try(Action action, Exception customException = null)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                return Error(customException ?? ex);
            }
            return Success(default);
        }
        public static Result<T> Try(T value, Action<T> action, Exception customException = null)
        {
            try
            {
                action(value);
            }
            catch (Exception ex)
            {
                return Error(customException ?? ex);
            }
            return Success(default);
        }
        public static Result<T> Try(Func<T> func, Exception customException = null)
        {
            T result;
            try
            {
                result = func();
            }
            catch (Exception ex)
            {
                return Error(customException ?? ex);
            }
            return Success(result);
        }
        public static Result<T> Try(T value, Func<T, T> func, Exception customException = null)
        {
            T result;
            try
            {
                result = func(value);
            }
            catch (Exception ex)
            {
                return Error(customException ?? ex);
            }
            return Success(result);
        }
    }
}
