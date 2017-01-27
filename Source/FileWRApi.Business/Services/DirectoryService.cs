using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using NLog;

namespace FileWR.Business.Services
{
    public class DirectoryService : IDirectoryService
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public async Task<string> CreateDirectoryAsync(string dirName)
        {
            try
            {
                var currentDirectory = HttpRuntime.AppDomainAppPath;
                var directoryInfo = Directory.CreateDirectory(Path.Combine(currentDirectory, dirName));

                return directoryInfo.FullName;
            }
            catch (Exception ex)
            {
                _logger.Error($"application has thrown an exception: {ex}");
                throw new IOException();
            }
        }
    }

    public interface IDirectoryService
    {
        Task<string> CreateDirectoryAsync(string dirName);
    }
}
