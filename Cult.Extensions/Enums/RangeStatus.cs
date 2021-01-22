// ReSharper disable All
namespace Cult.Extensions.Guard
{
    public enum RangeStatus
    {
        // (,)
        InclusiveBoth,
        // [,]
        ExclusiveBoth,
        // (,]
        InclusiveFromExclusiveTo,
        // [,)
        ExclusiveFromInclusiveTo,
    }
}