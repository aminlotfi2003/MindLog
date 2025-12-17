using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.Infrastructure.Persistence.Contexts;

namespace MindLog.Infrastructure.Persistence.Repositories.Application;

public class WorkExperienceRepository : EfRepository<Guid, WorkExperience>, IWorkExperienceRepository
{
    public WorkExperienceRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
