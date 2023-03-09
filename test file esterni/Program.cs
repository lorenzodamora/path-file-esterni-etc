using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.IO;
using System.Reflection;

using System.Drawing;
//

namespace test_file_esterni
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string line;
			string path = "";
			try
			{
				path = Path.GetFullPath(".");
				for (int i = 3; i > 0; i--) //il 3 dipende dalla gestione della cartella della soluzione
				{ //in breve: torna indietro di una cartella
					path = Path.GetDirectoryName(path);
				}
				path += "\\Sample.txt"; //aggiungi il file
				Console.WriteLine(path);

				//Pass the file path and file name to the StreamReader constructor
				StreamReader sr = new StreamReader(path); //altrimenti mettere il file in  ...\bin\debug\  file
														  //Read the first line of text
				line = sr.ReadLine();
				//Continue to read until you reach end of file
				while (line != null)
				{
					//write the line to console window
					Console.WriteLine(line);
					//Read the next line
					line = sr.ReadLine();
				}
				//close the file
				sr.Close();
				Console.ReadLine();
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception: " + e.Message);
			}
			finally
			{
				Console.WriteLine("Executing finally block.");
			}


			try
			{
				Console.WriteLine(path); //PERCHE è VUOTO
                path = Path.GetDirectoryName(path);

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(path);
				//Write a line of text
				sw.WriteLine("Hello World!!");
				//Write a second line of text
				sw.WriteLine("From the StreamWriter class");
				//Close the file
				sw.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception: " + e.Message);
			}
			finally
			{
				Console.WriteLine("Executing finally block.");
			}

		}
	}
}
