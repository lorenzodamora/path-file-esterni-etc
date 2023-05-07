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
				case "4":
					F4ContaLeLineeNelFile();
					break;
				case "5":
					F5FileReadAllLines();
					break;
				case "6":
					F6Test();
					break;
				case "7":
					
					break;
				default:
				case "0":
					F1();
					Console.WriteLine("\n\n\n ######### \n\n\n");
					F2();
					Console.WriteLine("\n\n\n ######### \n\n\n");
					F3();
					Console.WriteLine("\n\n\n ######### \n\n\n");
					F4ContaLeLineeNelFile();
					Console.WriteLine("\n\n\n ######### \n\n\n");
					F5FileReadAllLines();
					Console.WriteLine("\n\n\n ######### \n\n\n");
					F6Test();
					Console.WriteLine("\n\n\n ######### \n\n\n");
					
					break;
			}
		}
		static string GetPath()
		{
			string path = Path.GetFullPath("..\\..\\vari test\\testo");
			//path = Path.GetDirectoryName(path);
			//path = Path.GetDirectoryName(path);
			//path += @"\vari test\testo";
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
		static string InitPath()
		{
			string pat = Path.GetTempFileName();
			string path = GetPath() + "\\" + Path.GetFileName(pat);
			File.Move(pat, path);
			return path;
		}
		static void F1()
		{
			string path = InitPath();
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
			string path2 = InitPath();
			Console.WriteLine(path2);
			using (FileStream fs2 = new FileStream(path2, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
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
			string path3 = InitPath();
			Console.WriteLine(path3);
			FileStream fs3 = new FileStream(path3, FileMode.Open, FileAccess.ReadWrite, FileShare.None);

			//se il file è vuoto scrivere semplicemente la stringa e convertirla in byte
			string str = "test\nacaso\n0123456789\n\nciaogente1";
			Byte[] info = new UTF8Encoding(true).GetBytes(str);
			fs3.Write(info, 0, info.Length);

			//per leggere il file già pieno
			byte[] b = new byte[info.Length];
			UTF8Encoding temp = new UTF8Encoding(true);

			fs3.Seek(0, SeekOrigin.Begin); // torna all'inizio col cursore
			fs3.Read(b, 0, b.Length); //sembra legga tutto in un iterazione
			/*fs3.Read(b, 2, b.Length);*/ //va oltre i limiti della matrice
			/*fs3.Read(b, 2, b.Length-2);*/ //stampa 2 spazi, poi tutto il file,ma salta le ultime due lettere
			Console.WriteLine(temp.GetString(b));
			Console.WriteLine("\\\\\n");

			fs3.Seek(0, SeekOrigin.Begin);
			//legge e stampa byte per byte una riga, la prima,, il writeline finale sostituisce \n non stampato
			for (int ch = fs3.ReadByte(); ch!=-1; ch = fs3.ReadByte()) //   fs3.Position < fs3.Length   =è uguale a=   ch!=-1
			{
				Console.Write((char)ch);
				if ((char)ch == '\n') break;
			}

			fs3.Seek(0, SeekOrigin.Begin);
			//legge solo la riga scelta (3a) //la terza linea è la indice 2
			for (int ch = fs3.ReadByte(), line = 0; ch!=-1 && line != 3; ch = fs3.ReadByte()) //
			{
				if (line == 2) Console.Write((char)ch);
				if ((char)ch == '\n') line++;
			} //va a capo una volta finita la riga
			  //Console.WriteLine("");

			//leggi una parte della riga scelta (3a) "0123456789\n", (3456) //avendo l'indice caratteri
			//int chc = 0; //char count
			fs3.Seek(0, SeekOrigin.Begin);
			for (int ch = fs3.ReadByte(), line = 0, chc = 0; ch!=-1 && line != 3; ch = fs3.ReadByte())
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


			//fs3.Read(b, 10, b.Length); //sembra legga tutto in un iterazione
			//triggero un'errore per vedere se cancella tutto senza il fs.close =>
			//il contenuto del file è intatto

			//importante
			fs3.Close();
		}
		static void F4ContaLeLineeNelFile()
		{
			string path4 = InitPath();
			Console.WriteLine(path4);
			//scrive nel file
			string str = "a\nb\n\n\nend\n\n\n"; //ci sono 7 '\n'; il count li segna tutti
			Byte[] info = new UTF8Encoding(true).GetBytes(str);
			FileStream fsw = new FileStream(path4, FileMode.Open, FileAccess.Write, FileShare.None);
			fsw.Write(info, 0, info.Length);
			fsw.Close();

			//inizia la lettura
			int count = 0;
			FileStream fsr = new FileStream(path4, FileMode.Open, FileAccess.Read, FileShare.None);
			for (int ch = fsr.ReadByte(); ch!=-1; ch = fsr.ReadByte())
				if ((char)ch == '\n') count++;
			fsr.Close();
			Console.WriteLine(count);
		}
		static void F5FileReadAllLines()
		{
			string path = InitPath();
			Console.WriteLine(path);
			//scrive nel file
			string str = "a\nb\nc\nd\nend\n";
			Byte[] info = new UTF8Encoding(true).GetBytes(str);
			FileStream fsw = new FileStream(path, FileMode.Open, FileAccess.Write, FileShare.None);
			fsw.Write(info, 0, info.Length);
			fsw.Close();


			//inizia la lettura

			byte[] b = new byte[1024];
			UTF8Encoding temp = new UTF8Encoding(true);
			string line = "";
			FileStream fsr = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
			while (fsr.Read(b, 0, b.Length)>0)
				line += temp.GetString(b);
			fsr.Close();

			string[] lines = line.TrimEnd('\0').Split('\n');
			Console.WriteLine(string.Join(" | ", lines));
		}
		static void F6Test()
		{//filestream apre in sovrascrittura, ma ciò che non sovrascrive rimane
			string path = InitPath();
			Console.WriteLine(path);

			//scrive nel file
			string str = "a\nb\nc\nd\nend\n";
			Byte[] info = new UTF8Encoding(true).GetBytes(str);
			FileStream fsw = new FileStream(path, FileMode.Open, FileAccess.Write, FileShare.None);
			fsw.Write(info, 0, info.Length);
			fsw.Close();

			//di nuovo
			str = "start\n";
			info = new UTF8Encoding(true).GetBytes(str);
			fsw = new FileStream(path, FileMode.Open, FileAccess.Write, FileShare.None);
			fsw.Write(info, 0, info.Length);
			fsw.Close();

			//inizia la lettura

			byte[] b = new byte[1024];
			UTF8Encoding temp = new UTF8Encoding(true);
			string line = "";
			FileStream fsr = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
			while (fsr.Read(b, 0, b.Length)>0)
				line += temp.GetString(b);
			fsr.Close();

			Console.WriteLine(line.TrimEnd('\0'));

			//per evitare ciò che si fa?

			str = "a\n";
			info = new UTF8Encoding(true).GetBytes(str);
			fsw = new FileStream(path, FileMode.Truncate, FileAccess.Write, FileShare.None); //file mode truncate azzera il file
			fsw.Write(info, 0, info.Length);
			fsw.Close();


			b = new byte[1024];
			temp = new UTF8Encoding(true);
			line = "";
			fsr = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
			while (fsr.Read(b, 0, b.Length)>0)
				line += temp.GetString(b);
			fsr.Close();
			line = line.TrimEnd('\0');

			Console.WriteLine("\n++++++++++\n");
			Console.WriteLine(line);
		}
	}
}