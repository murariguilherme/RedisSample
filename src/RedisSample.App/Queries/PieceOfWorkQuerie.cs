using RedisSample.DataDomain.Interfaces;
using RedisSample.DataDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedisSample.App.Queries
{
    public class PieceOfWorkQuerie : IPieceOfWorkQuerie
    {
        private IPieceOfWorkRepository _repository;

        public PieceOfWorkQuerie(IPieceOfWorkRepository repository)
        {
            _repository = repository;
        }

        public async Task<PieceOfWork> GetById(Guid id)
        {
            var result = await _repository.Read(id);
            return result;
        }

        public async Task<List<PieceOfWork>> GetList()
        {
            return (List<PieceOfWork>)await _repository.GetAll();
        }
    }
}
