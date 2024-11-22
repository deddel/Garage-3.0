using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage3._0.Data;
using Garage3._0.Models.Entities;
using Garage3._0.Helper;
using Garage3._0.Models.ViewModels;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;


namespace Garage3._0.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ParkedVehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: ParkedVehicles
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.TypeSortParm = string.IsNullOrEmpty(sortOrder) ? "type" : "";
            ViewBag.RegSortParm = sortOrder == "reg" ? "reg_desc" : "reg";
            ViewBag.ColorSortParm = sortOrder == "color" ? "color_desc" : "color";
            ViewBag.BrandSortParm = sortOrder == "brand" ? "brand_desc" : "brand";
            ViewBag.ModelSortParm = sortOrder == "model" ? "model_desc" : "model";
            ViewBag.WheelSortParm = sortOrder == "wheel_desc" ? "wheel" : "wheel_desc";
            ViewBag.ArrivalTimeSortParm = sortOrder == "arrivalTime_desc" ? "arrivalTime" : "arrivalTime_desc";
            var order = from s in _context.ParkedVehicle
                        select s;
            switch (sortOrder)
            {
                case "type":
                    order = order.OrderByDescending(s => s.VehicleType);
                    break;
                case "reg":
                    order = order.OrderBy(s => s.RegistrationNumber);
                    break;
                case "reg_desc":
                    order = order.OrderByDescending(s => s.RegistrationNumber);
                    break;
                case "color":
                    order = order.OrderBy(s => s.Color);
                    break;
                case "color_desc":
                    order = order.OrderByDescending(s => s.Color);
                    break;
                case "brand":
                    order = order.OrderBy(s => s.Brand);
                    break;
                case "brand_desc":
                    order = order.OrderByDescending(s => s.Brand);
                    break;
                case "model":
                    order = order.OrderBy(s => s.VehicleModel);
                    break;
                case "model_desc":
                    order = order.OrderByDescending(s => s.VehicleModel);
                    break;
                case "wheel":
                    order = order.OrderBy(s => s.Wheel);
                    break;
                case "wheel_desc":
                    order = order.OrderByDescending(s => s.Wheel);
                    break;
                case "arrivalTime":
                    order = order.OrderBy(s => s.ArrivalTime);
                    break;
                case "arrivalTime_desc":
                    order = order.OrderByDescending(s => s.ArrivalTime);
                    break;
                default:
                    order = order.OrderBy(s => s.VehicleType);
                    break;
            }
            return View(await order.ToListAsync());
        }

        public async Task<IActionResult> Filter(int? type, string regNr, string color, string brand, string model, int? wheels, DateTime arrivalTime)
        {

            var filtered = type is null ?
                _context.ParkedVehicle :
                _context.ParkedVehicle.Where(m => m.VehicleType.Id == type);

            filtered = string.IsNullOrWhiteSpace(regNr) ?
                filtered :
                filtered.Where(m => m.RegistrationNumber!.Contains(regNr));

            filtered = string.IsNullOrWhiteSpace(color) ?
                filtered :
                filtered.Where(m => m.Color!.Contains(color));

            filtered = string.IsNullOrWhiteSpace(brand) ?
                filtered :
                filtered.Where(m => m.Brand!.Contains(brand));

            filtered = string.IsNullOrWhiteSpace(model) ?
                filtered :
                filtered.Where(m => m.VehicleModel!.Contains(model));

            filtered = wheels is null ?
                filtered :
                filtered.Where(m => (int)m.Wheel == wheels);

            filtered = arrivalTime == new DateTime(0001, 01, 01, 00, 00, 00) ?
            filtered :
            filtered.Where(m => m.ArrivalTime.Date == arrivalTime.Date);

            if (filtered.IsNullOrEmpty())
            {
                TempData["errorMessage"] = "Vehicle not found.";
            }

            return View(nameof(Overview), await filtered!.ToListAsync());
        }

        public async Task<IActionResult> Filter2(int? type, string regNr, DateTime arrivalTime)
        {
            var filtered = _context.ParkedVehicle.Select(p => new ParkedViewModel
            {
                Id = p.Id,
                Type = p.VehicleType,
                RegistrationNumber = p.RegistrationNumber,
                ArrivalTime = p.ArrivalTime,
                ParkedTime = DateTime.Now - p.ArrivalTime

            });

            filtered = type is null ?
               filtered :
               filtered.Where(m => m.Type.Id == type);

            filtered = string.IsNullOrWhiteSpace(regNr) ?
                filtered :
                filtered.Where(m => m.RegistrationNumber!.Contains(regNr));

            filtered = arrivalTime == new DateTime(0001, 01, 01, 00, 00, 00) ?
                filtered :
                filtered.Where(m => m.ArrivalTime.Date == arrivalTime.Date);

            if (filtered.IsNullOrEmpty())
            {
                TempData["errorMessage"] = "Vehicle not found.";
            }

            return View(nameof(Overview), await filtered.ToListAsync());
        }

        public async Task<IActionResult> StatisticsView()
        {
            var vehicleTypes = await _context.VehicleType.ToListAsync();
            var parkedVehicles = await _context.ParkedVehicle.Select(p => new
            {
                Wheel = p.Wheel,
                Color = p.Color,
                Brand = p.Brand,
                Model = p.VehicleModel,
                Type = p.VehicleType.VehicleTypeName,
                ParkingFee = ParkingHelper.ParkingFee(p.ArrivalTime, DateTime.Now)
            })
            .ToListAsync();

            var typeCounts = parkedVehicles
                .GroupBy(p => p.Type)
                .ToDictionary(g => g.Key, g => g.Count());

            var vehicleTypeCounts = vehicleTypes.ToDictionary(
                vt => vt.VehicleTypeName,
                vt => typeCounts.GetValueOrDefault(vt.VehicleTypeName, 0).ToString());

            int amountWheels = parkedVehicles.Sum(s => s.Wheel);
            decimal sum = 0;
            foreach (var s in parkedVehicles)
            {
                sum += s.ParkingFee;
            }

            var displayStats = new StatisticsDisplayViewModel
            {
                Cars = vehicleTypeCounts.GetValueOrDefault("Car", "0"),
                Boats = vehicleTypeCounts.GetValueOrDefault("Boat", "0"),
                Buses = vehicleTypeCounts.GetValueOrDefault("Bus", "0"),
                Motorcycles = vehicleTypeCounts.GetValueOrDefault("Motorcycle", "0"),
                Airplanes = vehicleTypeCounts.GetValueOrDefault("Airplane", "0"),
                Wheels = amountWheels,
                Sum = sum
            };

            return View(displayStats);
        }

        public async Task<IActionResult> Overview(string sortOrder)
        {
            var model = _context.ParkedVehicle.Select(p => new ParkedViewModel
            {
                Id = p.Id,
                Type = p.VehicleType,
                RegistrationNumber = p.RegistrationNumber,
                ArrivalTime = p.ArrivalTime,
                ParkedTime = DateTime.Now - p.ArrivalTime

            });

            ViewBag.TypeSortParm = string.IsNullOrEmpty(sortOrder) ? "type" : "";
            ViewBag.RegSortParm = sortOrder == "reg" ? "reg_desc" : "reg";
            ViewBag.ArrivalTimeSortParm = sortOrder == "arrivalTime_desc" ? "arrivalTime" : "arrivalTime_desc";

            var order = from s in model
                        select s;
            switch (sortOrder)
            {
                case "type":
                    order = order.OrderByDescending(s => s.Type);
                    break;
                case "reg":
                    order = order.OrderBy(s => s.RegistrationNumber);
                    break;
                case "reg_desc":
                    order = order.OrderByDescending(s => s.RegistrationNumber);
                    break;
                case "arrivalTime":
                    order = order.OrderBy(s => s.ArrivalTime);
                    break;
                case "arrivalTime_desc":
                    order = order.OrderByDescending(s => s.ArrivalTime);
                    break;
                default:
                    order = order.OrderBy(s => s.Type);
                    break;
            }

            return View(await order.ToListAsync());
        }

        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Park
        public IActionResult Park()
        {
            return View();
        }

        // POST: ParkedVehicles/Park
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Park([Bind("Id,VehicleType,RegistrationNumber,Color,Brand,VehicleModel,Wheel")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                DateTime dateTime = DateTime.Now;
                dateTime = new DateTime(
                    dateTime.Ticks - (dateTime.Ticks % TimeSpan.TicksPerSecond),
                    dateTime.Kind
                );
                parkedVehicle.ArrivalTime = dateTime;
                if (parkedVehicle.RegistrationNumber != null)
                {
                    parkedVehicle.RegistrationNumber = parkedVehicle.RegistrationNumber.ToUpper();
                }
                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Vehicle {parkedVehicle.RegistrationNumber} successfully parked.";
                return RedirectToAction(nameof(Overview));
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleType,RegistrationNumber,Color,Brand,VehicleModel,Wheel,ArrivalTime")] ParkedVehicle parkedVehicle)
        {
            if (id != parkedVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkedVehicle);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Vehicle details for {parkedVehicle.RegistrationNumber} updated.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleExists(parkedVehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Overview));
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            string? regId = parkedVehicle?.RegistrationNumber;
            if (parkedVehicle != null)
            {
                _context.ParkedVehicle.Remove(parkedVehicle);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Vehicle {regId} successfully unparked.";
            return RedirectToAction(nameof(Overview));
        }

        // GET: ParkedVehicles/Receipt/5
        public async Task<IActionResult> ReceiptView(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);

            if (parkedVehicle == null)
            {
                return NotFound();
            }

            DateTime timeNow = DateTime.Now;
            DateTime arrivalTime = parkedVehicle.ArrivalTime;

            var model = new ReceiptViewModel
            {
                Id = parkedVehicle.Id,
                RegistrationNumber = parkedVehicle.RegistrationNumber,
                ArrivalTime = arrivalTime,
                DepartureTime = timeNow,
                ParkedTime = (timeNow - arrivalTime),
                ParkedFee = ParkingHelper.ParkingFee(arrivalTime, timeNow)
            };

            return View(model);
        }

        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.Id == id);
        }
    }
}
