using CMS_Project.Data.Context;
using CMS_Project.Data.Repositories.Concrete.Base;
using CMS_Project.Data.Repositories.Interface.EntityTypeRepositories;
using CMS_Project.Entity.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS_Project.Data.Repositories.Concrete.EntityTypeRepositories
{
    public class ProductRepository: KernelRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) { }
    }
}
