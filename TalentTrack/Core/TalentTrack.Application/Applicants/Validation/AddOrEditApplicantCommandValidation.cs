using FluentValidation;
using TalentTrack.Application.Features.Applicants.Commands;

namespace TalentTrack.Application.Features.Applicants.Validation;

public class AddOrEditApplicantCommandValidation : AbstractValidator<AddOrEditApplicantCommand>
{

    public AddOrEditApplicantCommandValidation()
    {
        ApplyValidationsRules();

    }
    public void ApplyValidationsRules()
    {
        RuleFor(x => x.ApplicantDto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.ApplicantDto.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");

        RuleFor(x => x.ApplicantDto.MobileNumber)
            .NotEmpty().WithMessage("Mobile number is required.")
            .Matches(@"^\d{10}$").WithMessage("Mobile number must be a valid 10-digit number.");

        RuleFor(x => x.ApplicantDto.JobTitleId)
            .GreaterThan(0).WithMessage("Job Title ID must be greater than zero.");
    }


}
