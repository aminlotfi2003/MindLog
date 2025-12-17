using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.Infrastructure.Persistence.Contexts;

namespace MindLog.Infrastructure.Persistence.Repositories.Application;

public class SkillItemRepository : EfRepository<Guid, SkillItem>, ISkillItemRepository
{
    public SkillItemRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
