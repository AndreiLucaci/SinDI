namespace SinDI.Models
{
	public class KnownCtor
	{
		public object[] InjectionParams { get; }

		public KnownCtor(params object[] injectionParams)
		{
			InjectionParams = injectionParams;
		}
	}
}
