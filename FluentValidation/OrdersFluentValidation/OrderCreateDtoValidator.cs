using FluentValidation;
using Vehicle_service.Dto.Orders;
using Vehicle_service.Repositories;

namespace Vehicle_service.FluentValidation.OrdersFluentValidation
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .LessThanOrEqualTo(10000)
                .WithMessage("Amount must be between 0 and 10000.");

            RuleFor(x => x.CarId)
                .GreaterThan(0)
                .WithMessage("Car ID must be positive.");
        }
    }
}