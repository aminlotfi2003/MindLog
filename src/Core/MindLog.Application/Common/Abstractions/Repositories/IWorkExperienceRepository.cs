using MindLog.Domain.Entities;
using MindLog.SharedKernel.Abstractions;

namespace MindLog.Application.Common.Abstractions.Repositories;

public interface IWorkExperienceRepository : IRepository<Guid, WorkExperience>
{
}
