using CMS_Project.Data.Context;
using CMS_Project.Data.Repositories.Concrete.Base;
using CMS_Project.Data.Repositories.Interface.EntityTypeRepositories;
using CMS_Project.Entity.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS_Project.Data.Repositories.Concrete.EntityTypeRepositories
{
    public class CategoryRepository:KernelRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext applicationDbContext):base(applicationDbContext) { }
    }
}
