using System;
using System.IO;
namespace SimpleLogger
{
    public class TextFileLogger : ILogCatcher, IDisposable
    {
        StreamWriter stream;
        public TextFileLogger(string path)
        {
            var fi = new FileInfo(path);
            if (!fi.Exists)
                stream = fi.CreateText();
            else
                stream = new StreamWriter(fi.OpenWrite());
        }

        public void OnLogCatched(string text)
        {
            stream.WriteLine(text);
        }

        public void Dispose()
        {
            stream?.Dispose();
        }
    }
}
