using AutoMapper;
using Restoran.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Mapping_Profile
{
    public class MappingProfileSimpleDish : Profile
    {
        public MappingProfileSimpleDish() 
        {
            CreateMap<Dish, SimpleDish>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameDishes))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost))
                .ForMember(dest => dest.Cal, opt => opt.MapFrom(src => src.Calories))
                .ForMember(dest => dest.Amount, opt => opt.Ignore());
        }
         
    }
}
