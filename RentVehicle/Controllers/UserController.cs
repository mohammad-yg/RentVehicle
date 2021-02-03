using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using RentVehicle.Core.Interfaces;
using RentVehicle.Core.ViewModels;

namespace RentVehicle.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _repository.FindUserBy(viewModel.Name);

                if (user == null)
                {
                    await _repository.Register(viewModel);
                    return Content("اطلاعات شما با موفقیت ثبت شد");
                }
                ModelState.AddModelError(nameof(RegisterUserViewModel.Name), "کاربری با این نام وجود دارد");
            }

            return View(viewModel);
        }
    }
}
