using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DatingApp.API.Helpers;

namespace DatingApp.API.Data.Repository
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _dataContext;

        public DatingRepository(DataContext context)
        {
            _dataContext = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _dataContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _dataContext.Remove(entity);
        }

        public async Task<Photo> GetMainPhoto(int userId)
        {
            return await _dataContext.Photos.Where(x => x.UserId == userId)
                            .FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            return await _dataContext.Photos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<User> GetUser(int id)
        {            
            return await _dataContext.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// correto com o video]
        /// 
        /// </summary>
        /// <param name="userParams"></param>
        /// <returns></returns>
        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users =  _dataContext.Users.Include(p=>p.Photos)
                .OrderByDescending(u=>u.UltimaActividade).AsQueryable();

            users = users.Where(u => u.Id != userParams.UserId);

            users = users.Where(u => u.Genero == userParams.Genero);

            if(userParams.MinAge!= 18 || userParams.MaxAge != 99)
            {
                var minDob = DateTime.Today.AddYears(-userParams.MaxAge-1);
                var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(u => u.Nascimento >= minDob && u.Nascimento <= maxDob);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "Criado":
                        users = users.OrderByDescending(u => u.Criado);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.UltimaActividade);
                        break;
                }
            }


            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _dataContext.SaveChangesAsync()  > 0;
        }


    }
}
