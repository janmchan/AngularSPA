using Newtonsoft.Json;
using System;
namespace AgeRanger.ResourceAccess.Data.Contracts
{
	public interface IAuditable
	{
		[JsonIgnore]
		Guid TaskId { get; set; }
		[JsonIgnore]
		string TaskName { get; set; }
		[JsonIgnore]
		string CreatedBy { get; set; }
		[JsonIgnore]
		string CreatedAt { get; set; }
	}
}
