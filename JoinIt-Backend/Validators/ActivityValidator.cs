using FluentValidation;
using JoinIt_Backend.Models;

namespace JoinIt_Backend.Validators
{
    public class ActivityValidator : AbstractValidator<Activity>
    {
        public ActivityValidator()
        {
            RuleFor(activity => activity.Description).MinimumLength(5).MaximumLength(255);
            RuleFor(activity => activity.Name).MinimumLength(5).MaximumLength(75);
        }
    }
}
