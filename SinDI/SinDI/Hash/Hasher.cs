using System;
using System.Linq;

namespace SinDI.Hash
{
	public class Hasher
	{
		private const ulong BaseHash = 3074457345618258791ul;
		private const ulong MulitplyHash = 3074457345618258799ul;

		public ulong ComputeHash(Type type)
		{
			return ComputeHash(type.AssemblyQualifiedName);
		}

		private ulong ComputeHash(string input)
		{
			return input.Aggregate(BaseHash, (current, chr) => current + (current + chr) * MulitplyHash);
		}
	}
}
