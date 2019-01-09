using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.DTO
{
    public class UsuarioParaRegistro
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(8,MinimumLength = 4,ErrorMessage ="Você deve fornecer uma senha válida de 4 a 8 caracteres")]
        public string Senha { get; set; }

        [Required]
        public string Genero { get; set; }

        [Required]
        public DateTime Nascimento { get; set; }

        [Required]
        public string ConhecidoComo { get; set; }

        [Required]
        public string  Cidade { get; set; }

        [Required]
        public string Pais { get; set; }

        public DateTime Criado { get; set; }

        public DateTime UltimaAtividade { get; set; }

        public UsuarioParaRegistro()
        {
            Criado = DateTime.Now;
            UltimaAtividade = DateTime.Now;
        }

    }
}
