using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FileWR.Business
{
    public class FileReader : IFileReader
    {
        private readonly ILogger<FileReader> _logger;

        public FileReader(ILogger<FileReader> logger)
        {
            _logger = logger;
        }

        public async Task<string> ReadAsync(string path)
        {
            var fileText = string.Empty;
            using (StreamReader stream = File.OpenText(path))
            {
                try
                {
                    fileText = await stream.ReadToEndAsync();
                    _logger.LogInformation($"Number of chars read from file {fileText.Length}");
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
