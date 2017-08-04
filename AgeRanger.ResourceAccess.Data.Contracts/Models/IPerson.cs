using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRanger.ResourceAccess.Data.Contracts.Models
{
    public interface IPerson: IAuditable, IDataAccessObject
	{
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int? Age { get; set; }
    }
}
