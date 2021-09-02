using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Single_Page_Automatic_Assignment.AddOns
{
    public class FilePathRead
    {
        public static string ReadFile()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/filepath.txt");
            string[] lines = File.ReadAllLines(filePath);

            return lines[0];
        }
    }
}
