using System;
using System.Runtime.Serialization;

namespace SinDI.Exceptions
{
	public class CtorNotFoundException : Exception
	{
		public CtorNotFoundException()
		{
		}

		public CtorNotFoundException(string message) : base(message)
		{
		}

		public CtorNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected CtorNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
