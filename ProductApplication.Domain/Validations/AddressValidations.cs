using FluentValidation;
using ProductApplication.Domain.ComplexType;

namespace ProductApplication.Domain.Validations
{
    public class AddressValidations : AbstractValidator<Address>
    {
        public AddressValidations()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(f => f.Neighborhood)
                .NotEmpty().WithMessage("Bairro não pode ser nulo.")
                .Length(3, 250).WithMessage("Bairro deve conter entre 3 e 250 caracteres.");

            RuleFor(f => f.ZipCode)
                .NotEmpty().WithMessage("CEP não pode ser nulo.")
                .Length(11).WithMessage("CEP deve conter 11 caracteres.");

            RuleFor(f => f.City)
                .NotEmpty().WithMessage("Cidade não pode ser nulo.")
                .Length(3, 250).WithMessage("Cidade deve conter entre 3 e 250 caracteres.");

            RuleFor(f => f.Complement)
                .MaximumLength(250).WithMessage("O complemento deve conter no máximo 250 caracteres!");

            RuleFor(f => f.Number)
                .NotEmpty().WithMessage("Número não pode ser nulo.")
                .MaximumLength(50).WithMessage("Número deve conter no máximo 50 caracteres!");

            RuleFor(f => f.Street)
                .NotEmpty().WithMessage("Rua não pode ser nulo.")
                .Length(3, 250).WithMessage("Rua deve conter entre 3 e 250 caracteres.");

            RuleFor(f => f.State)
                .NotEmpty().WithMessage("UF não pode ser nulo")
                .Length(2).WithMessage("O UF deve conter 2 caracteres!");
        }
    }
}
