using drone_Domain.Dtos;
using drone_Domain.Entities;
using AutoMapper;

namespace drone_implementation.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Drone, RegisterDroneDto>().ReverseMap();
        }
    }
}
