using System;
using System.Collections.Generic;

namespace Cult.Toolkit.ExtraQueue
{
    public static class QueueExtensions
    {
		public static T DequeueOrDefault<T>(this Queue<T> self)
		{
			if (self == null)
				throw new ArgumentNullException(nameof(self));

			return (self.Count > 0) ? self.Dequeue() : default;
		}

		public static bool TryDequeue<T>(this Queue<T> self, out T element)
		{
			if (self == null)
				throw new ArgumentNullException(nameof(self));

			element = default;

			if (self.Count > 0)
			{
				element = self.Dequeue();
				return true;
			}

			return false;
		}
	}
}
}
