namespace SinDI.Tests.Stubs
{
	internal class TestObjComplexWithSingleConstructor
	{
		public ITestObj Obj1 { get; }

		public TestObjComplexWithSingleConstructor(ITestObj obj1)
		{
			Obj1 = obj1;
		}
	}
}
