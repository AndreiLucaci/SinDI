namespace SinDI
{
	public interface ISinContainer
	{
		void Register<TInstance>();
		void Register<TInstance>(TInstance instance);
		void Register<TService, TInstance>() where TInstance : TService;
		TInstance Resolve<TInstance>();
	}
}
