using System;
using System.Collections.Generic;
using System.Text;
using RedisSample.DataDomain.Models;
using RedisSample.DataDomain.Interfaces;
using EasyCaching.Core;

namespace RedisSample.DataDomain.Data
{
    public class PieceOfWorkRepository : Repository<PieceOfWork>, IPieceOfWorkRepository
    {
        public PieceOfWorkRepository(AppDbContext context, IEasyCachingProviderFactory factory) : base(context, factory) { }
    }
}
