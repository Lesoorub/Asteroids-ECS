using System.Collections.Generic;
namespace SimpleLogger
{
    public class TextMemoryLogger : ILogCatcher
    {
        public List<string> log = new List<string>();
        public void OnLogCatched(string text)
        {
            log.Add(text);
        }
    }
}
