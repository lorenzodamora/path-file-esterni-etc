using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_
{
	public class SimpleFile_3
	{
		static void Main()
		{
			Console.WriteLine("se si stoppa a metà il programma si bugga tutto con il prossimo restart");
			string path = Path.GetFullPath(".");
			for (int i = 3; i > 0; i--) //il 3 dipende dalla gestione della cartella della soluzione
			{ //in breve: torna indietro di una cartella
				path = Path.GetDirectoryName(path);
			}
			path += @"\txt";

            //Directory.CreateDirectory(path + @"\test"); //già nel codice

            SimpleFileCopy(path);
			SimpleFileMove(path);
			SimpleFileDelete(path);
		}

		static void SimpleFileCopy(string path)
		{
			string fileName = "Test.txt";
			string sourcePath = path;
			string targetPath = path + @"\test";

			// Use Path class to manipulate file and directory paths.
			string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
			string destFile = System.IO.Path.Combine(targetPath, fileName);

			// To copy a folder's contents to a new location:
			// Create a new target folder.
			// If the directory already exists, this method does not create a new directory.
			System.IO.Directory.CreateDirectory(targetPath);

			// To copy a file to another location and
			// overwrite the destination file if it already exists.
			System.IO.File.Copy(sourceFile, destFile, true);

			// To copy all the files in one directory to another directory.
			// Get the files in the source folder. (To recursively iterate through
			// all subfolders under the current directory, see
			// "How to: Iterate Through a Directory Tree.")
			// Note: Check for target path was performed previously
			//       in this code example.
			if (System.IO.Directory.Exists(sourcePath))
			{
				string[] files = System.IO.Directory.GetFiles(sourcePath);

				// Copy the files and overwrite destination files if they already exist.
				foreach (string s in files)
				{
					// Use static Path methods to extract only the file name from the path.
					fileName = System.IO.Path.GetFileName(s);
					destFile = System.IO.Path.Combine(targetPath, fileName);
					System.IO.File.Copy(s, destFile, true);
				}
			}
			else
			{
				Console.WriteLine("Source path does not exist!");
			}

			// Keep console window open in debug mode.
			Console.WriteLine("Press any key to continue.");
			Console.ReadKey();
		}
		static void SimpleFileMove(string path)
		{
			string sourceFile = path + @"\Test.txt";
			string destinationFile = path + @"\test\Testcopy.txt";

			// To move a file or folder to a new location:
			try
			{
				System.IO.File.Move(sourceFile, destinationFile); //si può anche rinominare
			}
			catch(System.IO.IOException e)
			{
                Console.WriteLine(LineNumber() + e.Message);
                return;
            }
            // To move an entire directory. To programmatically modify or combine
            // path strings, use the System.IO.Path class.
            //System.IO.Directory.Move(@"C:\Users\Public\public\test\", @"C:\Users\Public\private");

            //lo rimetto dove prima per la prossima run
			/* */
            destinationFile = path + @"\Test.txt";
            sourceFile = path + @"\test\Testcopy.txt";

            // To move a file or folder to a new location:
            try
            {
                System.IO.File.Move(sourceFile, destinationFile); //si può anche rinominare
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(LineNumber() + e.Message);
                return;
            }
			/* */
        }
		static void SimpleFileDelete(string path)
		{
			path = CreaFileDaCancellare(path); //path = path + \delete

			// Delete a file by using File class static method...
			if (System.IO.File.Exists(path + @"\Dtest.txt"))
			{
				// Use a try block to catch IOExceptions, to
				// handle the case of the file already being
				// opened by another process.
				try
				{
					System.IO.File.Delete(path + @"\Dtest.txt");
				}
				catch (System.IO.IOException e)
				{
					Console.WriteLine(LineNumber() + e.Message);
					return;
				}
			}

			// ...or by using FileInfo instance method.
			System.IO.FileInfo fi = new System.IO.FileInfo(path + @"\Dtest1.txt");
			try
			{
				fi.Delete();
			}
			catch (System.IO.IOException e)
			{
				Console.WriteLine(LineNumber() + e.Message);
			}

			Console.ReadKey();

			// Delete a directory. Must be writable or empty.
			try
			{
				System.IO.Directory.Delete(path); 
			}
			catch (System.IO.IOException e)
			{
				Console.WriteLine(LineNumber() + e.Message + "Cancello tutto comunque con il prossimo blocco di codice.");
			}
			// Delete a directory and all subdirectories with Directory static method...
			if (System.IO.Directory.Exists(path))
			{
				try
				{
					System.IO.Directory.Delete(path, true); //di base è false
				}

				catch (System.IO.IOException e)
				{
					Console.WriteLine(LineNumber() + e.Message);
				}
			}

			/*
			 * 
			// ...or with DirectoryInfo instance method.
			System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(@"C:\Users\Public\public");
			// Delete this dir and all subdirs.
			try
			{
				di.Delete(true);
			}
			catch (System.IO.IOException e)
			{
				Console.WriteLine(e.Message);
			}
			*
			*/
		}

		static string CreaFileDaCancellare(string path)
		{
			Int64 x;
			try
			{
				Directory.CreateDirectory(path + @"\delete\delete2");

				//Open the File
				StreamWriter sw = new StreamWriter(path + @"\delete\Dtest.txt", true, Encoding.ASCII);
				StreamWriter sw1 = new StreamWriter(path + @"\delete\Dtest1.txt", true, Encoding.ASCII);
				//Write out the numbers 1 to 10 on the same line.
				for (x = 0; x < 10; x++)
				{
					sw.Write(x);
					sw1.Write(x);
				}

				//close the file
				sw.Close();
				sw1.Close(); //se non lo chiudi non lo elimina dopo
			}
			catch (Exception e)
			{
				Console.WriteLine(LineNumber() + e.Message);
			}
			finally
			{
				Console.WriteLine("Creati cartella e file da eliminare");
			}

			Console.ReadKey();

			return path + @"\delete";
		}
        static int LineNumber([System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
        {
            return lineNumber;
        }
    }
}