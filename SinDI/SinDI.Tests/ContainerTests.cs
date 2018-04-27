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
	}
}
