using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRanger.ResourceAccess.Data.Contracts.Models
{
    public interface IAgeGroup: IDataAccessObject
	{
        int Id { get; set; }
        int MinAge { get; set; }
        int MaxAge { get; set; }
        string Description { get; set; }
    }
}
