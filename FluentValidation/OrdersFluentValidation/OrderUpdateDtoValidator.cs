using FluentValidation;
using Vehicle_service.Dto.Orders;

namespace Vehicle_service.FluentValidation.OrdersFluentValidation
{
    public class OrderUpdateDtoValidator : AbstractValidator<OrderUpdateDto>
    {
        public OrderUpdateDtoValidator()
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