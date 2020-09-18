using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RedisSample.DataDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisSample.DataDomain.Mappings
{
    public class EmployeeMapping : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }

    public class PieceOfWorkMapping : IEntityTypeConfiguration<PieceOfWork>
    {
        public void Configure(EntityTypeBuilder<PieceOfWork> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
