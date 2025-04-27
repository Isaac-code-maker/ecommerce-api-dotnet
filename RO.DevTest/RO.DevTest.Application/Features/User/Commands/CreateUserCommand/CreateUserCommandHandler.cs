using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Enums; // Certifique-se de importar o namespace correto
using RO.DevTest.Domain.Exception;

namespace RO.DevTest.Application.Features.User.Commands.CreateUserCommand;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IIdentityAbstractor _identityAbstractor;

    public CreateUserCommandHandler(IIdentityAbstractor identityAbstractor)
    {
        _identityAbstractor = identityAbstractor;
    }

    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Validar o comando
        CreateUserCommandValidator validator = new();
        ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new BadRequestException(validationResult);
        }

        // Criar o usuário
        RO.DevTest.Domain.Entities.User newUser = request.AssignTo();
        IdentityResult userCreationResult = await _identityAbstractor.CreateUserAsync(newUser, request.Password);
        if (!userCreationResult.Succeeded)
        {
            throw new BadRequestException(userCreationResult);
        }

        // Converter Role para UserRoles
        if (!Enum.TryParse(request.Role, out UserRoles userRole))
        {
            throw new BadRequestException("Role inválido.");
        }

        // Adicionar o usuário a um role
        IdentityResult userRoleResult = await _identityAbstractor.AddToRoleAsync(newUser, userRole);
        if (!userRoleResult.Succeeded)
        {
            throw new BadRequestException(userRoleResult);
        }

        // Retornar o resultado
        return new CreateUserResult(newUser);
    }
}
