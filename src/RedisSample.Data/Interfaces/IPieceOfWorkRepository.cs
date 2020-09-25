using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RedisSample.DataDomain.Models;

namespace RedisSample.DataDomain.Interfaces
{
    public interface IPieceOfWorkRepository: IRepository<PieceOfWork>
    {
        Task<IEnumerable<PieceOfWork>> GetListWithoutCache();
    }
}
