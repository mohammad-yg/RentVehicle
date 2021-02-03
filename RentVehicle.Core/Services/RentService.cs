using System.Collections.Generic;
using System.Formats.Asn1;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentVehicle.Core.Classes.Generators;
using RentVehicle.Core.Interfaces;
using RentVehicle.Core.ViewModels;
using RentVehicle.DataLayer.Contexts;
using RentVehicle.DataLayer.Entities;

namespace RentVehicle.Core.Services
{
    public class RentService : IRentRepository
    {
        private readonly AppDbContext _dbContext;

        public RentService(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Rent> FindRentBy(int id)
        {
            return await _dbContext.Rents.FindAsync(id);
        }

        public async Task<Rent> FindRentBy(string number)
        {
            return await _dbContext.Rents.FirstOrDefaultAsync(x => x.Number == number);
        }

        public async Task<Vehicle> FindVehicleBy(string name)
        {
            return await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<User> FindUserBy(string name)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<List<Rent>> GetAll()
        {
            return await _dbContext.Rents
                .Include(x=>x.Buyer)
                .Include(x=>x.Vehicle)
                .ToListAsync();
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            return await _dbContext.Vehicles.ToListAsync();
        }

        public async Task Rent(AddRentViewModel viewModel, int creatorId, double price)
        {
            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.Name == viewModel.VehicleName);
            var user = await _dbContext.Users.FindAsync(creatorId);

            if (vehicle != null && user != null)
            {
                var rent = await _dbContext.Rents.FirstOrDefaultAsync(x => x.VehicleId == vehicle.Id);

                if (rent == null)
                {
                    rent = new Rent(viewModel.Day, user.Id, vehicle.Id, price);
                    rent.Number = CodeGenerator.GetNewRentNumber();
                    vehicle.IsRented = true;
                    await _dbContext.AddAsync(rent);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task CancelRent(string number, int cancellerId)
        {
            var rent = await _dbContext.Rents.Include(x=>x.Vehicle).FirstOrDefaultAsync(x => x.Number == number);

            if (rent != null)
            {
                var vehicle = rent.Vehicle;
                vehicle.IsRented = false;
                rent.IsCanceled = true;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}