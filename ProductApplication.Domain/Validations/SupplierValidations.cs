using FluentValidation;
using ProductApplication.Domain.Entities;
using System.Text.RegularExpressions;

namespace ProductApplication.Domain.Validations
{
    public class SupplierValidations : AbstractValidator<Supplier>
    {
        public SupplierValidations()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(f => f.CompanyName)
                .NotEmpty().WithMessage("Razão Social não pode ser nula")
                .Length(3, 250).WithMessage("Razão Social deve conter entre 3 e 250 caracteres.");

            RuleFor(f => f.CNPJ)
                .NotEmpty().WithMessage("CNPJ não pode ser nulo")
                .Must(IsCnpj).WithMessage("O CNPJ é inválido ou possui caracteres incorretos!");

            RuleFor(f => f.Trade)
                .NotEmpty().WithMessage("Nome Fantasia não pode ser nula")
                .Length(3, 250).WithMessage("Nome Fantasia deve conter entre 3 e 250 caracteres.");

            RuleFor(f => f.Address)
                .SetValidator(new AddressValidations());

            RuleFor(f => f.ContactEmail)
                .NotEmpty().WithMessage("Email não pode ser nulo")
                .Length(3, 250).WithMessage("Email deve conter entre 3 e 250 caracteres.")
                .EmailAddress().WithMessage("Email inváldo!");

            RuleFor(f => f.Telephone)
                .NotEmpty().WithMessage("O telefone não pode ser nulo")
                .Must(IsValidPhoneNumber).WithMessage("O telefone é inválido ou possui caracteres incorretos!");
        }

        private static bool IsCnpj(string cnpj)
        {
            return CnpjValidator.Validate(cnpj);
        }

        private static bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.Match(phoneNumber,
                @"^(\(?[0-9]{2}\)?)\s?[0-9]{4,5}-?[0-9]{4}$").Success;
        }
    }
}
