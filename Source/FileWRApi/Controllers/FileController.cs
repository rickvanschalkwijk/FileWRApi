using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using FileWR.Business;
using FileWR.Business.Services;
using FileWRApi.Models;

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
            // create the directory
            var dirPath = await _directoryService.CreateDirectoryAsync("input");

            // construct all the file paths for input and output files
            ConstructFilePaths(dirPath);

            // create the input file with random chars and digits
            var filePath = await _fileWriter.CreateFileAsync(inputFilePath, ContentHelper.GenerateFileContents(chars));

            // read the create file content
            var content = await _fileReader.ReadAsync(filePath);

            // split between chars and digits
            var splitFileContent = ContentHelper.SplitFileContent(content);

            // write them to seperate files
            await _fileWriter.CreateFileAsync(outPutCharFilePath, splitFileContent["chars"]);
            await _fileWriter.CreateFileAsync(outPutDigitsFilePath, splitFileContent["digits"]);

            var result = new GenerateResult
            {
                NumberOfCharsGenerted = splitFileContent["chars"].Length,
                NumberOfDigitestGenereted = splitFileContent["digits"].Length
            };

            return Ok(result);
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
