using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Models;

namespace Library.Repositories
{
    public class OwnersRepository : IRepository<Owner>
    {
        private readonly ILogger _logger;
        private readonly PetsContext _petsContext;

        public OwnersRepository(ILoggerFactory loggerFactory, PetsContext petsContext)
        {
            _logger = loggerFactory.CreateLogger<OwnersRepository>();
            _petsContext = petsContext;
        }

        public IEnumerable<Owner> GetAll()
        {
            return _petsContext.Owners;
        }
    }
}
