using RedisSample.DataDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedisSample.App.Queries
{
    public interface IPieceOfWorkQuerie
    {
        Task<PieceOfWork> GetById(Guid id);
        Task<List<PieceOfWork>> GetList();
    }
}
