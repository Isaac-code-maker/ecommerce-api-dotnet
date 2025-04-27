using MediatR;
using RO.DevTest.Application.Contracts.Persistence.Repositories;
using RO.DevTest.Application.Contracts.Persistence; // Add the correct namespace for IUserRepository
using RO.DevTest.Domain.Entities;

namespace RO.DevTest.Application.Features.User.Commands.UpdateUserCommand;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user == null)
        {
            return false;
        }

        // Atualizar os campos do usuário
        if (!string.IsNullOrEmpty(request.Username))
        {
            user.UserName = request.Username;
        }

        if (!string.IsNullOrEmpty(request.Email))
        {
            user.Email = request.Email;
        }

        if (!string.IsNullOrEmpty(request.Role))
        {
            user.Role = request.Role;
        }

        await _userRepository.UpdateAsync(user);
        return true;
    }
}