using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.Infrastructure.Persistence.Contexts;

namespace MindLog.Infrastructure.Persistence.Repositories.Application;

public class EducationRecordRepository : EfRepository<Guid, EducationRecord>, IEducationRecordRepository
{
    public EducationRecordRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
