using Library.Repositories;
using Microsoft.Extensions.Logging;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class OwnersRepositorySpec
    {
        //edge cases ignored for brevity

        [Fact]
        public void GetAll_WhenInvoked_ReturnsAllOwnersWithPets()
        {
            var petsContext = new PetsContext
            {
                Owners = new List<Owner>
                {
                    new Owner
                {
                    Name = "Bob",
                    Gender = "Male",
                    Age = 23,
                    Pets= new List<Pet>
                    {
                        new Pet
                        {
                            Name = "Garfield",
                            Type = "Cat"
                        },
                        new Pet
                        {
                            Name = "Fido",
                            Type = "Dog"
                        }
                    }
                },
                new Owner
                {
                    Name = "Jennifer",
                    Gender = "Female",
                    Age = 18,
                    Pets= new List<Pet>
                    {
                        new Pet
                        {
                            Name = "Garfield",
                            Type = "Cat"
                        }
                    }
                },
                new Owner
                {
                    Name = "Fred",
                    Gender = "Male",
                    Age = 40,
                    Pets = null
                }
            }
            };

            var repository = new OwnersRepository(new LoggerFactory(), petsContext);

            var result = repository.GetAll();

            Assert.Equal(3, result.Count());
            Assert.Equal(3, result.Where(o => o.Pets != null).SelectMany(x => x.Pets).Count());
        }
    }
}
