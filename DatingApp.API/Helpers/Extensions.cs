

using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.API.Helpers
{
    public static class Extensions
    {
        public static void AddAplicationError(this HttpResponse response, string mensagem)
        {
            response.Headers.Add("Application-Error", mensagem);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static int CalcularIdade(this DateTime dateTime){
            var age = DateTime.Today.Year - dateTime.Year;

            if(dateTime.AddYears(age)>DateTime.Today){
                age--;
            }

            return age;
        }
    }
}