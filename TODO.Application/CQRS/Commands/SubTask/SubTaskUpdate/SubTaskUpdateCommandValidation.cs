using System;
using FluentValidation;
using TODO.Core.Dto;

namespace TODO.Application.CQRS.Commands.SubTask.SubTaskUpdate
{
    public class SubTaskUpdateCommandValidation : AbstractValidator<SubTaskUpdateCommand>
    {
        public SubTaskUpdateCommandValidation()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("SubTask Id cannot be empty.");

            RuleFor(x => x.SubTask)
                .NotNull().WithMessage("SubTask DTO cannot be null.");

            RuleFor(x => x.SubTask.Title)
                .NotEmpty().WithMessage("SubTask title is required.")
                .MaximumLength(200).WithMessage("SubTask title cannot exceed 200 characters.");

            RuleFor(x => x.SubTask.Description)
                .MaximumLength(1000).WithMessage("SubTask description cannot exceed 1000 characters.");

            RuleFor(x => x.SubTask.ExpiryDate)
                .Must(BeAValidDate).WithMessage("ExpiryDate must be in the future.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date > DateTime.UtcNow;
        }
    }
}
