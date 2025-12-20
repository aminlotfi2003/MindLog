using MediatR;
using MindLog.Application.Common.Abstractions;
using MindLog.Application.Common.Abstractions.Repositories;
using MindLog.SharedKernel.Exceptions;

namespace MindLog.Application.Features.Profiles.Commands.UpdateProfile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Unit>
{
    private readonly IPersonalProfileRepository _repo;
    private readonly IUnitOfWork _uow;

    public UpdateProfileCommandHandler(IPersonalProfileRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<Unit> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var profile = await _repo.GetByIdAsync(request.Id,cancellationToken);
        if (profile is null)
            throw new NotFoundException($"Profile with ID '{request.Id}' was not found.");

        var fullNameExists = await _repo.AnyAsync(
            x => x.FullName == request.FullName,
            cancellationToken
        );
        if (fullNameExists)
            throw new ConflictException($"A profile with full name '{request.FullName}' already exists.");

        await _repo.Update(profile, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
