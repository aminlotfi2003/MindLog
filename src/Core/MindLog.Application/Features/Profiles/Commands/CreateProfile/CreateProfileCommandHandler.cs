using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.Domain.Entities;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Profiles.Commands.CreateProfile;

public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, Guid>
{
    private readonly IPersonalProfileRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateProfileCommandHandler(IPersonalProfileRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Guid> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        var profileExists = await _repo.AnyAsync(
            x => x.FullName == request.FullName,
            cancellationToken
        );
        if (profileExists)
            throw new ConflictException($"A profile with full name '{request.FullName}' already exists.");

        var profile = PersonalProfile.Create(
            request.FullName,
            request.Summary,
            request.BirthDate,
            request.Email,
            request.PhoneNumber,
            request.Website,
            request.LinkedInUrl,
            request.GitHubUrl,
            request.Address
        );

        await _repo.AddAsync(profile, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);
        return profile.Id;
    }
}
