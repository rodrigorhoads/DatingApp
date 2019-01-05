using DatingApp.API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;    
        }

        public void SeedUsers()
        {
            var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");

            var usuarios = JsonConvert.DeserializeObject<List<User>>(userData);

            foreach(var usuario in usuarios)
            {
                byte[] passwordHash, passwordsalt;
                CreatePassworHash("senha", out passwordHash, out passwordsalt);

                usuario.SenhaHash = passwordHash;
                usuario.SenhaSalt = passwordsalt;
                usuario.Nome = usuario.Nome.ToLower();

                _context.Users.Add(usuario);
            }
            _context.SaveChanges();

        }

        private void CreatePassworHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
    }
}
