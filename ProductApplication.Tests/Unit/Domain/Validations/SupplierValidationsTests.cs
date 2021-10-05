using FluentValidation.TestHelper;
using ProductApplication.Domain.Entities;
using ProductApplication.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProductApplication.Tests.Unit.Domain.Validations
{
    public class SupplierValidationTests
    {
        private SupplierValidations CreateSupplierValidation()
        {
            return new SupplierValidations();
            {
            };
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Category", "CompanyName")]
        public void Valide_CompanyName_Null_And_Empty(string companyName)
        {
            var supplierValidation = CreateSupplierValidation();
            supplierValidation.ShouldHaveValidationErrorFor<SupplierTest, string>(f => f.CompanyName, companyName).WithErrorMessage("Razão Social não pode ser nula");
        }

        [Theory]
        [InlineData(251)]
        [InlineData(2)]
        [Trait("Category", "CompanyName")]
        public void Valide_CompanyName_Characters(int characters)
        {
            var supplierValidation = CreateSupplierValidation();
            string companyName = new string('.', characters);
            supplierValidation.ShouldHaveValidationErrorFor<SupplierTest, string>(f => f.CompanyName, companyName).WithErrorMessage("Razão Social deve conter entre 3 e 250 caracteres.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Category", "CNPJ")]
        public void Valide_CNPJ_Null_And_Empty(string CNPJ)
        {
            var supplierValidation = CreateSupplierValidation();
            supplierValidation.ShouldHaveValidationErrorFor<SupplierTest, string>(f => f.CNPJ, CNPJ).WithErrorMessage("CNPJ não pode ser nulo");
        }

        [Theory]
        [InlineData("..")]
        [InlineData("................................................")]
        [InlineData("323454982783")]
        [InlineData("1235422.345")]
        [Trait("Category", "CNPJ")]
        public void Valide_CNPJ_Invalid(string CNPJ)
        {
            var supplierValidation = CreateSupplierValidation();
            supplierValidation.ShouldHaveValidationErrorFor<SupplierTest, string>(f => f.CNPJ, CNPJ).WithErrorMessage("O CNPJ é inválido ou possui caracteres incorretos!");
        }

        [Theory]
        [InlineData("07.526.557/0001-00")]
        [InlineData("07526557/0001-00")]
        [InlineData("07526557000100")]
        [Trait("Category", "CNPJ")]
        public void Valide_CNPJ_Valid(string CNPJ)
        {
            var supplierValidation = CreateSupplierValidation();
            supplierValidation.ShouldNotHaveValidationErrorFor<SupplierTest, string>(f => f.CNPJ, CNPJ);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Category", "Trade")]
        public void Valide_Trade_Null_And_Empty(string trade)
        {
            var supplierValidation = CreateSupplierValidation();
            supplierValidation.ShouldHaveValidationErrorFor<SupplierTest, string>(f => f.Trade, trade).WithErrorMessage("Nome Fantasia não pode ser nula");
        }

        [Theory]
        [InlineData(251)]
        [InlineData(2)]
        [Trait("Category", "Trade")]
        public void Valide_Trade_Characters(int characters)
        {
            var supplierValidation = CreateSupplierValidation();
            string trade = new string('.', characters);
            supplierValidation.ShouldHaveValidationErrorFor<SupplierTest, string>(f => f.Trade, trade).WithErrorMessage("Nome Fantasia deve conter entre 3 e 250 caracteres.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Category", "ContactEmail")]
        public void Valide_ContactEmail_Null_And_Empty(string contactEmail)
        {
            var supplierValidation = CreateSupplierValidation();
            supplierValidation.ShouldHaveValidationErrorFor<SupplierTest, string>(f => f.ContactEmail, contactEmail).WithErrorMessage("Email não pode ser nulo");
        }

        [Theory]
        [InlineData(251)]
        [InlineData(2)]
        [Trait("Category", "ContactEmail")]
        public void Valide_ContactEmail_Characters(int characters)
        {
            var supplierValidation = CreateSupplierValidation();
            string contactEmail = new string('.', characters);
            supplierValidation.ShouldHaveValidationErrorFor<SupplierTest, string>(f => f.ContactEmail, contactEmail).WithErrorMessage("Email deve conter entre 3 e 250 caracteres.");
        }

        [Theory]
        [InlineData("exemplogmail.com")]
        [InlineData("@..")]
        [Trait("Category", "ContactEmail")]
        public void Valide_ContactEmail_Invalid(string contactEmail)
        {
            var supplierValidation = CreateSupplierValidation();
            supplierValidation.ShouldHaveValidationErrorFor<SupplierTest, string>(f => f.ContactEmail, contactEmail).WithErrorMessage("Email inváldo!");

        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Category", "Telephone")]
        public void Valide_Telephone_Null_And_Empty(string telephone)
        {
            var supplierValidation = CreateSupplierValidation();
            supplierValidation.ShouldHaveValidationErrorFor<SupplierTest, string>(f => f.Telephone, telephone).WithErrorMessage("O telefone não pode ser nulo");
        }

        [Theory]
        [InlineData("4665,1..54.947")]
        [InlineData("(4345)99774--5524")]
        [Trait("Category", "Telephone")]
        public void Valide_Telephone_Invalid(string telephone)
        {
            var supplierValidation = CreateSupplierValidation();
            supplierValidation.ShouldHaveValidationErrorFor<SupplierTest, string>(f => f.Telephone, telephone).WithErrorMessage("O telefone é inválido ou possui caracteres incorretos!");
        }
        [Theory]
        [InlineData("(47)99772-5524")]
        [InlineData("47997725524")]
        [Trait("Category", "Telephone")]
        public void Valide_Telephone_Valid(string telephone)
        {
            var supplierValidation = CreateSupplierValidation();
            supplierValidation.ShouldNotHaveValidationErrorFor<SupplierTest, string>(f => f.Telephone, telephone);
        }
    }

    internal class SupplierTest : Supplier
    {
        public SupplierTest()
        {
        }
    }
}
