//Read a Text File
using System;
using System.IO;
namespace readwriteapp
{
    class Class1
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Directory.CreateDirectory(@"C:\visual test");
                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter("C:\\visual test\\Test.txt");  //da accesso negato, si risolve aprendo visual studio come admin
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
            Console.ReadKey();


            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader("C:\\visual test\\Test.txt");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the lie to console window
                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
            Console.ReadKey();
        }
    }
}