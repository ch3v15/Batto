using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Resources;
using Microsoft.CSharp;

namespace Batto
{
	class Program
	{
		public static void Main(string[] Args)
		{
			try
			{
				ColorWriteLine("File resource generation", ConsoleColor.Yellow);
				CreateResourceFile(Args[0]);
				ColorWriteLine("Program assembly", ConsoleColor.Yellow);
				BuildExecutableProgram(Args[0]);
				ColorWriteLine("Programs successfully assembled", ConsoleColor.Green);
			}
			catch { ColorWriteLine("Error compiling file", ConsoleColor.Red); }
		}

		public static void ColorWriteLine(string Value, ConsoleColor Color)
		{
			Console.ForegroundColor = Color;
			Console.WriteLine(Value);
			Console.ForegroundColor = ConsoleColor.White;
		}

		public static void CreateResourceFile(string PathToFile)
		{
			using (ResourceWriter Writer = new ResourceWriter("Batto.resources"))
			{
				Writer.AddResource("batch", File.ReadAllText(PathToFile));
				Writer.Generate();
				Writer.Close();
			}
		}

		public static void BuildExecutableProgram(string NameApplication)
		{
			CompilerParameters Params = new CompilerParameters();
			Params.GenerateExecutable = true;
			Params.ReferencedAssemblies.Add("System.dll");
			Params.OutputAssembly = NameApplication.Replace(".bat", "") + ".exe";
			Params.EmbeddedResources.Add("Batto.resources");
			Params.CompilerOptions = "\n/t:winexe";

			string Source = Properties.Resources.Batto;
			new CSharpCodeProvider().CompileAssemblyFromSource(Params, Source);
			File.Delete("Batto.resources");
		}
	}
}