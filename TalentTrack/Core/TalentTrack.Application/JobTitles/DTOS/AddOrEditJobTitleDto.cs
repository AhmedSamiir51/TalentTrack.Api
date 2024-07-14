using System.ComponentModel.DataAnnotations;

namespace TalentTrack.Application.Features.JobTitles.DTOS;

public class AddOrEditJobTitleDto
{
    public int? Id { get; set; }
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
}
