namespace TalentTrack.Application.Features.Applicants.DTOS;

public class ApplicantDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string MobileNumber { get; set; }
    public int JobTitleId { get; set; }
    public string JobTitleName { get; set; }

}
