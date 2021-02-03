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
    public class UserService : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> FindUserBy(int id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<User> FindUserBy(string name)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<List<User>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task Remove(string name)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Name == name);

            if (user != null)
            {
                user.IsRemoved = true;
                user.RemoveDate = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task<User> Register(RegisterUserViewModel viewModel)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Name == viewModel.Name);

            if (user == null)
            {
                user = new User(viewModel.Name);
                await _dbContext.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }

            return user;
        }
    }
}