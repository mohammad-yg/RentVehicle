using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentVehicle.Core.Classes.Convertors;
using RentVehicle.Core.Interfaces;
using RentVehicle.Core.ViewModels;
using RentVehicle.DataLayer.Entities;

namespace RentVehicle.Areas.Admin.Controllers
{
    [Area("admin")]
    public class VehicleController : Controller
    {
        private IVehicleRepository _repository;

        public VehicleController(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var pc = new PersianCalendar();
            var vehicles = await _repository.GetAll();

            var viewModel = vehicles.Select(x =>
                new VehicleDetailViewModel()
                {
                    Name = x.Name,
                    CreatorName = x.Creator?.Name,
                    IsRemoved = x.IsRemoved,
                    RemoveDate = x.RemoveDate != null ? pc.PersianDate(x.RemoveDate ?? DateTime.MinValue) : "",
                    CreateDate = pc.PersianDate(x.CreateDate),
                    IsRented = x.IsRented,
                    Price = x.Price.ToString(),
                    VehicleType = x.VehicleType.ToPersianText()
                });
            return View(viewModel);
        }

        public async Task<IActionResult> Detail(string name)
        {
            var vehicle = await _repository.FindVehicleBy(name);

            if (vehicle != null)
            {
                var pc = new PersianCalendar();

                var viewModel = new VehicleDetailViewModel()
                {
                    Name = vehicle.Name,
                    CreatorName = vehicle.Creator?.Name,
                    IsRemoved = vehicle.IsRemoved,
                    RemoveDate = vehicle.RemoveDate != null ? pc.PersianDate(vehicle.RemoveDate ?? DateTime.MinValue) : "",
                    CreateDate = pc.PersianDate(vehicle.CreateDate),
                    IsRented = vehicle.IsRented,
                    Price = vehicle.Price.ToString(),
                    VehicleType = vehicle.VehicleType.ToPersianText()
                };

                return View(viewModel);
            }

            return Redirect(nameof(Index));
        }

        public IActionResult Add()
        {
            var types = Enum.GetValues(typeof(VehicleType)).Cast<VehicleType>().Select(x=>new string[]{x.ToString() , x.ToPersianText()});

            ViewBag.VehcleTypes = types;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddVehicleViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //Default manager
                var creatorId = 1;

                var vehicle = await _repository.FindVehicleBy(viewModel.Name);

                if (vehicle == null)
                {
                    await _repository.Add(viewModel, creatorId);
                    return Redirect(nameof(Index));
                }
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Remove(string name)
        {
            //Default manager
            var removerId = 1;

            var vehicle = await _repository.FindVehicleBy(name);

            if (vehicle != null)
            {
                await _repository.Remove(name, removerId);
                return Redirect(nameof(Index));
            }

            return Redirect(nameof(Index));
        }
    }
}
