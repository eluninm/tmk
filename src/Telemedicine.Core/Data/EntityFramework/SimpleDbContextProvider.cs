using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemedicine.Core.Data.EntityFramework
{
    public class SimpleDbContextProvider : IDbContextProvider
    {
        private readonly DataContext _dbContext = new DataContext();

        public DataContext Context { get { return _dbContext; }}
    }
}
