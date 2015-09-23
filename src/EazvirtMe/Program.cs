using EazvirtMe.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// Disable string encryption for now
[assembly: Obfuscation(Feature = "string encryption", Exclude = true)]

namespace EazvirtMe
{
	class Program
	{
		static void Main(String[] args)
		{
			var assembly = Assembly.GetExecutingAssembly();
			foreach (var type in assembly.DefinedTypes)
			{
				EazTestAttribute attribute;
				if ((attribute = (EazTestAttribute)type.GetCustomAttribute(typeof(EazTestAttribute))) != null)
					RunTests(type, attribute);
			}
		}

		static void RunTests(TypeInfo typeInfo, EazTestAttribute attribute)
		{
			if (!typeInfo.IsClass)
				return;

			var possibleMethods = typeInfo.DeclaredMethods.Where(
				m => m.IsPublic && !m.IsSpecialName);
				// m.GetParameters().Length == (m.IsStatic ? 0 : 1));
			var staticMethods = possibleMethods.Where(m => m.IsStatic);
			var instanceMethods = possibleMethods.Where(m => !m.IsStatic);

			Int32 staticCount = staticMethods.Count(), instanceCount = instanceMethods.Count();
			if (staticCount > 0 || instanceCount > 0)
			{
				Console.WriteLine("> Type: {0}", typeInfo.FullName);

				if (staticCount > 0)
				{
					Console.WriteLine(">>> Running static tests ({0})", staticMethods.Count());
					Int32 successes = RunStaticTests(typeInfo, staticMethods);
					Console.WriteLine(">>> [{0}/{1}] successful", successes, staticCount);
				}

				if (instanceCount > 0)
				{
					Console.WriteLine(">>> Running instance tests ({0})", instanceMethods.Count());
					Int32 successes = RunInstanceTests(typeInfo, instanceMethods);
					Console.WriteLine(">>> [{0}/{1}] successful", successes, instanceCount);
				}
			}
			else Console.WriteLine("> Type (empty): {0}", typeInfo.FullName);
		}

		static Int32 RunStaticTests(TypeInfo typeInfo, IEnumerable<MethodInfo> methods)
		{
			Int32 successes = 0;
			foreach (var method in methods)
				if (LogMethodSuccess(method, method.Invoke(null, null)))
					successes++;
			return successes;
		}

		static Int32 RunInstanceTests(TypeInfo typeInfo, IEnumerable<MethodInfo> methods)
		{
			Int32 successes = 0;
			Object instance = Activator.CreateInstance(typeInfo);
			foreach (var method in methods)
				if (LogMethodSuccess(method, method.Invoke(instance, null)))
					successes++;
			return successes;
		}

		static Boolean LogMethodSuccess(MethodInfo methodInfo, Object returnValue)
		{
			if (methodInfo.ReturnType == typeof(Boolean))
			{
				Boolean success = (Boolean)returnValue;
				Console.WriteLine(">>> [{0}] {1}", success ? "Passed" : "Failed", methodInfo.Name);
				return success;
			}
			else
			{
				Console.WriteLine(">>> [Passed] {0}", methodInfo.Name);
				return true;
			}
		}
	}
}
