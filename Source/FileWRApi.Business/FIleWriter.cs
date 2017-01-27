using System;
using System.IO;
using System.Threading.Tasks;
using FileWR.Business.Services;
using Microsoft.Extensions.Logging;

namespace FileWR.Business
{
    public class FileWriter : IFileWriter
    {
        private readonly ILogger<FileWriter> _logger;

        public FileWriter(ILogger<FileWriter> logger, IDirectoryService directoryService)
        {
            _logger = logger;
        }

        public async Task<string> CreateFileAsync(string path, string content)
        {
            try
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    await writer.WriteAsync(content);
                }

                _logger.LogInformation($"File created at path: {path}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unable to create file: {ex}");
            }

            return path;
        }
    }

    public interface IFileWriter
    {
        Task<string> CreateFileAsync(string path, string content);
    }
}
