using System;
using System.IO;

namespace Lab1_RationalFraction
{
    public class FractionsScanner : IDisposable
    {
        private bool disposed = false;
        private StreamReader file;

        public FractionsScanner(string path)
        {
            file = new StreamReader(path);
        }

        public IFractionCollection GetNextCollection()
        {
            if (!file.EndOfStream)
            {
                string text = file.ReadLine();
                string[] str = text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                CachedFractionList result = new CachedFractionList();
                foreach (var s in str)
                {
                    string[] vals = s.Split(new[] {'/'}, StringSplitOptions.None);
                    if (vals.Length != 2)
                    {
                        throw new InvalidDataException();
                    }
                    result.Add(new RationalFraction(long.Parse(vals[0]), ulong.Parse(vals[1])));
                }

                return result;
            } 
            
            return null;
        }

        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                if (file != null)
                {
                    file.Dispose();
                }
            }
            disposed = true;
        }
    }
}