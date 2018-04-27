using System.Collections.Generic;

namespace SinDI.Models
{
	public class Pair<TKey, TValue>
	{
		public TKey Key { get; set; }
		public TValue Value { get; set; }

		public Pair(TKey key, TValue value)
		{
			Key = key;
			Value = value;
		}

		protected bool Equals(Pair<TKey, TValue> other)
		{
			return EqualityComparer<TKey>.Default.Equals(Key, other.Key);
		}

		public override bool Equals(object obj)
		{
			return !ReferenceEquals(null, obj) && (ReferenceEquals(this, obj) ||
												   obj.GetType() == GetType() && Equals((Pair<TKey, TValue>)obj));
		}

		public override int GetHashCode()
		{
			return EqualityComparer<TKey>.Default.GetHashCode(Key);
		}

		internal sealed class KeyEqualityComparer : IEqualityComparer<Pair<TKey, TValue>>
		{
			public bool Equals(Pair<TKey, TValue> x, Pair<TKey, TValue> y)
			{
				return ReferenceEquals(x, y) || !ReferenceEquals(x, null) && !ReferenceEquals(y, null) &&
					   x.GetType() == y.GetType() && EqualityComparer<TKey>.Default.Equals(x.Key,
						   y.Key);
			}

			public int GetHashCode(Pair<TKey, TValue> obj)
			{
				return EqualityComparer<TKey>.Default.GetHashCode(obj.Key);
			}
		}

		public static IEqualityComparer<Pair<TKey, TValue>> KeyComparer { get; } = new KeyEqualityComparer();
	}
}
