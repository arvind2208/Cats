using System;
using System.Collections.Generic;
using System.Linq;
using Library;
using Library.Repositories;
using Microsoft.Extensions.Logging;
using Models;
using Moq;
using Xunit;

namespace UnitTests
{
    public class GetPetsByOwnersGenderServiceSpec
    {
        private readonly Mock<IRepository<Owner>> _mockRepository;

        public GetPetsByOwnersGenderServiceSpec()
        {
            _mockRepository = new Mock<IRepository<Owner>>();
            _mockRepository.Setup(x => x.GetAll()).Returns(new List<Owner>
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
                    Pets= new List<Pet>
                    {
                        new Pet
                        {
                            Name = "Tom",
                            Type = "Cat"
                        },
                        new Pet
                        {
                            Name = "Max",
                            Type = "Cat"
                        },
                        new Pet
                        {
                            Name = "Sam",
                            Type = "Dog"
                        }
                    }
                },
                new Owner
                {
                    Name = "Samantha",
                    Gender = "Female",
                    Age = 40,
                    Pets= new List<Pet>
                    {
                        new Pet
                        {
                            Name = "Tabby",
                            Type = "Cat"
                        },
                        new Pet
                        {
                            Name = "Simba",
                            Type = "Cat"
                        },
                        new Pet
                        {
                            Name = "Nemo",
                            Type = "Fish"
                        }
                    }
                }
            });
        }
        [Theory]
        [InlineData("male")]
        [InlineData("female")]
        [InlineData("")]
        [InlineData(null)]
        public void GetPetsByOwnersGender_WhenQueriedByGender_ReturnsFilteredResult_OrderedByName(string gender)
        {
            var service = new GetPetsByOwnersGenderService(new LoggerFactory(), _mockRepository.Object);

            var response = service.Invoke(new GetCatsByOwnersGenderRequest
            {
                OwnerGender = gender
            });

            foreach (var petsByGender in response.CatsByOwnersGenders)
            {
                var catNames = petsByGender.Cats.ToList();

                Assert.Equal(3, catNames.Count);

                if (petsByGender.OwnerGender == "Male")
                {
                    Assert.Equal("Garfield", catNames[0]);
                    Assert.Equal("Max", catNames[1]);
                    Assert.Equal("Tom", catNames[2]);
                }
                else
                {
                    Assert.Equal("Garfield", catNames[0]);
                    Assert.Equal("Simba", catNames[1]);
                    Assert.Equal("Tabby", catNames[2]);
                }
            }
        }

        [Theory]
        [InlineData("male")]
        [InlineData("female")]
        [InlineData("")]
        [InlineData(null)]
        public void GetPetsByOwnersGender_GivenNoCats_WhenQueriedByGender_ReturnsEmptyList(string gender)
        {
            var mockRepository = new Mock<IRepository<Owner>>();
            mockRepository.Setup(x => x.GetAll()).Returns(new List<Owner>
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
                            Name = "Sam",
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
                    }
                },
                new Owner
                {
                    Name = "Fred",
                    Gender = "Male",
                    Age = 40,
                    Pets = null
                }
            });

            var service = new GetPetsByOwnersGenderService(new LoggerFactory(), mockRepository.Object);

            var response = service.Invoke(new GetCatsByOwnersGenderRequest
            {
                OwnerGender = gender
            });

            foreach (var petsByGender in response.CatsByOwnersGenders)
            {
                var pets = petsByGender.Cats.ToList();

                Assert.Empty(pets);
            }
        }
    }
}
