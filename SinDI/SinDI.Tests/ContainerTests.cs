using System;
using NUnit.Framework;

namespace SinDI.Tests
{
	[TestFixture]
	public class ContainerTests
	{
		private ISinContainer _container;

		[SetUp]
		public void SetUp()
		{
			_container = new SinContainer();
		}

		[Test]
		public void Conainter_RegisterInstance_RegistrationSuccesfull()
		{
			string elem = "sapte";

			_container.Register(elem);

			var result = _container.Resolve<string>();

			Assert.AreSame(elem, result);
		}

		[Test]
		public void Container_RegisterInterface_ResolvesInstanceSuccesfully()
		{
			_container.Register<ITestObj, TestObj>();

			var result = _container.Resolve<ITestObj>();

			Assert.NotNull(result);
		}

		[Test]
		public void Container_RegisterInstance_ResolvesInstanceSuccesfull()
		{
			_container.Register<TestObj>();

			var result = _container.Resolve<TestObj>();

			Assert.NotNull(result);
		}
	}

	internal interface ITestObj { }

	internal class TestObj : ITestObj { }
}
