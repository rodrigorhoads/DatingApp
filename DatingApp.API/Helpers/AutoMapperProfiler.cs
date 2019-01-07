using AutoMapper;
using DatingApp.API.Controllers;
using DatingApp.API.DTO;
using DatingApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiler:Profile
    {
        public AutoMapperProfiler()
        {
            CreateMap<User, UserParaListarDTO>()
                .ForMember(dest => dest.PhotoUrl,
                opt => { opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);})
                .ForMember(dest => dest.Idade,opt=>{
                    opt.ResolveUsing(d=>d.Nascimento.CalcularIdade());
                });

            CreateMap<User, UserParaDetalhesDTO>()
                                .ForMember(dest => dest.PhotoUrl,
                opt => { opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url); })
                                .ForMember(dest => dest.Idade,opt=>{
                    opt.ResolveUsing(d=>d.Nascimento.CalcularIdade());
                });; 

            CreateMap<Photo, PhotoForDetailDTO>();
            CreateMap<UserParaUpdatesDTO, User>();
            CreateMap<Photo, PhotoForReturnDTO>();
            CreateMap<PhotoForCreationDTO, Photo>();
        }
    }
}
