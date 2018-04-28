using SinDI.Models;

namespace SinDI
{
	public interface ISinContainer
	{
		void Register<TInstance>(KnownCtor knownCtor = null);
		void Register<TInstance>(TInstance instance);
		void Register<TService, TInstance>(KnownCtor knownCtor = null) where TInstance : TService;
		TInstance Resolve<TInstance>();
	}
}
