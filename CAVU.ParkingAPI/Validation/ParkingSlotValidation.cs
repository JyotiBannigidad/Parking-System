using CAVU.ParkingAPI.RequestDto;
using FluentValidation;

namespace CAVU.ParkingAPI.Validation
{
    public class ParkingSlotValidation:AbstractValidator<GetAvilablilityRequestDto>
    {
        public ParkingSlotValidation()
        {
            RuleFor(x => x.StartDate).NotEmpty();
            RuleFor(x => x.EndDate).NotEmpty();
        }
    }
}
