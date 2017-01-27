using System;
using System.IO;
using System.Threading.Tasks;
using NLog;

namespace FileWR.Business
{
    public class FileWriter : IFileWriter
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public async Task<string> CreateFileAsync(string path, string content)
        {
            try
            {
                using (StreamWriter writer = File.CreateText(path))
                {
                    await writer.WriteAsync(content);
                }

                _logger.Info($"File created at path: {path}");
            }
            catch (Exception ex)
            {
                _logger.Error($"Unable to create file: {ex}");
            }

            return path;
        }
    }

    public interface IFileWriter
    {
        Task<string> CreateFileAsync(string path, string content);
    }
}
