using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using RentVehicle.Core.Classes.Convertors;
using RentVehicle.Core.Interfaces;
using RentVehicle.Core.ViewModels;

namespace RentVehicle.Controllers
{
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
    }
}
