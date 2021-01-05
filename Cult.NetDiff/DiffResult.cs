namespace Cult.NetDiff
{
    public class DiffResult<T>
    {
        public T Obj1 { get; }
        public T Obj2 { get; }
        public DiffStatus Status { get; }
        public DiffResult(T obj1, T obj2, DiffStatus status)
        {
            Obj1 = obj1;
            Obj2 = obj2;
            Status = status;
        }
        public string ToFormatString()
        {
            var obj = Status == DiffStatus.Deleted ? Obj1 : Obj2;

            return $"{Status.GetStatusChar()} {obj}";
        }
        public override string ToString()
        {
            return $"Obj1:{Obj1.ToString() ?? string.Empty} Obj2:{Obj2.ToString() ?? string.Empty} Status:{Status}"
                .Replace("\0", "");
        }
    }
}
