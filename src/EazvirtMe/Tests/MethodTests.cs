using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

namespace EazvirtMe.Tests
{
	[TestFixture]
	public class MethodTests
	{
		[Test]
		[Obfuscation(Feature = "virtualization", Exclude = false)]
		public void MethodSpec_Test1()
		{
			// Call static MethodSpec with signature T::Func<T>(T)
			Assert.AreEqual(
				Method_MethodSpec_StaticClass.GetParam<String>("Hello world!"),
				"Hello world!");

			// Call static MethodSpec with signature X<T>::Func<T>()
			Assert.IsNotNull(Method_MethodSpec_StaticClass.GetList<Int32>());

			// Call static MethodSpec with signature X<Y<T>>::Func<T>()
			Assert.IsNotNull(Method_MethodSpec_StaticClass.GetStackOfLists<Byte>());
		}

		[Test]
		[Obfuscation(Feature = "virtualization", Exclude = false)]
		public void Generics_Test1()
		{
			var instance1 = new Method_Generic_InstanceClass<String, Int32>("Instance 1", 4321);
			var instance2 = new Method_Generic_InstanceClass<String, Int32>
				.InternalClass<Object, Int16>(new Object(), 0);

			// Call MethodSpec of child class with generic types + parent with generics types
			Assert.IsNotNull(instance2.Method1<String>("param1", "param2", 13));

			instance2.Param3 = null;

			// Same as prior, with null
			Assert.IsNull(instance2.Method1<Object>(new Object(), "param2", 29));
		}
	}

	class Method_MethodSpec_StaticClass
	{
		public static T GetParam<T>(T param1)
		{
			return param1;
		}

		public static IList<T> GetList<T>()
		{
			return new List<T>();
		}

		public static Stack<List<T>> GetStackOfLists<T>()
		{
			return new Stack<List<T>>();
		}
	}

	class Method_Generic_InstanceClass<A, B>
	{
		public A Param1;
		public B Param2;

		public Method_Generic_InstanceClass(A param1, B param2)
		{
			Param1 = param1;
			Param2 = param2;
		}

		public class InternalClass<C, D>
		{
			public C Param3;
			public D Param4;

			public InternalClass(C param3, D param4)
			{
				Param3 = param3;
				Param4 = param4;
			}

			public C Method1<E>(E param1, A param2, D param3)
			{
				return Param3;
			}
		}
	}
}
