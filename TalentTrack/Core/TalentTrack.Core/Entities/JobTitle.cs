using System.ComponentModel.DataAnnotations;
using TalentTrack.Core.Interfaces;

namespace TalentTrack.Core.Entities;
public class JobTitle : BaseEntity, IMustHaveDelete
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(1000)]
    public string Description { get; set; }

    [StringLength(1000)]
    public string Responsibilities { get; set; }

    [StringLength(500)]
    public string Skills { get; set; }

    [Required]
    [StringLength(100)]
    public string JobCategory { get; set; }

    [Required]
    public DateTime ValidFrom { get; set; }

    [Required]
    public DateTime ValidTo { get; set; }

    [Range(1, int.MaxValue)]
    public int MaxApplications { get; set; }

    public List<Applicant> Applicants { get; set; }

    public bool IsDeleted { get; set; } = false;
}
