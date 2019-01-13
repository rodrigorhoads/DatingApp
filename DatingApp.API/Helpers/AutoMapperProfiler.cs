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
            CreateMap<UsuarioParaRegistro, User>()
                .ForMember(dest => dest.Nome, opt => { opt.ResolveUsing(u => u.Username); });

            CreateMap<MessageForCreationDTO, Message>().ReverseMap();

            CreateMap<Message, MessageToReturnDTO>()
                    .ForMember(m => m.SenderPhotoUrl, opt =>
                              opt.MapFrom(m => m.Sender.Photos.FirstOrDefault(p => p.IsMain).Url))
                    .ForMember(m => m.RecipientPhotoUrl, opt =>
                            opt.MapFrom(m => m.Recipient.Photos.FirstOrDefault(p => p.IsMain).Url));
            }
    }
}
