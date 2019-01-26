using System;
using System.Collections.Generic;
using System.Linq;
using Library.Repositories;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;

namespace Library
{
    public class GetPetsByOwnersGenderService : IService<GetCatsByOwnersGenderRequest, GetCatsByOwnersGenderResponse>
    {
        private readonly ILogger _logger;
        private readonly IRepository<Owner> _repository;

        public GetPetsByOwnersGenderService(ILoggerFactory loggerFactory, IRepository<Owner> repository)
        {
            _logger = loggerFactory.CreateLogger<GetPetsByOwnersGenderService>();
            _repository = repository;
        }

        public GetCatsByOwnersGenderResponse Invoke(GetCatsByOwnersGenderRequest request)
        {
            _logger.LogTrace(JsonConvert.SerializeObject(request));

            var owners = _repository.GetAll();

            if (!string.IsNullOrEmpty(request.OwnerGender))
            {
                owners = owners.Where(x => x.Gender.Equals(request.OwnerGender,
                      StringComparison.OrdinalIgnoreCase));
            }

            var grp = owners.GroupBy(x => x.Gender);

            var catsByOwnersGenders  = new List<CatsByOwnersGender>();

            grp.ToList().ForEach(g => catsByOwnersGenders.Add(new CatsByOwnersGender
            {
                OwnerGender = g.Key,
                Cats = g.Where(o => o.Pets != null)
                            .SelectMany(y => y.Pets
                                .Where(p => p.Type.Equals("Cat", StringComparison.OrdinalIgnoreCase))
                                .Select(p => p.Name)
                                .OrderBy(x => x))
            }));

            var response = new GetCatsByOwnersGenderResponse
            {
                CatsByOwnersGenders = catsByOwnersGenders
            };

            _logger.LogTrace(JsonConvert.SerializeObject(response));

            return response;
        }
    }
}
