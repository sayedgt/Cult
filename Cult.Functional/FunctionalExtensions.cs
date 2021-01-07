// ReSharper disable All
namespace Cult.Functional
{
    public static class FunctionalExtensions
    {
        public static Result<T> ToResult<T>(this Maybe<T> maybe, params string[] errorMessages)
        {
            if (!maybe.HasValue())
            {
                return Result<T>.Error(errorMessages);
            }

            var value = maybe.GetOrDefault(default);
            return Result<T>.Success(value);
        }
    }
}