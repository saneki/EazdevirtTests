using System;
using System.Reflection;
using NUnit.Framework;

namespace EazvirtMe.Tests
{
	[TestFixture]
	public class StindTests
	{
		[Test]
		[Obfuscation(Feature = "virtualization", Exclude = false)]
		public void Test1()
		{
			int arg1 = 0;
			string arg2 = null;
			Object arg3 = null;
			List arg4 = null;
			Boolean arg5 = false;

			Test1_Ldarg(ref arg1, ref arg2, ref arg3, ref arg4, ref arg5);

			Assert.AreEqual(arg1, 100);
			Assert.AreEqual(arg2, "Hello!");
			Assert.IsNotNull(arg3);
			Assert.IsNotNull(arg4);
			Assert.IsTrue(arg5);
		}

		[Obfuscation(Feature = "virtualization", Exclude = false)]
		void Test1_Ldarg(ref int arg1, ref string arg2, ref Object arg3, ref List arg4, ref Boolean arg5)
		{
			arg1 = 100;
			arg2 = "Hello!";
			arg3 = new Object();
			arg4 = new List();
			arg5 = true;
		}
	}
}