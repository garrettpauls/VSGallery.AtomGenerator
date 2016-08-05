using System;
using System.IO;

namespace VSGallery.AtomGenerator
{
    public sealed class Logger
    {
        private readonly string mFile;

        private bool mHasLoggedFileAccessError;

        private Logger(string file)
        {
            mFile = file;
        }

        private void _WriteLine(bool isError, string value)
        {
            _WriteToConsole(isError, value);

            try
            {
                var header = isError ? "ERROR" : "INFO";
                File.AppendAllText(mFile, $"{header}: {value}\r\n");
            }
            catch(Exception ex)
            {
                if(!mHasLoggedFileAccessError)
                {
                    mHasLoggedFileAccessError = true;

                    _WriteToConsole(true, $"Could not write log to file (future write failures are not logged): {mFile}");
                    _WriteToConsole(true, ex.ToString());
                }
            }
        }

        public void Error(string message)
        {
            Error(message, null);
        }

        public void Error(string message, Exception ex)
        {
            if(ex != null)
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

        private static void _WriteToConsole(bool isError, string value)
        {
            var color = Console.ForegroundColor;
            if(isError)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            Console.WriteLine(value);
            Console.ForegroundColor = color;
        }

        public static Logger Create(string file)
        {
            var logDir = Directory.GetParent(file);
            if(!logDir.Exists)
            {
                try
                {
                    logDir.Create();
                }
                catch(Exception ex)
                {
                    _WriteToConsole(true, $"Could not create directory for logs: {logDir.FullName}");
                    _WriteToConsole(true, ex.Message);
                }
            }

            return new Logger(file);
        }
    }
}