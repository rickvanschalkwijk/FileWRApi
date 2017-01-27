using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FileWR.Business.Services
{
    public class DirectoryService : IDirectoryService
    {
        private readonly ILogger<DirectoryService> _logger;

        public DirectoryService(ILogger<DirectoryService> logger)
        {
            _logger = logger;
        }

        public async Task<string> CreateDirectoryAsync(string dirName)
        {
            try
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                var directoryInfo = Directory.CreateDirectory(Path.Combine(currentDirectory, dirName));

                return directoryInfo.FullName;
            }
            catch (Exception ex)
            {
                _logger.LogError($"application has thrown an exception: {ex}");
                throw new IOException();
            }
        }
    }

    public interface IDirectoryService
    {
        Task<string> CreateDirectoryAsync(string dirName);
    }
}
