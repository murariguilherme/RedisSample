using System;
using System.Collections.Generic;
using System.Text;
using RedisSample.DataDomain.Models;

namespace RedisSample.DataDomain.Interfaces
{
    public interface IPierceOfWorkRepository: IRepository<PieceOfWork>
    {
    }
}
