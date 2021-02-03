using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using RentVehicle.Core.Classes.Convertors;
using RentVehicle.Core.Interfaces;
using RentVehicle.Core.ViewModels;

namespace RentVehicle.Areas.Admin.Controllers
{
    [Area("admin")]
    public class RentController : Controller
    {
        private IRentRepository _repository;

        public RentController(IRentRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var pc = new PersianCalendar();
            var rents = await _repository.GetAll();

            var viewModel =
                rents.Select(x => new RentDetailViewModel()
                {
                    Price = x.Price.ToString(),
                    Day = x.Day.ToString(),
                    Number = x.Number.ToString(),
                    VehicleName = x.Vehicle?.Name,
                    RentDate = pc.PersianDate(x.RentDate),
                    BuyerName = x.Buyer?.Name,
                    CancelDate = x.CancelDate != null ? pc.PersianDate(x.CancelDate ?? DateTime.MinValue) : "",
                    IsCanceled = x.IsCanceled
                });

            return View(viewModel);
        }

        public async Task<IActionResult> Detail(string number)
        {
            var pc = new PersianCalendar();
            var rent = await _repository.FindRentBy(number);

            if (rent != null)
            {
                var viewModel = new RentDetailViewModel()
                {
                    Price = rent.Price.ToString(),
                    Day = rent.Day.ToString(),
                    Number = rent.Number.ToString(),
                    VehicleName = rent.Vehicle?.Name,
                    RentDate = pc.PersianDate(rent.RentDate),
                    BuyerName = rent.Buyer?.Name,
                    CancelDate = rent.CancelDate != null ? pc.PersianDate(rent.CancelDate ?? DateTime.MinValue) : "",
                    IsCanceled = rent.IsCanceled
                };
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(string number)
        {
            //Default manager 
            var cancellerId = 1;

            var rent = await _repository.FindRentBy(number);

            if (rent != null)
            {
                await _repository.CancelRent(number , cancellerId);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
