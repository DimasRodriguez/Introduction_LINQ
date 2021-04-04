using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Introduction
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Windows";
            ShowLargeFilesWithoutLinq(path);
            Console.WriteLine("******");
            ShowLargeFilesWithLinq(path);


            //Console.WriteLine("Hello World!");
        }

        private static void ShowLargeFilesWithLinq(string path)
        {

            // Una forma
            /*var query = from file in new DirectoryInfo(path).GetFiles()
                        orderby file.Length descending
                        select file;*/
            //Otra forma
            var query = new DirectoryInfo(path).GetFiles().OrderByDescending(f => f.Length).Take(5);
                        

            foreach (var file in query)
            {
                Console.WriteLine($"{file.Name,-30} : {file.Length,10:N0}");
            }
        }

        private static void ShowLargeFilesWithoutLinq(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();
            // A Array.Sort puedo pasarle como segundo argumento un método que diga cómo ordenarlo

            Array.Sort(files, new FileInfoComparer());

            //si quiero mostrar todos
            /*foreach (FileInfo file in files)
            {
                Console.WriteLine($"{file.Name} : {file.Length}");
            }*/

            //si quiero mostrar solo los primeros 5
            for (int i = 0; i< 5 ; i++)
            {
                FileInfo file = files[i];
                Console.WriteLine($"{file.Name, -30} : {file.Length, 10:N0}");
            }

        }
    }

    public class FileInfoComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            /*La idea es retornar:
                -1 si el x < y
                0 si son iguales
                1 sin x > y
            */
            return y.Length.CompareTo(x.Length);
        }
    }
}
