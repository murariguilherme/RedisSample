using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedisSample.DataDomain.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
