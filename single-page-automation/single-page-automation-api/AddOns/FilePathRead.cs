using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace single_page_automation_api.AddOns
{
    public class FilePathRead
    {
        public static string ReadFile()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory() + "/AddOns/filepath.txt");
            string[] lines = File.ReadAllLines(filePath);

            return lines[0];
        }
    }
}
