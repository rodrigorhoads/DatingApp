using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.DTO
{
    public class UserParaListarDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public string Genero { get; set; }
        public int Idade { get; set; }
        public string ConhecidoComo { get; set; }
        public DateTime Criado { get; set; }
        public DateTime UltimaAtividade { get; set; }

        public string Cidade { get; set; }
        public string Pais { get; set; }
        public string PhotoUrl { get; set; }
    }
}
