using AgeRanger.Core.Contracts;
using AgeRanger.Core.Contracts.Mapper;
using AgeRanger.ResourceAccess.Data.Contracts.Models;
using AgeRanger.ResourceAccess.Data.Models;
using AgeRanger.ViewModels;

namespace AgeRanger.Mappers
{
    public class PersonModelToPersonMapper : IMapper<PersonModel, IPerson>, ISingletonDependency
    {
        public IPerson Map(PersonModel from)
        {
			var person = new Person()
            {
                Age = from.Age,
                FirstName =  from.FirstName,
                LastName = from.LastName,
                Id = from.Id
            };
            return person;
        }
    }
}