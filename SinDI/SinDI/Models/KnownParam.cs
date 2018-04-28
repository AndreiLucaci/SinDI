using System;

namespace SinDI.Models
{
	public class KnownParam
	{
		public KnownParam(Type type)
		{
			Value = type;
		}

		public Type Value { get; set; }
	}

	public class KnownParam<T> : KnownParam
	{
		public KnownParam() : base(typeof(T))
		{
		}
	}
}
