using System.Linq;
using AgeRanger.ResourceAccess.Data.Contracts.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AgeRanger.ViewModels
{
    public class PersonModel
    {
        public PersonModel()
        {
            
        }
        public PersonModel(IPerson person, IQueryable<IAgeGroup> ageGroups)
        {
            Id = person.Id;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Age = person.Age ?? 1;
            var group = (from ag in ageGroups
                        where Age >= ag.MinAge && Age < ag.MaxAge
                         select ag.Description).FirstOrDefault();
            if (group != null)
            {
                AgeGroup = group;
            }
        }
		[Range(0, 999)]
		public int Age { get; set; }

		[Required]
        public string FirstName { get; set; }
		
        public int Id { get; set; }
		[Required]
		public string LastName { get; set; }
		
		public string AgeGroup { get; set; }
    }
}