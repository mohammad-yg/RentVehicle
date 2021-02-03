using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentVehicle.Core.ViewModels;
using RentVehicle.DataLayer.Contexts;
using RentVehicle.DataLayer.Entities;

namespace RentVehicle.Core.Interfaces
{
    public interface IRentRepository
    {
        Task<Rent> FindRentBy(int id);
        Task<Rent> FindRentBy(string number);
        Task<Vehicle> FindVehicleBy(string name);
        Task<User> FindUserBy(string name);
        Task<List<Rent>> GetAll();
        Task<List<Vehicle>> GetAllVehicles();
        Task Rent(AddRentViewModel viewModel, int creatorId , double price);
        Task CancelRent(string number , int cancellerId);
    }
}