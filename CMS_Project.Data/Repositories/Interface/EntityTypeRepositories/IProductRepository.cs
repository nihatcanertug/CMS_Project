using CMS_Project.Data.Repositories.Interface.Base;
using CMS_Project.Entity.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS_Project.Data.Repositories.Interface.EntityTypeRepositories
{
    public interface IProductRepository : IKernelRepository<Product> { }
   
}
