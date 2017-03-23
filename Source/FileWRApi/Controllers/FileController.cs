using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using FileWR.Business;
using FileWR.Business.Services;
using FileWRApi.Models;
using NLog;
using FileWRApi.Business;
using System.Threading;
using System.Collections.Generic;

namespace FileWRApi.Controllers
{
    public class FileController : BaseController
    {
        private readonly IFileWriter _fileWriter;
        private readonly IFileReader _fileReader;
        private readonly IDirectoryService _directoryService;

        private string inputFilePath;
        private string outPutCharFilePath;
        private string outPutDigitsFilePath;

        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public FileController(IFileWriter fileWriter, IFileReader fileReader, IDirectoryService directoryService)
        {
            _fileWriter = fileWriter;
            _fileReader = fileReader;
            _directoryService = directoryService;
        }

        [HttpGet]
        [Route("file/create/{chars}")]
        public async Task<IHttpActionResult> Create(int chars)
        {
            var simpleSchedular = new SimpleTaskScheduler();
            var splitFileContent = new Dictionary<string, string>();

            using (simpleSchedular)
            {
                var tasks = new Task[2];
                string content = string.Empty;

                var taskOne = new Task(() => 
                {
                    WriteStatusAndSleep("start for 1 sec", 1000);
                    var dirPath = _directoryService.CreateDirectoryAsync("input").Result;
                    ConstructFilePaths(dirPath);
                });
                tasks[0] = taskOne;

                var taskTwo = new Task(() =>
                {
                    WriteStatusAndSleep("start for 1 sec", 1000);
                    var filePath = _fileWriter.CreateFileAsync(inputFilePath, ContentHelper.GenerateFileContents(chars)).Result;
                    content = _fileReader.ReadAsync(filePath).Result;
                });
                tasks[1] = taskTwo;

                var taskThree = new Task(() => 
                {
                    WriteStatusAndSleep("start for 1 sec", 1000);
                    splitFileContent = ContentHelper.SplitFileContent(content);
                    _fileWriter.CreateFileAsync(outPutCharFilePath, splitFileContent["chars"]);
                    _fileWriter.CreateFileAsync(outPutDigitsFilePath, splitFileContent["digits"]);
                });
                tasks[2] = taskThree;

                foreach (var t in tasks)
                {
                    t.Start(simpleSchedular);
                }
            }

            var result = new GenerateResult
            {
                NumberOfCharsGenerted = splitFileContent["chars"].Length,
                NumberOfDigitestGenereted = splitFileContent["digits"].Length
            };

            return Ok(result);
        }

        private void WriteStatusAndSleep(string msg, int sleepTime)
        {
            _logger.Info("on Thread " + Thread.CurrentThread.ManagedThreadId.ToString() + " -- " + msg);
            Thread.Sleep(sleepTime);
        }

        private void ConstructFilePaths(string dirPath)
        {
            // combine paths the get the file location
            inputFilePath = Path.Combine(dirPath, "random-input-file.txt");
            outPutCharFilePath = Path.Combine(dirPath, "char-output-file.txt");
            outPutDigitsFilePath = Path.Combine(dirPath, "digits-output-file.txt");
        }
    }
}
