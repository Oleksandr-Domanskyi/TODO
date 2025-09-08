using System;
using FluentValidation;
using TODO.Core.Dto;

namespace TODO.Application.CQRS.Commands.SubTask.SubTaskCreate
{
    public class SubTaskCreateCommandValidation : AbstractValidator<SubTaskCreateCommand>
    {
        public SubTaskCreateCommandValidation()
        {
            RuleFor(x => x.SubTask)
                .NotNull().WithMessage("SubTaskCreateDTO cannot be null.");

            RuleFor(x => x.SubTask.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(x => x.SubTask.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.SubTask.ExpiryDate)
                .Must(BeAValidDate).WithMessage("ExpiryDate must be in the future.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date > DateTime.UtcNow;
        }
    }
}
