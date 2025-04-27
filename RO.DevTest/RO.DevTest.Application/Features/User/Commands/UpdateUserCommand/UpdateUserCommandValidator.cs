using FluentValidation;

namespace RO.DevTest.Application.Features.User.Commands.UpdateUserCommand;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("O ID do usuário é obrigatório.");
        RuleFor(x => x.Username).MaximumLength(50).WithMessage("O nome de usuário não pode ter mais de 50 caracteres.");
        RuleFor(x => x.Email).EmailAddress().WithMessage("O e-mail deve ser válido.");
        RuleFor(x => x.Role)
            .Must(role => role == "Admin" || role == "User")
            .WithMessage("O papel do usuário deve ser 'Admin' ou 'User'.");
    }
}