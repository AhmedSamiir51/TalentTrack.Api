namespace TalentTrack.Application.Features.JobTitles.DTOS;

public class JobTitleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Responsibilities { get; set; }
    public string Skills { get; set; }
    public string JobCategory { get; set; }
    public string ValidFrom { get; set; }
    public string ValidTo { get; set; }
    public int MaxApplications { get; set; }
}
