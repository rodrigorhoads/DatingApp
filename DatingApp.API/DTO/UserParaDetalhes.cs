using DatingApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.DTO
{
    public class UserParaDetalhesDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Genero { get; set; }
        public int Idade { get; set; }
        public string ConhecidoComo { get; set; }
        public DateTime Criado { get; set; }
        public DateTime UltimaAtividade { get; set; }
        public string Introducao { get; set; }
        public string ProcurandoPor { get; set; }
        public string Interesses { get; set; }
        public string Cidade { get; set; }
        public string Pais { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotoForDetailDTO> Photos{ get;set; }
    }
}
