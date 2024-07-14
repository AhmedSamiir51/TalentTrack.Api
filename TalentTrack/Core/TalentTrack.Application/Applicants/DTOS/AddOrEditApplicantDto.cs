using System.ComponentModel.DataAnnotations;

namespace TalentTrack.Application.Features.Applicants.DTOS;

public class AddOrEditApplicantDto
{
    public int? Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string MobileNumber { get; set; }

    [Required]
    public int JobTitleId { get; set; }
}
