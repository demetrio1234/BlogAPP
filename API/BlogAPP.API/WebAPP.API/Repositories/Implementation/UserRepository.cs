﻿using Azure.Core;
using Microsoft.EntityFrameworkCore;
using WebAPP.API.Data;
using WebAPP.API.Models.Domain;
using WebAPP.API.Repositories.Interface;

namespace WebAPP.API.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {

            this.dbContext = dbContext;
        }

        public async Task<User> CreateAsync(User user)
        {
            await dbContext.User.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await dbContext.User.ToListAsync();

        }

        public async Task<User?> GetUserByIdAsync(Guid Id)
        {
            return await dbContext.User.FirstOrDefaultAsync(user => user.Id == Id);
        }

        public async Task<User?> UpdateUserAsync(User request)
        {
            User? existingUser = await dbContext.User.FirstOrDefaultAsync(user => user.Id == request.Id);

            if (existingUser == null) return null;

            dbContext.Entry(existingUser).CurrentValues.SetValues(request);
            await dbContext.SaveChangesAsync();
            return existingUser;

        }

        public async Task<User?> DeleteUserAsync(Guid Id)
        {
            User? existingUser = await dbContext.User.FirstOrDefaultAsync(user => user.Id == Id);

            if (existingUser == null) return null;

            dbContext.Remove(existingUser);
            await dbContext.SaveChangesAsync();
            return existingUser;
        }
    }
}
