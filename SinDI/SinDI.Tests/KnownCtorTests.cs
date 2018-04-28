using NUnit.Framework;
using SinDI.Models;
using SinDI.Tests.Stubs;

namespace SinDI.Tests
{
	[TestFixture]
	public class KnownCtorTests
	{
		private const string Expecteditem = "expectedItem";
		private ISinContainer _container;

		[SetUp]
		public void SetUp()
		{
			_container = new SinContainer();
		}

		[Test]
		public void Container_KnownCtorWithConcreteParam_ResolvesSuccesfully()
		{
			ITestObj expectedTestObj = new TestObj {Item = Expecteditem};

			_container.Register<TestObjComplex>(new KnownCtor(expectedTestObj));

			var result = _container.Resolve<TestObjComplex>();

			Assert.NotNull(result);
			Assert.AreEqual(expectedTestObj, result.Obj1);
		}

		[Test]
		public void Container_KnownCtorWithRegisteredTypes_ResolvesSuccesfully()
		{
			ITestObj expectedTestObj = new TestObj { Item = Expecteditem };

			_container.Register(expectedTestObj);
			_container.Register<TestObjComplexWithSingleConstructor>();

			var result = _container.Resolve<TestObjComplexWithSingleConstructor>();

			Assert.NotNull(result);
			Assert.AreEqual(expectedTestObj, result.Obj1);
		}

		[Test]
		public void Container_KnownCtorWithTypeRegistered_ResolvesSuccesfully()
		{
			_container.Register<ITestObj, TestObj>();
			_container.Register<TestObjComplexWithSingleConstructor>();

			var result = _container.Resolve<TestObjComplexWithSingleConstructor>();

			Assert.NotNull(result);
			Assert.NotNull(result.Obj1);
			Assert.Null(result.Obj1.Item);
		}
	}
}
