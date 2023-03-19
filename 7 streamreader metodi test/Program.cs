using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace _7_streamreader_metodi_test
{
    public class Program
    {
        static void Main()
        {
            string path = Path.GetFullPath(".");
            for (int i = 3; i > 0; i--) //il 3 dipende dalla gestione della cartella della soluzione
            { //in breve: torna indietro di una cartella
                path = Path.GetDirectoryName(path);
            }
            path += "\\txt\\Sample.txt";

            CreaFile(path);

            StreamReader srl = new StreamReader(path);
            string line = "";
            Scrivi(ref srl, ref line);
            Console.WriteLine("---------\n");
            srl.BaseStream.Position = 1;
            Scrivi(ref srl, ref line);
            Console.WriteLine("  ----\n srl.BaseStream.Position = 1;\nriposiziona nel file. il numero corrisponde ai char \n\n---------\n");
            srl.BaseStream.Position = 0; //reset
            line = srl.Peek().ToString();
            Console.WriteLine(line);
            Scrivi(ref srl, ref line);
            Console.WriteLine("  ----\n srl.Peek()\nnon riesco ad usarlo\n\n---------\n");
            srl.BaseStream.Position = 0; //reset
            Console.WriteLine(srl.Read());
            Scrivi(ref srl, ref line);
            Console.WriteLine("  ----\n srl.Read()\nlegge l'ascii decimale\n\n---------\n");
            srl.BaseStream.Position = 0; //reset
            char[] chars = new char[10];
            srl.ReadBlock(chars, 0, 10);
            foreach (int c in chars)
                Console.Write(c);
            //Scrivi(ref srl, ref line);
            Console.WriteLine("\n  ----\nsrl.ReadBlock();\nè come read\n\n---------\n");
            Console.WriteLine("l'unico metodo fattibile per leggere è readline e poi nel caso rimuovere\n\n\n\n\n");

            srl.Close();
        }

        static void CreaFile(string path)
        {
            StreamWriter swf = new StreamWriter(path); //swf stream writer file
            swf.WriteLine("Hello World!!");
            swf.WriteLine("robe random!\n\nspazio vuoto\ncose");
            swf.Close();
        }
        static void Scrivi(ref StreamReader srl, ref string line)
        {
            line = srl.ReadLine();
            while (line != null)
            {
                Console.WriteLine(line);
                line = srl.ReadLine();
            }
        }
    }
}
