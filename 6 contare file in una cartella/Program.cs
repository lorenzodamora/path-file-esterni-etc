using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_contare_file_in_una_cartella
{
	internal class Program
	{
		static void Main()
		{
			string path = Directory.GetCurrentDirectory();
            path = path.Remove(path.Length - 10);
			path = Path.GetDirectoryName(path);
            path += @"\txt";
			
			string[] filespath = Directory.GetFiles(path);
			string[] dirspath = Directory.GetDirectories(path);
			Console.WriteLine($"i file sono {filespath.Length} e le cartelle sono {dirspath.Length}\n\n"); //Directory.GetDirectories(path).Length

			foreach (string files in filespath)
				Console.WriteLine(files);

			foreach (string dir in dirspath)
				Console.WriteLine(dir+"\\");

			Console.CursorTop++;
			Console.WriteLine("con un altro metodo\n\n");
			string[] file_se = Directory.GetFileSystemEntries(path);
			Console.WriteLine($"i file system entries sono {file_se.Length}\n\n");

			foreach(string file in file_se)
				Console.WriteLine(file);

			//contare i file in ogni cartella, e se c'è una cartella contare anche quelli dentro e così via

			//string[][] files2;
			//foreach (string dir in dirspath)
			//{

			//}
		}
	}
}