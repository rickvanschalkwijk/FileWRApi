using System;
using System.IO;
using System.Threading.Tasks;
using NLog;

namespace FileWR.Business
{
    public class FileReader : IFileReader
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public async Task<string> ReadAsync(string path)
        {
            var fileText = string.Empty;
            using (StreamReader stream = File.OpenText(path))
            {
                try
                {
                    fileText = await stream.ReadToEndAsync();
                    _logger.Info($"Number of chars read from file {fileText.Length}");
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }

            return fileText;
        }
    }

    public interface IFileReader
    {
        Task<string> ReadAsync(string path);
    }
}
