using FluentValidation.TestHelper;
using ProductApplication.Domain.ComplexType;
using ProductApplication.Domain.Validations;
using Xunit;

namespace ProductApplication.Tests.Unit.Domain.Validations
{
    public class AddressValidationTests
    {
        private AddressValidations CreateAddressValidation()
        {
            return new AddressValidations();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Category", "Neighborhood")]
        public void MustValidate_Neighborhood_NullAndEmpty(string Neighborhood)
        {
            var addressValidation = CreateAddressValidation();
            addressValidation.ShouldHaveValidationErrorFor<AddressTest, string>(e => e.Neighborhood, Neighborhood).WithErrorMessage("Bairro não pode ser nulo.");
        }

        [Theory]
        [InlineData(251)]
        [InlineData(2)]
        [Trait("Category", "Neighborhood")]
        public void MustValidate_Neighborhood_Characters(int characters)
        {
            var addressValidation = CreateAddressValidation();
            string Neighborhood = new string('.', characters);
            addressValidation.ShouldHaveValidationErrorFor<AddressTest, string>(e => e.Neighborhood, Neighborhood).WithErrorMessage("Bairro deve conter entre 3 e 250 caracteres.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Category", "Number")]
        public void MustValidate_Number_NullAndEmpty(string Number)
        {
            var addressValidation = CreateAddressValidation();
            addressValidation.ShouldHaveValidationErrorFor<AddressTest, string>(e => e.Number, Number).WithErrorMessage("Número não pode ser nulo.");
        }

        [Fact]
        [Trait("Category", "Number")]
        public void MustValidate_Number_Characters()
        {
            var addressValidation = CreateAddressValidation();
            string number = new string('.', 51);
            addressValidation.ShouldHaveValidationErrorFor<AddressTest, string>(e => e.Number, number).WithErrorMessage("Número deve conter no máximo 50 caracteres!");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Category", "Uf")]
        public void MustValidate_Uf_NullAndEmpty(string Uf)
        {
            var addressValidation = CreateAddressValidation();
            addressValidation.ShouldHaveValidationErrorFor<AddressTest, string>(e => e.State, Uf).WithErrorMessage("UF não pode ser nulo");
        }

        [Theory]
        [InlineData(3)]
        [InlineData(1)]
        [Trait("Category", "Uf")]
        public void MustValidate_Uf_Characters(int characters)
        {
            var addressValidation = CreateAddressValidation();
            string Uf = new string('.', characters);
            addressValidation.ShouldHaveValidationErrorFor<AddressTest, string>(e => e.State, Uf).WithErrorMessage("O UF deve conter 2 caracteres!");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Category", "Street")]
        public void MustValidate_Street_NullAndEmpty(string Street)
        {
            var addressValidation = CreateAddressValidation();
            addressValidation.ShouldHaveValidationErrorFor<AddressTest, string>(e => e.Street, Street).WithErrorMessage("Rua não pode ser nulo.");
        }

        [Theory]
        [InlineData(251)]
        [InlineData(2)]
        [Trait("Category", "Street")]
        public void MustValidate_Street_Characters(int characters)
        {
            var addressValidation = CreateAddressValidation();
            string Street = new string('.', characters);
            addressValidation.ShouldHaveValidationErrorFor<AddressTest, string>(e => e.Street, Street).WithErrorMessage("Rua deve conter entre 3 e 250 caracteres.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Category", "City")]
        public void MustValidate_City_NullAndEmpty(string City)
        {
            var addressValidation = CreateAddressValidation();
            addressValidation.ShouldHaveValidationErrorFor<AddressTest, string>(e => e.City, City).WithErrorMessage("Cidade não pode ser nulo.");
        }

        [Theory]
        [InlineData(251)]
        [InlineData(2)]
        [Trait("Category", "City")]
        public void MustValidate_City_Characters(int characters)
        {
            var addressValidation = CreateAddressValidation();
            string City = new string('.', characters);
            addressValidation.ShouldHaveValidationErrorFor<AddressTest, string>(e => e.City, City).WithErrorMessage("Cidade deve conter entre 3 e 250 caracteres.");
        }

        [Fact]
        [Trait("Category", "Complement")]
        public void MustValidate_Complement_Characters()
        {
            var addressValidation = CreateAddressValidation();
            string Complement = new string('.', 251);
            addressValidation.ShouldHaveValidationErrorFor<AddressTest, string>(e => e.Complement, Complement).WithErrorMessage("O complemento deve conter no máximo 250 caracteres!");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Category", "CEP")]
        public void MustValidate_CEP_NullAndEmpty(string CEP)
        {
            var addressValidation = CreateAddressValidation();
            addressValidation.ShouldHaveValidationErrorFor<AddressTest, string>(e => e.ZipCode, CEP).WithErrorMessage("CEP não pode ser nulo.");
        }

        [Theory]
        [InlineData(12)]
        [InlineData(10)]
        [Trait("Category", "CEP")]
        public void MustValidate_CEP_characters(int characters)
        {
            var addressValidation = CreateAddressValidation();
            string CEP = new string('.', characters);
            addressValidation.ShouldHaveValidationErrorFor<AddressTest, string>(e => e.ZipCode, CEP).WithErrorMessage("CEP deve conter 11 caracteres.");
        }
    }

    internal class AddressTest : Address
    {
        public AddressTest()
        {
        }
    }
}
