using FluentValidation;
using TalentTrack.Application.Features.JobTitles.Commands;

namespace TalentTrack.Application.Features.JobTitles.Validation;

public class AddOrEditJobTitlesCommandValidation : AbstractValidator<AddOrEditJobTitlesCommand>
{

    public AddOrEditJobTitlesCommandValidation()
    {
        ApplyValidationsRules();

    }
    public void ApplyValidationsRules()
    {
        RuleFor(x => x.JobTitlesDto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.JobTitlesDto.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");

        RuleFor(x => x.JobTitlesDto.Responsibilities)
            .MaximumLength(1000).WithMessage("Responsibilities must not exceed 1000 characters.");

        RuleFor(x => x.JobTitlesDto.Skills)
            .MaximumLength(500).WithMessage("Skills must not exceed 500 characters.");

        RuleFor(x => x.JobTitlesDto.JobCategory)
            .NotEmpty().WithMessage("Job Category is required.")
            .MaximumLength(100).WithMessage("Job Category must not exceed 100 characters.");

        RuleFor(x => x.JobTitlesDto.ValidFrom)
            .NotEmpty().WithMessage("Valid From date is required.");

        RuleFor(x => x.JobTitlesDto.ValidTo)
            .NotEmpty().WithMessage("Valid To date is required.")
            .GreaterThan(x => x.JobTitlesDto.ValidFrom).WithMessage("Valid To date must be after Valid From date.");

        RuleFor(x => x.JobTitlesDto.MaxApplications)
            .GreaterThan(0).WithMessage("Max Applications must be greater than zero.");
    }


}
