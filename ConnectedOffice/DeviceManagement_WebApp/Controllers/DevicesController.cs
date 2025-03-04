﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement.Database.Models;
using DeviceManagement.Data.Repository.Interfaces;

namespace DeviceManagement_WebApp.Controllers
{
    public class DevicesController : Controller
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IZoneRepository _zoneRepository;

        public DevicesController(IDeviceRepository deviceRepository, ICategoryRepository categoryRepository, IZoneRepository zoneRepository)
        {
            _deviceRepository = deviceRepository;
            _categoryRepository = categoryRepository;
            _zoneRepository = zoneRepository;
        }

        // GET: Devices
        public async Task<IActionResult> Index()
        {
            return View(await _deviceRepository.GetAllDevices());
        }

        // GET: Devices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _deviceRepository.GetAllById(m => m.DeviceId == id);
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // GET: Devices/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_categoryRepository.GetAll().Result, "CategoryId", "CategoryName");
            ViewData["ZoneId"] = new SelectList(_zoneRepository.GetAll().Result, "ZoneId", "ZoneName");
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            device.DeviceId = Guid.NewGuid();

            await _deviceRepository.Add(device);

            return RedirectToAction(nameof(Index));
        }

        // GET: Devices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var device = await _deviceRepository.GetById(id);
            if (device == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_categoryRepository.GetAll().Result, "CategoryId", "CategoryName", device.CategoryId);
            ViewData["ZoneId"] = new SelectList(_zoneRepository.GetAll().Result, "ZoneId", "ZoneName", device.ZoneId);
            return View(device);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DeviceId,DeviceName,CategoryId,ZoneId,Status,IsActive,DateCreated")] Device device)
        {
            if (id != device.DeviceId)
            {
                return NotFound();
            }
            try
            {
                await _deviceRepository.Update(device);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(device.DeviceId).Result)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Devices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var device = await _deviceRepository.GetAllById(m => m.DeviceId == id);
            
            if (device == null)
            {
                return NotFound();
            }

            return View(device);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var device = await _deviceRepository.GetById(id);
            await _deviceRepository.Remove(device);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> DeviceExists(Guid id)
        {
            return (await _deviceRepository.Find(m => m.CategoryId == id)).Any();
        }
    }
}
