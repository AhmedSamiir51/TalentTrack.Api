using AutoMapper;
using TalentTrack.Application.Features.Applicants.DTOS;
using TalentTrack.Application.Features.JobTitles.DTOS;
using TalentTrack.Core.Entities;

namespace TalentTrack.Application.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Mapping for JobTitle
        CreateMap<JobTitle, JobTitleDto>()
            .ForMember(e => e.ValidFrom, opt => opt.MapFrom(src => src.ValidFrom.ToString("yyyy-MM-dd")))
            .ForMember(e => e.ValidTo, opt => opt.MapFrom(src => src.ValidTo.ToString("yyyy-MM-dd")))
            .ReverseMap();
        CreateMap<JobTitle, AddOrEditJobTitleDto>().ReverseMap();

        // Mapping for Applicant
        CreateMap<Applicant, ApplicantDto>()
            .ForMember(dest => dest.JobTitleName, opt => opt.MapFrom(src => src.JobTitle.Name));
        CreateMap<Applicant, AddOrEditApplicantDto>().ReverseMap();
    }
}
