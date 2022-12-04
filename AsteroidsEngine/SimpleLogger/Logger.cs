using System;
using System.Collections.Generic;

namespace SimpleLogger
{
    public class Logger
    {
        public string format = "[{hour}:{minute}:{second}:{milliseconds}][{type}] {message}";
        public List<ILogCatcher> catchers = new List<ILogCatcher>();

        public delegate void LogArgs(string message);
        public event LogArgs OnLog;

        StringFormatter formatter = new StringFormatter();


        public void WriteLine(object obj, LogType type = LogType.Info)
        {
            var now = DateTime.Now;
            string text = formatter.Format(format,
                ("hour", now.Hour),
                ("minute", now.Minute),
                ("second", now.Second),
                ("milliseconds", now.Millisecond),
                ("type", type),
                ("message", obj)
                );

            OnLog?.Invoke(text);
            foreach (var catcher in catchers)
                catcher.OnLogCatched(text);
        }
    }
}
