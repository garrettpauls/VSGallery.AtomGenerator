using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace VSGallery.AtomGenerator
{
    public sealed class Logger : IDisposable
    {
        private readonly StreamWriter mFile;

        public Logger(string file)
        {
            mFile = new StreamWriter(
                File.Open(file, FileMode.Create, FileAccess.Write, FileShare.Read),
                Encoding.UTF8);
        }

        public void Dispose()
        {
            mFile.Dispose();
        }

        public void Error(string message)
        {
            Error(message, null);
        }

        public void Error(string message, Exception ex)
        {
            ErrorAsync(message, ex).Wait();
        }

        public Task ErrorAsync(string message)
        {
            return ErrorAsync(message, null);
        }

        public async Task ErrorAsync(string message, Exception ex)
        {
            await mFile.WriteLineAsync($"ERROR: {message}");
            if (ex != null)
            {
                await mFile.WriteLineAsync($@"
--------------------------------------------------
{_ToString(ex)}
--------------------------------------------------");
            }
        }

        public void Info(string message)
        {
            InfoAsync(message).Wait();
        }

        public Task InfoAsync(string message)
        {
            return mFile.WriteLineAsync($"INFO: {message}");
        }

        private static string _ToString(Exception exception)
        {
            return exception.ToString();
        }
    }
}
