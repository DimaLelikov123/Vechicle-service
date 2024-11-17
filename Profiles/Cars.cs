using AutoMapper;
using Vehicle_service.Dto.Cars;
using Vehicle_service.Models;

namespace Vehicle_service.Profiles
{
  public class CarsProfile : Profile
  {
    public CarsProfile()
    {
      CreateMap<Car, CarDto>();
      CreateMap<CarCreateDto, Car>()
        .ForMember(dest => dest.Id, opt => opt.Ignore());
      CreateMap<CarUpdateDto, Car>();
      CreateMap<Car, CarUpdateDto>();
    }
  }
}