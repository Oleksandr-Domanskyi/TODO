using System;
using FluentValidation;
using TODO.Application.CQRS.Commands.ProjectTaskCreate;
using TODO.Core.Dto.ProjectTask;

namespace TODO.Application.CQRS.Commands.ProjectTask.ProjectTaskCreate
{
    public class ProjectTaskCreateCommandValidation : AbstractValidator<ProjectTaskCreateCommand>
    {
        public ProjectTaskCreateCommandValidation()
        {
            RuleFor(x => x.ProjectTaskCreateDTO)
                .NotNull().WithMessage("ProjectTaskCreateDTO cannot be null.");

            RuleFor(x => x.ProjectTaskCreateDTO.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(x => x.ProjectTaskCreateDTO.Description)
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.ProjectTaskCreateDTO.ExpiryDate)
                .Must(BeAValidDate).WithMessage("ExpiryDate must be in the future.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return date > DateTime.UtcNow;
        }
    }
}
