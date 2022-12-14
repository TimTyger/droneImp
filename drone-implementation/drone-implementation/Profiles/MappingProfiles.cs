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
            CreateMap<Drone, DroneResp>().ReverseMap();
            CreateMap<MedicationResp, Medication>().ReverseMap();
            CreateMap<DroneResp, Drone>().ReverseMap();
        }
    }
}
