using FluentValidation;
using Vehicle_service.Dto.Cars;

namespace Vehicle_service.FluentValidation.CarsFluentValidation
{
    public class CarUpdateDtoValidator : AbstractValidator<CarUpdateDto>
    {
        public CarUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("{PropertyName} cannot be null")
                .NotEmpty().WithMessage("Car must contain a name");

            RuleFor(x => x.Model)
                .NotNull().WithMessage("{PropertyName} cannot be null")
                .NotEmpty().WithMessage("Car must contain a model");
        }
    }
}

