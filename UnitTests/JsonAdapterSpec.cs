using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Moq;
using Xunit;
using Library.Adapters;
using Library.FileReaders;
using Models;
using Microsoft.Extensions.Logging;
using System;

namespace UnitTests
{
    public class JsonAdapterSpec
    {
        private readonly Mock<IFileSystem> _mockFileSystem;

        public JsonAdapterSpec()
        {
            _mockFileSystem = new Mock<IFileSystem>();

            var testData = File.ReadAllText("../../../TestData/Pet.json");
            _mockFileSystem.Setup(x => x.ReadAllText(It.IsAny<string>()))
                .Returns(testData);
        }

        [Fact]
        public void GetAll_WhenInvoked_ReturnsAllOwnersWithPets()
        {
            var adapter = new JsonAdapter(new LoggerFactory(), _mockFileSystem.Object);

            var petsContext = new PetsContext();
            adapter.Fill(null, petsContext);

            Assert.Equal(6, petsContext.Owners.Count());
            Assert.Equal(10, petsContext.Owners.Where(o => o.Pets != null).SelectMany(x => x.Pets).Count());
        }
    }
}
