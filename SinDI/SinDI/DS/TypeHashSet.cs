using System;
using System.Collections.Generic;
using System.Linq;
using SinDI.Hash;
using SinDI.Models;

namespace SinDI.DS
{
	public class TypeHashSet<TValue>
	{
		private readonly HashSet<Pair<ulong, TValue>> _items;
		private readonly Hasher _hasher;

		public TypeHashSet()
		{
			_items = new HashSet<Pair<ulong, TValue>>(new Pair<ulong, TValue>.KeyEqualityComparer());
			_hasher = new Hasher();
		}

		public void Add(Type key, TValue value)
		{
			var hashedKey = _hasher.ComputeHash(key);

			var pair = new Pair<ulong, TValue>(hashedKey, value);

			_items.Add(pair);
		}

		public TValue Get(Type type)
		{
			var hash = _hasher.ComputeHash(type);
			var pair = _items.FirstOrDefault(i => i.Key == hash);

			if (pair == null)
			{
				throw new KeyNotFoundException(type.FullName);
			}

			return pair.Value;
		}

		public bool TryGet(Type type, out TValue value)
		{
			try
			{
				value = Get(type);
			}
			catch (KeyNotFoundException)
			{
				value = default(TValue);
				return false;
			}
			return true;
		}
	}
}
