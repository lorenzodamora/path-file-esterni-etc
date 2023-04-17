using System;
using System.Dynamic;
using System.IO;
using System.Text;

namespace file_accesso_diretto
{
	internal class Program
	{
		static void Main()
		{
			string a = Console.ReadLine();
			switch (a)
			{
				case "1":
					F1();
					break;
				case "2":
					F2();
					break;
				case "3":
					F3();
					break;
				default:
				case "0":
					F1();
					Console.WriteLine("\n\n\n ######### \n\n\n");
					F2();
					Console.WriteLine("\n\n\n ######### \n\n\n");
					F3();
					break;
			}
		}
		static string GetPath()
		{
			string path = Path.GetFullPath(".");
			path = Path.GetDirectoryName(path);
			path = Path.GetDirectoryName(path);
			path += @"\vari test\testo";
			// Delete a directory and all subdirectories with Directory static method...
			if (System.IO.Directory.Exists(path))
				try
				{
					System.IO.Directory.Delete(path, true); //di base è false
				}
				catch (System.IO.IOException e)
				{
					Console.WriteLine(e.Message);
				}
			Directory.CreateDirectory(path);
			return path;
		}

		static void F1()
		{
			string pat = Path.GetTempFileName();
			string path = GetPath() + "\\" + Path.GetFileName(pat);
			File.Move(pat, path);
			Console.WriteLine(path);
			using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
			{
				Byte[] info = new UTF8Encoding(true).GetBytes("abcdefghijklmnpqrstuvwxyz .,:-_èò+àéç*°§ù$£€|0123456789");
				fs.Write(info, 0, info.Length);

				byte[] b = new byte[info.Length];
				UTF8Encoding temp = new UTF8Encoding(true);
				fs.Seek(0, SeekOrigin.Begin);
				//questo porta a inizio file //in seek offset è in base a tutto il file // seekorigin può essere solo inizio current e fine,  seek somma la posizione di seekorigin a offset.
				//while (fs.Read(b, 0, b.Length-10)>0) //in read offset è dell'array
				//Console.WriteLine(temp.GetString(b));

				while (fs.Read(b, 0, b.Length)>0) //in read offset è in base a tutto il file
					Console.WriteLine(temp.GetString(b));
				Console.WriteLine();

				while (fs.Read(b, 0, b.Length)>0)
					Console.WriteLine(temp.GetString(b).Remove(20));

			}
		}
		static void F2()
		{
			string pat2 = Path.GetTempFileName();
			string path2 = GetPath() + "\\" + Path.GetFileName(pat2);
			File.Move(pat2, path2);
			Console.WriteLine(path2);
			using (FileStream fs2 = File.Open(path2, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
			{
				Byte[] info = new UTF8Encoding(true).GetBytes("0\n1\n2\n3\n4\n5\n6\n7\n8\n9");
				for (int i = 0; i < info.Length; i++)
					fs2.Write(info, i, 1);

				byte[] b = new byte[info.Length];
				UTF8Encoding temp = new UTF8Encoding(true);

				fs2.Seek(0, SeekOrigin.Begin);
				while (fs2.Read(b, 0, b.Length)>0) //in read offset è in base a tutto il file? o ai byte?
					Console.WriteLine(temp.GetString(b));
				Console.WriteLine();

				fs2.Seek(4, SeekOrigin.Begin); //leggi il byte 4 ( il 5o )
				Console.WriteLine((char)fs2.ReadByte()); //legge il 2

				fs2.Seek(-1, SeekOrigin.Current);
				byte h = (byte)fs2.ReadByte();
				fs2.WriteByte(h); // esce 22 ma cancella \n, quindi esce 223
				Console.WriteLine("\n\\" + (char)(int)h);
				fs2.Seek(-1, SeekOrigin.Current);
				Console.WriteLine("\\" + (char)fs2.ReadByte());

				Console.WriteLine("\n\n#\n");
				fs2.Seek(0, SeekOrigin.Begin);
				while (fs2.Read(b, 0, b.Length)>0) //in read offset è in base a tutto il file? o ai byte?
					Console.WriteLine(temp.GetString(b));
				Console.WriteLine();
			}
		}
		static void F3()
		{
			string pat3 = Path.GetTempFileName();
			string path3 = GetPath() + "\\" + Path.GetFileName(pat3);
			File.Move(pat3, path3);
			Console.WriteLine(path3);
			using (FileStream fs3 = File.Open(path3, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
			{
				string str = "test\nacaso\n0123456789\n\nciaogente";
				Byte[] info = new UTF8Encoding(true).GetBytes(str);
				fs3.Write(info, 0, info.Length);

				byte[] b = new byte[info.Length];
				UTF8Encoding temp = new UTF8Encoding(true);

				fs3.Seek(0, SeekOrigin.Begin);
				while (fs3.Read(b, 0, b.Length)>0) //in read offset è in base a tutto il file? o ai byte?
					Console.WriteLine(temp.GetString(b));
				Console.WriteLine("\\\\\n");

				fs3.Seek(0, SeekOrigin.Begin);
				//legge e stampa una riga, il writeline finale sostituisce \n non stampato
				for (int ch = fs3.ReadByte(); ch!=-1; ch = fs3.ReadByte()) //fs3.Position < fs3.Length  ==   ch!=-1
				{
					Console.Write((char)ch);
					if ((char)ch == '\n') break;
				}

				fs3.Seek(0, SeekOrigin.Begin);
				//legge solo la riga scelta (3a)
				int line = 0;
				for (int ch = fs3.ReadByte(); ch!=-1 && line != 3; ch = fs3.ReadByte())
				{
					if (line == 2) Console.Write((char)ch);
					if ((char)ch == '\n') line++;
				} //va a capo una volta finita la riga
				  //Console.WriteLine("");

				//leggi una parte della riga scelta (3a) "0123456789\n", (3456)
				line=0;
				int chc = 0; //char count
				fs3.Seek(0, SeekOrigin.Begin);
				for (int ch = fs3.ReadByte(); ch!=-1 && line != 3; ch = fs3.ReadByte())
				{
					if (line == 2)
					{
						if (chc >= 3 && chc <=6) Console.Write((char)ch);
						chc++;
					}
					if ((char)ch == '\n') line++;
				}
				//oppure porto tutta la riga a stringa e leggo la parte che voglio (utile per lavorarci)
				Console.WriteLine();

			}
		}

	}
}




