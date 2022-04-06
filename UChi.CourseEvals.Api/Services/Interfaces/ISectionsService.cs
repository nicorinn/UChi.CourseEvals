using UChi.CourseEvals.Api.Models;
using UChi.CourseEvals.Domain.Entities;

namespace UChi.CourseEvals.Api.Services.Interfaces;

public interface ISectionsService
{
    public Task<SectionModel?> GetSectionById(int id);

    public Task<Section?> AddSection(NewSectionModel sectionModel);
}