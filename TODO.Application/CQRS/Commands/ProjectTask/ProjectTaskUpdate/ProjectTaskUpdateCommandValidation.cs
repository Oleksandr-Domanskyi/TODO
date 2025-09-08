using System;
using FluentValidation;
using TODO.Application.CQRS.Commands.ProjectTaskUpdate;
using TODO.Core.Dto.ProjectTask;

namespace TODO.Application.CQRS.Commands.ProjectTask.ProjectTaskUpdate
{
    public class ProjectTaskUpdateCommandValidation : AbstractValidator<ProjectTaskUpdateCommand>
    {
        public ProjectTaskUpdateCommandValidation()
        {
            RuleFor(x => x.ProjectTaskUpdateDTO)
                .NotNull().WithMessage("ProjectTaskUpdateDTO cannot be null.");

            RuleFor(x => x.ProjectTaskUpdateDTO.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(x => x.ProjectTaskUpdateDTO.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.ProjectTaskUpdateDTO.ExpiryDate)
                .Must(BeAValidDate).WithMessage("ExpiryDate must be in the future.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date > DateTime.UtcNow;
        }
    }
}
