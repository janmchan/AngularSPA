using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeRanger.ResourceAccess.Data.Contracts.Models;

namespace AgeRanger.ResourceAccess.Data.Models
{
    public class AgeGroup: IAgeGroup
    {
        public int Id { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; } = Int32.MaxValue;
        public string Description { get; set; }
    }
}
