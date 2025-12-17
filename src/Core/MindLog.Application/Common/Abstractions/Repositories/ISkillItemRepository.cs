using MindLog.Domain.Entities;
using MindLog.SharedKernel.Abstractions;

namespace MindLog.Application.Common.Abstractions.Repositories;

public interface ISkillItemRepository : IRepository<Guid, SkillItem>
{
}
