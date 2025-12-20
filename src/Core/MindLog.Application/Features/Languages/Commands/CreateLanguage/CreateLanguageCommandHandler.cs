using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Languages.Commands.CreateLanguage;

public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, Guid>
{
    private readonly IPersonalProfileRepository _profileRepo;
    private readonly ILanguageProficiencyRepository _languageRepo;
    private readonly IUnitOfWork _uow;

    public CreateLanguageCommandHandler(
        IPersonalProfileRepository profileRepo,
        ILanguageProficiencyRepository languageRepo,
        IUnitOfWork uow)
    {
        _profileRepo = profileRepo;
        _languageRepo = languageRepo;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
    {
        var profileExists = await _profileRepo.AnyAsync(
            x => x.Id == request.ProfileId,
            cancellationToken
        );
        if (!profileExists)
            throw new NotFoundException($"Profile with ID '{request.ProfileId}' was not found.");

        var titleExists = await _languageRepo.AnyAsync(
            x => x.Language == request.Language,
            cancellationToken
        );
        if (titleExists)
            throw new ConflictException($"A Language with name '{request.Language}' already exists.");

        var course = LanguageProficiency.Create(
            request.ProfileId,
            request.Language,
            request.Level,
            request.Certificate,
            request.SortOrder
        );

        await _languageRepo.AddAsync(course, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return course.Id;
    }
}
