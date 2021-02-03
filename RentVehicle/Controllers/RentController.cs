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
    [Route("[Controller]/{action=Index}")]
    public class RentController : Controller
    {
        private IRentRepository _repository;

        public RentController(IRentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var vehicles = await _repository.GetAllVehicles();

            //Select vehicles that have not been rented and Set into ViewBag
            ViewBag.Vehigles = vehicles.Where(x => x.IsRented == false).Select(x => new VehicleDetailViewModel()
            { Name = x.Name, Price = x.Price.ToString() });


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(AddRentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //Default manager 
                var creatorId = 1;

                var vehicle = await _repository.FindVehicleBy(viewModel.VehicleName);

                if (vehicle != null)
                {
                    var price = vehicle.Price * viewModel.Day;
                    await _repository.Rent(viewModel, creatorId, price);
                    return Content("اجاره نامه شما با موفقیت ثبت شد. از رانندگی لذت ببرید");
                }
                ModelState.AddModelError(nameof(AddRentViewModel.VehicleName), "خطا");
            }

            return View(viewModel);
        }

        //it is Fake Login : ->
        public IActionResult MyRentals()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> MyRentals(string name)
        {
            var pc = new PersianCalendar();
            var user = await _repository.FindUserBy(name);

            if (user != null)
            {
                var rents = await _repository.GetAll();
                var userRents = rents.Where(x => x.BuyerId == user.Id);

                var viewModel =
                    userRents.Select(x => new RentDetailViewModel()
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

                return View("List", viewModel);
            }

            ModelState.AddModelError("Name", "کاربری با این نام وجود ندارد");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(string number)
        {
            //Default manager 
            var cancellerId = 1;

            var user = await _repository.FindRentBy(number);

            if (user != null)
            {
                await _repository.CancelRent(number, cancellerId);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
