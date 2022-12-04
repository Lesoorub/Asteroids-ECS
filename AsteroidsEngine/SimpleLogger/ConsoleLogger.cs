using System;
using System.Collections.Generic;
namespace SimpleLogger
{
    public class ConsoleLogger : ILogCatcher
    {
        public List<string> log = new List<string>();
        public void OnLogCatched(string text)
        {
            Console.WriteLine(text);
        }
    }
}
