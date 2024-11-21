using FluentValidation;
using Vehicle_service.Dto.Orders;

namespace Vehicle_service.FluentValidation.OrdersFluentValidation
{
    public class OrderUpdateDtoValidator : AbstractValidator<OrderUpdateDto>
    {
        public OrderUpdateDtoValidator()
        {
            RuleFor(x => x.Price)
                .NotNull().WithMessage("{PropertyName} cannot be null")
                .NotEmpty().WithMessage("Order must contain a price");
        }
    }
}
