using AgeRanger.ResourceAccess.Data.Contracts.Models;
using System;

namespace AgeRanger.ResourceAccess.Data.Models
{
	public class Person : IPerson
    {
		public Person(Guid taskId, string taskName)
		{
			TaskId = taskId;
			TaskName = taskName;
		}
		public Person()
		{

		}
        public int? Age { get; set; }

        public string FirstName { get; set; }

        public int Id { get; set; }

        public string LastName { get; set; }

		public Guid TaskId { get; set; }

		public string TaskName { get; set; }

		public string CreatedBy { get; set; }

		public string CreatedAt { get; set; }
	}
}
