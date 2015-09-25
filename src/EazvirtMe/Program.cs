using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

// Disable string encryption for now
[assembly: Obfuscation(Feature = "string encryption", Exclude = true)]

namespace EazvirtMe
{
	class Program
	{
		static void Main(String[] args)
		{
			String filepath = Process.GetCurrentProcess().MainModule.FileName,
			       filename = Path.GetFileName(filepath);
			Console.WriteLine("usage: nunit-console {0}", filename);
		}
	}
}
