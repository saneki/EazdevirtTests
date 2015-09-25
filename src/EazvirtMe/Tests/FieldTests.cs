using System;
using System.Reflection;
using NUnit.Framework;

namespace EazvirtMe.Tests
{
	[TestFixture]
	public class FieldTests
	{
		public String InstanceField1 = "My own field";
		public static Byte StaticField1 = 0x6F;

		[Test]
		[Obfuscation(Feature = "virtualization", Exclude = false)]
		public void Test1()
		{
			// ldsfld of own type
			Assert.AreEqual(StaticField1, 0x6F);

			// ldfld of own type
			Assert.AreEqual(InstanceField1, "My own field");

			// ldsfld of other type (TypeDef)
			Assert.AreEqual(Field_Test1_Class.StaticField1, "I'm a static field!");

			// ldfld of instance of other type (TypeDef)
			Assert.AreEqual(new Field_Test1_Class().InstanceField1, 908070);
		}
	}

	class Field_Test1_Class
	{
		public Int32 InstanceField1 = 908070;
		public static String StaticField1 = "I'm a static field!";
	}
}
