using System;
using System.Reflection;
using EazvirtMe.Attributes;

namespace EazvirtMe.Tests
{
	[EazTest]
	public class EazCallTests
	{
		[Obfuscation(Feature = "virtualization", Exclude = false)]
		public Boolean Test1()
		{
			// Call another virtualized method
			this.Test1_Called();

			// Call another virtualized method, static
			EazCallTests.Test1_Called_Static();

			// Call another virtualized method, from an instance of another type
			// and with a param + return value
			var instanceClass = new Test1_InstanceClass();
			if (!instanceClass.Test1_Called_OtherClass(1234).Equals("1234"))
				return false;

			return true;
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

	class Test1_InstanceClass
	{
		[Obfuscation(Feature = "virtualization", Exclude = false)]
		public String Test1_Called_OtherClass(Int32 someParam)
		{
			return someParam.ToString();
		}
	}
}
