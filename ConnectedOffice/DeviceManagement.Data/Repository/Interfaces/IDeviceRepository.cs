using DeviceManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManagement.Data.Repository.Interfaces
{
    public interface IDeviceRepository : IGenericRepository<Device> 
    {
        Task<IEnumerable<Device>> GetAllDevices();
        Task<Device> GetAllById(Expression<Func<Device, bool>> expression);
    }
}
