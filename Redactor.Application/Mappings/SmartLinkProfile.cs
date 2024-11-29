using AutoMapper;
using Newtonsoft.Json;
using Redactor.Application.DTO;
using Redactor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Redactor.Application.Profiles
{
    public class SmartLinkProfile : Profile
    {
        public SmartLinkProfile()
        {
            CreateMap<LinkRequest, Smartlinks>()
                .ForMember(dest => dest.Rules, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Rules))) // Сериализация объекта в строку JSON
                .ReverseMap()
                .ForMember(dest => dest.Rules, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<IEnumerable<RedirectRule>>(src.Rules))); // Десериализация строки JSON в объект

            CreateMap<Smartlinks, LinkResponse>()
                .ForMember(dest => dest.Rules, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<IEnumerable<RedirectRule>>(src.Rules))) // Десериализация строки JSON в объект
                .ReverseMap()
                .ForMember(dest => dest.Rules, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Rules))); // Сериализация объекта в строку JSON
        }
    }
}
