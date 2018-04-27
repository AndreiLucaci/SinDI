using System;
using System.Linq;
using SinDI.DS;

namespace SinDI
{
	public class SinContainer : ISinContainer
	{
		private readonly TypeHashSet<Func<object>> _registeredItems;

		public SinContainer()
		{
			_registeredItems = new TypeHashSet<Func<object>>();
		}

		public void Register<TInstance>()
		{
			Register<TInstance, TInstance>();
		}

		public void Register<TInstance>(TInstance instance)
		{
			_registeredItems.Add(typeof(TInstance), () => instance);
		}

		public void Register<TBase, TDerived>()
			where TDerived : TBase
		{
			_registeredItems.Add(typeof(TBase), () => Ctor(typeof(TDerived)));
		}

		public TInstance Resolve<TInstance>()
		{
			return (TInstance)Resolve(typeof(TInstance));
		}

		private object Resolve(Type type)
		{
			Func<object> creator;
			if (_registeredItems.TryGet(type, out creator))
			{
				return creator();
			}
			if (!type.IsAbstract)
			{
				return Ctor(type);
			}
			throw new TypeAccessException($"No registered type found for {type}");
		}

		private object Ctor(Type type)
		{
			var ctor = type.GetConstructors().Single();
			var paramTypes = ctor.GetParameters().Select(p => p.ParameterType);
			var ctorParams = paramTypes.Select(Resolve).ToArray();
			return Activator.CreateInstance(type, ctorParams);
		}
	}
}
