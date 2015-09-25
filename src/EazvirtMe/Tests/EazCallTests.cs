using System;
using System.Reflection;
using NUnit.Framework;

namespace EazvirtMe.Tests
{
	[TestFixture]
	public class EazCallTests
	{
		[Test]
		[Obfuscation(Feature = "virtualization", Exclude = false)]
		public void Test1()
		{
			// Call another virtualized method
			this.Test1_Called();

			// Call another virtualized method, static
			// ---
			// Note: This causes an interesting issue: Both Test1 and Test1_Called_Static
			// end up with the same method name after obfuscation (\0002). This seems to be
			// because Eazfuscator has two "name counters," one for instance methods and
			// another for static methods, per type. Because of this, when devirtualizing
			// this method, the following call to Test1_Called_Static is resolved as a
			// call to Test1, resulting in an exception.
			// ---
			// EazCallTests.Test1_Called_Static();

			// Call another virtualized method, from an instance of another type
			// and with a param + return value
			var instanceClass = new EazCall_Test1_InstanceClass();
			Assert.AreEqual(instanceClass.Test1_Called_OtherClass(1234), "1234");
		}

		[Obfuscation(Feature = "virtualization", Exclude = false)]
		void Test1_Called()
		{
		}

		[Obfuscation(Feature = "virtualization", Exclude = false)]
		static void Test1_Called_Static()
		{
		}
	}

	class EazCall_Test1_InstanceClass
	{
		[Obfuscation(Feature = "virtualization", Exclude = false)]
		public String Test1_Called_OtherClass(Int32 someParam)
		{
			return someParam.ToString();
		}
	}
}
