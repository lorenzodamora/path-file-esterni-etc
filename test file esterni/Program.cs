using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.NetworkInformation;
//using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Drawing;

using System.IO;
using System.Text; //contiene la classe Encoding
using System.Reflection;//lo ha aggiunto il codice??

//

namespace test_file_esterni
{
	internal class Program
	{
		static string path;

		static void Main()
		{
            /* obsoleto
				path = Path.GetFullPath(".");
				for (int i = 3; i > 0; i--) //il 3 dipende dalla gestione della cartella della soluzione
				{ //in breve: torna indietro di una cartella
					path = Path.GetDirectoryName(path);
				} */
            path = Directory.GetCurrentDirectory();
            path = path.Remove(path.Length - 10); //altrimenti mettere il path è  ...\bin\debug
            path += "\\txt"; //aggiungi il folder
			Directory.CreateDirectory(path);//crea il folder se non esiste
            Console.WriteLine(path);

            FStreamWriter1();
			FStreamWriter2();
			FStreamReader1(@"\Test.txt");

		}

		static void FStreamReader1(string filename)
		{
			string line;
			try
			{
				path += filename;
				//Pass the file path and file name to the StreamReader constructor
				StreamReader sr = new StreamReader(path); 
				//Read the first line of text
				line = sr.ReadLine();
				//Continue to read until you reach end of file
				while (line != null) //null prende solo la fine del file, segna anche righe saltate
				{
					//write the line to console window
					Console.WriteLine(line);
					//Read the next line
					line = sr.ReadLine();
				}
				//close the file
				sr.Close();
				Console.ReadKey();
			}
			catch (Exception e)
			{
				Console.WriteLine("Exception: " + e.Message);
			}
			finally
			{
				Console.WriteLine("Executing finally block.");
			}

			path = Path.GetDirectoryName(path); //togli "\Test.txt"

		}
		static void FStreamWriter1()
		{
			try
			{
				//Pass the filepath and filename to the StreamWriter Constructor
				StreamWriter sw = new StreamWriter(path + "\\Test.txt", false); //append è di base false
				//Write a line of text
				sw.WriteLine("Hello World!!"); //se sw è false sovrascrive la prima riga, altrimenti aggiunge alla fine del file
				//Write a second line of text
				sw.WriteLine("From the StreamWriter class"); //se sw è false sovrascrive la seconda riga
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

		static void FStreamWriter2()
		{
			Int64 x;
			try
			{
				//Open the File
				StreamWriter sw = new StreamWriter(path + "\\Test1.txt", true, Encoding.ASCII);

				//Write out the numbers 1 to 10 on the same line.
				for (x = 0; x < 10; x++)
				{
					sw.Write(x);
				}

				//close the file
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