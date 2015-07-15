using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemedicine.Core.Data
{
    public interface IDbContextProvider
    {
        DataContext Context { get; }
    }
}
