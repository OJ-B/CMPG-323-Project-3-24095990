using DeviceManagement.Data.Repository.Interfaces;
using DeviceManagement.Database.Data;
using DeviceManagement.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DeviceManagement.Data.Repository.Models
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        private readonly ConnectedOfficeContext _context;

        public DeviceRepository(ConnectedOfficeContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Device>> GetAllDevices()
        {
            return await Task.FromResult(
                _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
            );
        }

        public async Task<Device> GetAllById(Expression<Func<Device, bool>> expression)
        {
            var device = await _context.Device
                .Include(d => d.Category)
                .Include(d => d.Zone)
                .FirstOrDefaultAsync(expression);

            return device;
        }
    }
}
