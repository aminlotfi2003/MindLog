using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.Infrastructure.Persistence.Contexts;

namespace MindLog.Infrastructure.Persistence.Repositories.Application;

public class TrainingCourseRepository : EfRepository<Guid, TrainingCourse>, ITrainingCourseRepository
{
    public TrainingCourseRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
