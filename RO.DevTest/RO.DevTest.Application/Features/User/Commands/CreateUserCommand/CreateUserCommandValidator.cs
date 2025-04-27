using FluentValidation;
using RO.DevTest.Application.Features.User.Commands.CreateUserCommand; // Adicione esta linha

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("O nome de usuário é obrigatório.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("O e-mail é obrigatório e deve ser válido.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("A senha é obrigatória.");
        RuleFor(x => x.PasswordConfirmation)
            .Equal(x => x.Password)
            .WithMessage("A confirmação de senha deve corresponder à senha.");
        RuleFor(x => x.Role).NotEmpty().WithMessage("O role é obrigatório.");
    }
}
