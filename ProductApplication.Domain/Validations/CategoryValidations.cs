using FluentValidation;
using ProductApplication.Domain.Entities;

namespace ProductApplication.Domain.Validations
{
    public class CategoryValidations : AbstractValidator<Category>
    {
        public CategoryValidations()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O Nome não pode ser nulo")
                .Length(3, 250).WithMessage("O Nome deve conter entre 3 e 250 caracteres.");

            RuleFor(x => x.IdSupplier)
                .NotEmpty().WithMessage("O id do fornecedor não pode ser nulo")
                .ExclusiveBetween(0, int.MaxValue).WithMessage("O id do fornecedor deve ser maior que 0");
        }
    }
}
