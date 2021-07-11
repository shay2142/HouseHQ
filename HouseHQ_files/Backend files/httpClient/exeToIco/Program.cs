using System;
using System.Drawing;
using System.IO;

namespace exeToIco
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string executablePath = @"C:\Users\shay5\AppData\Local\Discord\app-1.0.9002\Discord.exe";

            // 2. Store the icon instance
            Icon theIcon = ExtractIconFromFilePath(executablePath);

            // 3. If the icon was extracted, proceed to save it
            if (theIcon != null)
            {
                // 4. Save the icon to my desktop 
                using (FileStream stream = new FileStream(@"C:\Users\shay5\Desktop\puttygen.ico", FileMode.CreateNew))
                {
                    theIcon.Save(stream);
                }
            }
        }
        public static Icon ExtractIconFromFilePath(string executablePath)
        {
            Icon result = (Icon)null;

            try
            {
                result = Icon.ExtractAssociatedIcon(executablePath);
            }
            catch (Exception)
            {
                Console.WriteLine("Unable to extract the icon from the binary");
            }

            return result;
        }
    }
}