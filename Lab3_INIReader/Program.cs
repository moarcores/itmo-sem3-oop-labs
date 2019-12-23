using System;
using System.Text;

namespace Lab3_INIReader
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IniReader reader = new IniReader("/Users/sergei/RiderProjects/sem3_OOPLabs/Lab3_INIReader/test.ini");
            reader.ParseFile();
            Console.WriteLine(reader.GetInt("LogNCMD", "COMMON"));
            Console.WriteLine(reader.GetDouble("LogNCMD", "COMMON"));
            Console.WriteLine(reader.GetString("LogNCMD", "COMMON"));
            
            Console.WriteLine(reader.GetDouble("BufferLenSeconds", "ADC_DEV"));
//            Console.WriteLine(reader.GetInt("BufferLenSeconds", "ADC_DEV"));
            Console.WriteLine(reader.GetString("BufferLenSeconds", "ADC_DEV"));
            
            Console.WriteLine(reader.GetString("DiskCachePath", "COMMON"));
//            Console.WriteLine(reader.GetInt("DiskCachePath", "COMMON"));
//            Console.WriteLine(reader.GetDouble("DiskCachePath", "COMMON"));
        }
    }
}