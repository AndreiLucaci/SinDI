using System;
using System.Collections.Generic;
using System.Linq;
using SinDI.DS;
using SinDI.Exceptions;
using SinDI.Models;

namespace SinDI
{
	public class SinContainer : ISinContainer
	{
		private readonly TypeHashSet<Func<object>> _registeredItems;

		public SinContainer()
		{
			_registeredItems = new TypeHashSet<Func<object>>();
		}

		public void Register<TInstance>(KnownCtor knownCtor = null)
		{
			Register<TInstance, TInstance>(knownCtor);
		}

		public void Register<TInstance>(TInstance instance)
		{
			_registeredItems.Add(typeof(TInstance), () => instance);
		}

		public void Register<TBase, TDerived>(KnownCtor knownCtor = null)
			where TDerived : TBase
		{
			if (knownCtor != null)
			{
				_registeredItems.Add(typeof(TBase), () => Ctor(typeof(TDerived), knownCtor));
				return;
			}
			_registeredItems.Add(typeof(TBase), () => Ctor(typeof(TDerived)));
		}

		public TInstance Resolve<TInstance>()
		{
			return (TInstance)Resolve(typeof(TInstance));
		}

		private object Resolve(Type type)
		{
			if (_registeredItems.TryGet(type, out var creator))
			{
				return creator();
			}
			if (!type.IsAbstract)
			{
				return Ctor(type);
			}
			throw new TypeNotFoundException($"No registered type found for {type}");
		}

		private object Ctor(Type type)
		{
			try
			{
				var ctor = type.GetConstructors().Single();
				var paramTypes = ctor.GetParameters().Select(p => p.ParameterType);
				var ctorParams = paramTypes.Select(Resolve).ToArray();
				return Activator.CreateInstance(type, ctorParams);
			}
			catch (InvalidOperationException)
			{
				throw new CtorNotFoundException($"Type {type} contains more constructors or is not properly registered.");
			}
		}

		private object Ctor(Type type, KnownCtor knownCtor)
		{
			var ctorParams = GetConstructorParams(knownCtor);
			return Activator.CreateInstance(type, ctorParams);
		}

		private object[] GetConstructorParams(KnownCtor knownCtor)
		{
			var constructorParams = new List<object>();
			foreach (var param in knownCtor.InjectionParams)
			{
			    if (param is KnownParam knownParam)
			    {
			        constructorParams.Add(Resolve(knownParam.Value));
			    }
			    else
			    {
			        constructorParams.Add(param);
			    }
			}
			return constructorParams.ToArray();
		}
	}
}
