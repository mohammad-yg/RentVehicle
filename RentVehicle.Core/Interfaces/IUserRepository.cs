using System.Collections.Generic;
using System.Threading.Tasks;
using RentVehicle.Core.ViewModels;
using RentVehicle.DataLayer.Entities;

namespace RentVehicle.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindUserBy(int id);
        Task<User> FindUserBy(string name);
        Task<List<User>> GetAll();
        Task Remove(string name);
        Task<User> Register(RegisterUserViewModel viewModel);
    }
}