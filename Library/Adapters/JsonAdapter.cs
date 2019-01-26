using System.Collections.Generic;
using Newtonsoft.Json;
using Library.FileReaders;
using Models;
using Microsoft.Extensions.Logging;

namespace Library.Adapters
{
    public class JsonAdapter : IAdapter
    {
        private readonly ILogger _logger;
        private readonly IFileSystem _fileSystem;

        public JsonAdapter(ILoggerFactory loggerFactory, IFileSystem fileSystem)
        {
            _logger = loggerFactory.CreateLogger<JsonAdapter>();
            _fileSystem = fileSystem;
        }

        public void Fill(string filePath, PetsContext petsContext)
        {
            string content = _fileSystem.ReadAllText(filePath);

            var owners = JsonConvert.DeserializeObject<List<Owner>>(content);

            petsContext.Owners = owners;
        }
    }
}
