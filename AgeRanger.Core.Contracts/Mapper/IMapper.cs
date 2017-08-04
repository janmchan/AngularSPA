using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRanger.Core.Contracts.Mapper
{
    public interface IMapper<in TFrom, out To>
    {
        To Map(TFrom from);
    }
}
