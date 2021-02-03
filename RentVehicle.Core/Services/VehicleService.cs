using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentVehicle.Core.Interfaces;
using RentVehicle.Core.ViewModels;
using RentVehicle.DataLayer.Contexts;
using RentVehicle.DataLayer.Entities;

namespace RentVehicle.Core.Services
{
    public class VehicleService : IVehicleRepository
    {
        private readonly AppDbContext _dbContext;

        public VehicleService(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Vehicle> FindVehicleBy(int id)
        {
            return await _dbContext.Vehicles.FindAsync(id);
        }

        public async Task<Vehicle> FindVehicleBy(string name)
        {
            return await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<List<Vehicle>> GetAll()
        {
            return await _dbContext.Vehicles.Include(x=>x.Creator).ToListAsync();
        }

        public async Task Add(AddVehicleViewModel viewModel, int creatorId)
        {
            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.Name == viewModel.Name);
            var user = await _dbContext.Users.FindAsync(creatorId);
            var vehicleType = (VehicleType)Enum.Parse(typeof(VehicleType), viewModel.VehicleType, true);
            var price = double.Parse(viewModel.Price);

            if (vehicle == null && user != null)
            {
                vehicle = new Vehicle(user.Id , viewModel.Name , vehicleType, price);
                await _dbContext.Vehicles.AddAsync(vehicle);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task Remove(string name, int removerId)
        {
            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.Name == name);

            if (vehicle != null)
            {
                vehicle.IsRemoved = true;
                vehicle.RemoveDate = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}