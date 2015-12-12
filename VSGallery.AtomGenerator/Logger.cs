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
            _WriteLine($"ERROR: {message}");
            if (ex != null)
            {
                _WriteLine($@"
--------------------------------------------------
{_ToString(ex)}
--------------------------------------------------");
            }
        }

        public void Info(string message)
        {
            _WriteLine($"INFO: {message}");
        }

        private static string _ToString(Exception exception)
        {
            return exception.ToString();
        }

        private void _WriteLine(string value)
        {
            File.AppendAllText(mFile, value + "\r\n");
        }
    }
}
