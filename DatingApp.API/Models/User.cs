using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.API.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order =0)]
        public int Id {get;set;}
        public string Nome{get;set;}
        public byte[] SenhaHash{get;set;}
        public byte[] SenhaSalt{get;set;}
        public string Genero { get; set; }
        public DateTime Nascimento { get; set; }
        public string ConhecidoComo { get; set; }
        public DateTime Criado { get; set; }
        public DateTime UltimaAtividade { get; set; }
        public string Introducao { get; set; }
        public string ProcurandoPor { get; set; }
        public string Interesses { get; set; }
        public string Cidade { get; set; }
        public string Pais { get; set; }
        public ICollection<Photo> Photos { get; set; }

        public ICollection<Like> Likers { get; set; }
        public ICollection<Like> Likees { get; set; }
    }
}