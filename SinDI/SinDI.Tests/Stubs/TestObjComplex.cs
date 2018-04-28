namespace SinDI.Tests.Stubs
{
	internal class TestObjComplex
	{
		public ITestObj Obj1 { get; }
		public ITestObj Obj2 { get; }

		public TestObjComplex(ITestObj obj1, ITestObj obj2)
		{
			Obj1 = obj1;
			Obj2 = obj2;
		}

		public TestObjComplex(ITestObj obj1)
		{
			Obj1 = obj1;
		}
	}
}
