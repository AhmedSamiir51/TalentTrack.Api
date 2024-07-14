using System.ComponentModel.DataAnnotations;
using TalentTrack.Core.Interfaces;

namespace TalentTrack.Core.Entities;

public class Applicant : BaseEntity, IMustHaveDelete
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string MobileNumber { get; set; }

    public int JobTitleId { get; set; }
    public JobTitle JobTitle { get; set; }

    public bool IsDeleted { get; set; } = false;
}