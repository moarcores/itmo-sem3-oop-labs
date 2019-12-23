using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Services;
using System.Text.RegularExpressions;

namespace Lab3_INIReader
{
    public class IniReader
    {
        private Dictionary<string, Dictionary<string, string>> parsedFile = null;
        StreamReader reader = null;
        
        public IniReader(string filepath)
        {
            try
            {
                reader = new StreamReader(filepath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void ParseFile()
        {
            if (reader == null)
            {
                throw new FileNotFoundException();
            }
            parsedFile = new Dictionary<string, Dictionary<string, string>>();


            string currentSection = null;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains(";")) // cutting down comments
                {
                    line = line.Substring(0, line.IndexOf(';'));
                }

                if (line.Length == 0) // cutting down empty lines
                {
                    continue;
                }

                if (line.EndsWith("]") && line.StartsWith("[")) //current line is expected to be a section name
                {
                    string sub = line.Substring(1, line.Length - 2);
                    string mask  = "[^0-9a-zA-Z_]";
                    if (Regex.IsMatch(sub, mask, RegexOptions.Singleline) || sub.Length == 0 || currentSection == line)
                    {
                        throw new InvalidDataException();
                    }
                    currentSection = sub;
                    parsedFile.Add(currentSection, new Dictionary<string, string>());
                    continue;
                }
                
                //if current line is not empty/comment/section name it is supposed to be a "name = value" pair
                string[] strings = line.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
                if (strings.Length != 3 
                    || strings[1] != "=" 
                    || Regex.IsMatch(strings[0], "[^0-9a-zA-Z_]", RegexOptions.Singleline) 
                    || Regex.IsMatch(strings[2], "[^0-9a-zA-Z_./]", RegexOptions.Singleline)
                    || parsedFile[currentSection].ContainsKey(strings[0])
                    )
                {
                    throw new InvalidDataException();
                }
                
                parsedFile[currentSection].Add(strings[0], strings[2]);
            }
        }

        public int GetInt(string name, string section)
        {
            if (!(parsedFile.ContainsKey(section) && parsedFile[section].ContainsKey(name)))
            {
                throw new ArgumentException();
            }
            //int.TryParse()
            return Convert.ToInt32(parsedFile[section][name]);
        }

        public double GetDouble(string name, string section)
        {
            if (!(parsedFile.ContainsKey(section) && parsedFile[section].ContainsKey(name)))
            {
                throw new ArgumentException();
            }

            return Convert.ToDouble(parsedFile[section][name]);
        }

        public string GetString(string name, string section)
        {
            if (!(parsedFile.ContainsKey(section) && parsedFile[section].ContainsKey(name)))
            {
                throw new ArgumentException();
            }
            return parsedFile[section][name];
        }
    }
}