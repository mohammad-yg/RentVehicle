using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using RentVehicle.Core.Classes.Convertors;
using RentVehicle.Core.Interfaces;
using RentVehicle.Core.ViewModels;

namespace RentVehicle.Areas.Admin.Controllers
{
    [Area("admin")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var pc = new PersianCalendar();
            var users = await _repository.GetAll();

            var viewModel =
                users.Select(x =>
                    new UserDetailViewModel()
                    {
                        Name = x.Name,
                        RegisterDate = pc.PersianDate(x.RegisterDate),
                        IsRemoved = x.IsRemoved,
                        RemoveDate = x.RemoveDate != null ? pc.PersianDate(x.RemoveDate ?? DateTime.MinValue) : ""
                    });
            return View(viewModel);
        }

        public async Task<IActionResult> Detail(string name)
        {
            var user = await _repository.FindUserBy(name);
            var pc = new PersianCalendar();

            if (user != null)
            {
                var viewModel = new UserDetailViewModel()
                {
                    Name = user.Name,
                    RegisterDate = pc.PersianDate(user.RegisterDate),
                    IsRemoved = user.IsRemoved,
                    RemoveDate = user.RemoveDate != null ? pc.PersianDate(user.RemoveDate ?? DateTime.MinValue) : ""
                };
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(RegisterUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _repository.FindUserBy(viewModel.Name);

                if (user == null)
                {
                    await _repository.Register(viewModel);
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(nameof(RegisterUserViewModel.Name), "کاربری با این نام وجود دارد");

            }
            return View(viewModel);
        }

        public async Task<IActionResult> Remove(string name)
        {
            var user = await _repository.FindUserBy(name);

            if (user != null)
            {
                await _repository.Remove(name);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
