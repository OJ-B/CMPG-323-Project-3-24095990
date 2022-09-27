using DeviceManagement.Data.Repository.Interfaces;
using DeviceManagement.Database.Data;
using DeviceManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeviceManagement.Data.Repository.Models
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ConnectedOfficeContext context) : base(context) { }
    }
}
