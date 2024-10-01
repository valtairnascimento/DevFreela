using DevFreela.Application.Commands.UserCommands.InsertUser;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateUserValidator :AbstractValidator<InsertUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(u => u.Email).EmailAddress().WithMessage("Email Invalido");

            RuleFor(u => u.BirthDate).Must(d => d < DateTime.Now.AddYears(-18)).WithMessage("Deve ser maior de idade");
        }
    }
}
