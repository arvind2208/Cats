using Microsoft.Extensions.Logging;
using System.IO;

namespace Library.FileReaders
{
    public class FileSystem : IFileSystem
    {
        private readonly ILogger _logger;
        public FileSystem(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FileSystem>();
        }

        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
