using System;
using System.IO;

namespace VSGallery.AtomGenerator
{
    public sealed class Logger
    {
        private readonly string mFile;

        private Logger(string file)
        {
            mFile = file;
        }

        public static Logger Create(string file)
        {
            var logDir = Directory.GetParent(file);
            if (!logDir.Exists)
            {
                logDir.Create();
            }

            return new Logger(file);
        }

        public void Error(string message)
        {
            Error(message, null);
        }

        public void Error(string message, Exception ex)
        {
            if (ex != null)
            {
                message += $@"
--------------------------------------------------
{_ToString(ex)}
--------------------------------------------------";
            }

            _WriteLine(true, message);
        }

        public void Info(string message)
        {
            _WriteLine(false, message);
        }

        private static string _ToString(Exception exception)
        {
            return exception.ToString();
        }

        private void _WriteLine(bool isError, string value)
        {
            var color = Console.ForegroundColor;
            if (isError)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            Console.WriteLine(value);
            Console.ForegroundColor = color;

            var header = isError ? "ERROR" : "INFO";
            File.AppendAllText(mFile, $"{header}: {value}\r\n");
        }
    }
}
