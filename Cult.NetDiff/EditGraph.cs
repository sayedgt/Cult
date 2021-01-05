using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable All 
namespace Cult.NetDiff
{
    internal enum Direction
    {
        Right,
        Bottom,
        Diagonal,
    }
    internal readonly struct Point : IEquatable<Point>
    {
        public int X { get; }
        public int Y { get; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Point))
                return false;

            return Equals((Point)obj);
        }
        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }
        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 23 + X.GetHashCode();
            hash = hash * 23 + Y.GetHashCode();

            return hash;
        }
        public override string ToString()
        {
            return $"X:{X} Y:{Y}";
        }
    }
    internal class EditGraph<T>
    {
        private readonly T[] _seq1;
        private readonly T[] _seq2;
        private DiffOption<T> _option;
        private List<Node> _heads;
        private readonly Point _endpoint;
        private int[] _farthestPoints;
        private readonly int _offset;
        private bool _isEnd;
        public EditGraph(
                    IEnumerable<T> seq1, IEnumerable<T> seq2)
        {
            this._seq1 = seq1.ToArray();
            this._seq2 = seq2.ToArray();
            _endpoint = new Point(this._seq1.Length, this._seq2.Length);
            _offset = this._seq2.Length;
        }
        private void BeginCalculatePath()
        {
            Initialize();

            _heads.Add(new Node(new Point(0, 0)));

            Snake();
        }
        public List<Point> CalculatePath(DiffOption<T> option)
        {
            if (!_seq1.Any())
                return Enumerable.Range(0, _seq2.Length + 1).Select(i => new Point(0, i)).ToList();

            if (!_seq2.Any())
                return Enumerable.Range(0, _seq1.Length + 1).Select(i => new Point(i, 0)).ToList();

            this._option = option;

            BeginCalculatePath();

            while (Next()) { }

            return EndCalculatePath();
        }
        private bool CanCreateHead(Point currentPoint, Direction direction, Point nextPoint)
        {
            if (!InRange(nextPoint))
                return false;

            if (direction == Direction.Diagonal)
            {
                var equal = _option.EqualityComparer?.Equals(_seq1[nextPoint.X - 1], (_seq2[nextPoint.Y - 1])) ?? _seq1[nextPoint.X - 1].Equals(_seq2[nextPoint.Y - 1]);

                if (!equal)
                    return false;
            }

            return UpdateFarthestPoint(nextPoint);
        }
        private List<Point> EndCalculatePath()
        {
            var wayPoint = new List<Point>();

            var current = _heads.FirstOrDefault(h => h.Point.Equals(_endpoint));
            while (current != null)
            {
                wayPoint.Add(current.Point);

                current = current.Parent;
            }

            wayPoint.Reverse();

            return wayPoint;
        }
        private Point GetPoint(Point currentPoint, Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    return new Point(currentPoint.X + 1, currentPoint.Y);
                case Direction.Bottom:
                    return new Point(currentPoint.X, currentPoint.Y + 1);
                case Direction.Diagonal:
                    return new Point(currentPoint.X + 1, currentPoint.Y + 1);
            }

            throw new ArgumentException();
        }
        private void Initialize()
        {
            _farthestPoints = new int[_seq1.Length + _seq2.Length + 1];
            _heads = new List<Node>();
        }
        private bool InRange(Point point)
        {
            return point.X >= 0 && point.Y >= 0 && point.X <= _endpoint.X && point.Y <= _endpoint.Y;
        }
        private bool Next()
        {
            if (_isEnd)
                return false;

            UpdateHeads();

            return true;
        }
        private void Snake()
        {
            var tmp = new List<Node>();
            foreach (var h in _heads)
            {
                var newHead = Snake(h);

                tmp.Add(newHead ?? h);
            }

            _heads = tmp;
        }
        private Node Snake(Node head)
        {
            Node newHead = null;
            while (true)
            {
                if (TryCreateHead(newHead ?? head, Direction.Diagonal, out var tmp))
                    newHead = tmp;
                else
                    break;
            }

            return newHead;
        }
        private bool TryCreateHead(Node head, Direction direction, out Node newHead)
        {
            newHead = null;
            var newPoint = GetPoint(head.Point, direction);

            if (!CanCreateHead(head.Point, direction, newPoint))
                return false;

            newHead = new Node(newPoint) { Parent = head };

            _isEnd |= newHead.Point.Equals(_endpoint);

            return true;
        }
        private bool UpdateFarthestPoint(Point point)
        {
            var k = point.X - point.Y;
            var y = _farthestPoints[k + _offset];

            if (point.Y <= y)
                return false;

            _farthestPoints[k + _offset] = point.Y;

            return true;
        }
        private void UpdateHeads()
        {
            if (_option.Limit > 0 && _heads.Count > _option.Limit)
            {
                var tmp = _heads.First();
                _heads.Clear();

                _heads.Add(tmp);
            }

            var updated = new List<Node>();

            foreach (var head in _heads)
            {
                if (TryCreateHead(head, Direction.Right, out var rightHead))
                {
                    updated.Add(rightHead);
                }

                if (TryCreateHead(head, Direction.Bottom, out var bottomHead))
                {
                    updated.Add(bottomHead);
                }
            }

            _heads = updated;

            Snake();
        }
    }
    internal class Node
    {
        public Node Parent { get; set; }
        public Point Point { get; set; }
        public Node(Point point)
        {
            Point = point;
        }
        public override string ToString()
        {
            return $"X:{Point.X} Y:{Point.Y}";
        }
    }
}
