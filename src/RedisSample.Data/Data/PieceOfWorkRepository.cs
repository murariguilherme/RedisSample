using System;
using System.Collections.Generic;
using System.Text;
using RedisSample.DataDomain.Models;
using RedisSample.DataDomain.Interfaces;
using EasyCaching.Core;

namespace RedisSample.DataDomain.Data
{
    public class EmployerRepository: Repository<Employer>, IEmployerRepository
    {
        public EmployerRepository(AppDbContext context, IEasyCachingProviderFactory factory) : base(context, factory) { }
    }
}
