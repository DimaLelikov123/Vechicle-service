using AutoMapper;
using Vehicle_service.Dto.Cars;
using Vehicle_service.Models;

namespace Vehicle_service.Profiles
{
  public class CarsProfile : Profile
  {
    public CarsProfile()
    {
      CreateMap<Car, CarReadDto>();
      CreateMap<Car, CarDto>();
      CreateMap<CarCreateDto, Car>()
        .ForMember(dest => dest.Id, opt => opt.Ignore());
      CreateMap<CarUpdateDto, Car>();
      CreateMap<Car, CarUpdateDto>();
    }
  }
}

//A DTO, or Data Transfer Object, is a design pattern used in software development to transfer data between different layers of an application or between applications. DTOs are simple objects that do not contain any business logic; they are used solely to carry data.
// 
// Purpose of DTOs:
// 
// 	•	Decoupling: DTOs help decouple your domain models (entities) from the data exposed to clients, enhancing flexibility and security.
// 	•	Controlled Exposure: By using DTOs, you can control exactly what data is sent over the network, preventing sensitive information from being exposed.
// 	•	Validation and Formatting: DTOs can include validation attributes and can be tailored to the needs of the API consumers.
// 	•	Performance: They can improve performance by transferring only the necessary data, reducing payload size.