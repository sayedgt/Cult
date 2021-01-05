using System;
namespace Cult.Utilities
{
    public static class RandomUtility
    {
        public static DateTime GetRandomDateTime(DateTime? startDateTime = null, DateTime? endDateTime = null)
        {
            var rnd = GetUniqueRandom();
            var rndYear = new Random() .Next(-100, +100);
            var start = startDateTime ?? DateTime.Now.AddYears(rndYear);
            var end = endDateTime ?? DateTime.Now;
            var range = (end - start).Days;
            return start.AddDays(rnd.Next(range)).AddHours(rnd.Next(0, 24)).AddMinutes(rnd.Next(0, 60)).AddSeconds(rnd.Next(0, 60));
        }
        public static Random GetUniqueRandom()
        {
            return new Random(Guid.NewGuid().GetHashCode());
        }
    }
}
