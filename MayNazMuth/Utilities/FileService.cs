using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MayNazMuth.Utilities
{
    class FileService
    {
        static StreamReader sr;

        public static string readFile(string fileName)
        {
            string fileContents = "";

            try
            {
                //Try and read the file...
                sr = new StreamReader(fileName);
                fileContents = sr.ReadToEnd();


            }
            catch (IOException ioe)
            {
                MessageBox.Show("There was a problem opening the file: " + fileName + "." + ioe.Message);
                Console.WriteLine(ioe.Message);

            }

            return fileContents;
        }
    }
}
