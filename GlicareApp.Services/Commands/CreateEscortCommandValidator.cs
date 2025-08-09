using FluentValidation;

namespace GlicareApp.Services.Commands;

public class CreateEscortCommandValidator :AbstractValidator<CreateEscortCommand>
{
    public CreateEscortCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email válido é obrigatório.");
        RuleFor(x => x.PacientId).NotEmpty().WithMessage("ID do paciente é obrigatório.");
        
        RuleFor(x => x.LoginType)
            .NotEmpty()
            .Must(x => x == "GOOGLE" || x == "ICLOUD" || x == "GLICARE")
            .WithMessage("Tipo de login inválido.");

        RuleFor(x => x.TokenAuth)
            .NotEmpty()
            .WithMessage("Token de autenticação é obrigatório.");

        RuleFor(x => x.UseTerms)
            .Equal(true)
            .WithMessage("Termos de uso devem ser aceitos.");
    }
}