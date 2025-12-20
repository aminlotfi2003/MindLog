using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Languages.Commands.UpdateLanguage;

public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, Unit>
{
    private readonly IPersonalProfileRepository _profileRepo;
    private readonly ILanguageProficiencyRepository _languageRepo;
    private readonly IUnitOfWork _uow;

    public UpdateLanguageCommandHandler(
        IPersonalProfileRepository profileRepo,
        ILanguageProficiencyRepository languageRepo,
        IUnitOfWork uow)
    {
        _profileRepo = profileRepo;
        _languageRepo = languageRepo;
        _uow = uow;
    }

    public async Task<Unit> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
    {
        var course = await _languageRepo.GetByIdAsync(request.Id, cancellationToken);
        if (course is null)
            throw new NotFoundException($"Language with ID '{request.Id}' was not found.");

        var profileExists = await _profileRepo.AnyAsync(
            x => x.Id == request.ProfileId,
            cancellationToken
        );
        if (!profileExists)
            throw new NotFoundException($"Profile with Id '{request.ProfileId}' was not found.");

        var titleExists = await _languageRepo.AnyAsync(
            x => x.Language == request.Language,
            cancellationToken
        );
        if (titleExists)
            throw new ConflictException($"A language with name '{request.Language}' already exists.");

        await _languageRepo.Update(course, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
