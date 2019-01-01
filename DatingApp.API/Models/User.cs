namespace DatingApp.API.Models
{
    public class User
    {
        public int Id {get;set;}
        public string Nome{get;set;}
        public byte[] SenhaHash{get;set;}
        public byte[] SenhaSalt{get;set;}
    }
}