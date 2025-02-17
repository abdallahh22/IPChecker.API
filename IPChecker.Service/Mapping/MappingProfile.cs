using AutoMapper;
using IPChecker.Data.Entities;
using IPChecker.Service.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPChecker.Service.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BlockedCountry, BlockedCountryDto>().ReverseMap();
            CreateMap<BlockCountryRequestDto, BlockedCountry>();
            CreateMap<BlockedAttempt, BlockedAttemptDto>().ReverseMap();
        }
    }
}
