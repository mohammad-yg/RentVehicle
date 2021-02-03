using System.Collections.Generic;
using System.Threading.Tasks;
using RentVehicle.Core.ViewModels;
using RentVehicle.DataLayer.Entities;

namespace RentVehicle.Core.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> FindVehicleBy(int id);
        Task<Vehicle> FindVehicleBy(string name);
        Task<List<Vehicle>> GetAll();
        Task Add(AddVehicleViewModel viewModel , int creatorId);
        Task Remove(string name , int removerId);
    }
}