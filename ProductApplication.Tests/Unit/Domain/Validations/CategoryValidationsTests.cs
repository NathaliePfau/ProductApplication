using ProductApplication.Domain.Entities;
using ProductApplication.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProductApplication.Tests.Unit.Domain.Validations
{
    public class CategoryValidationTests
    {

        private CategoryValidations CreateCategoryValidation()
        {
            return new CategoryValidations();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [Trait("Category", "Name")]
        public void Valide_Name_Null_And_Empty(string name)
        {
            var categoryValidation = CreateCategoryValidation();
            categoryValidation.ShouldHaveValidationErrorFor<CategoryTest, string>(x => x.Name, name).WithErrorMessage("O Nome não pode ser nulo");
        }

        [Theory]
        [InlineData(251)]
        [InlineData(2)]
        [Trait("Category", "Name")]
        public void Valide_Name_Characters(int characters)
        {
            var categoryValidation = CreateCategoryValidation();
            string name = new string('.', characters);
            categoryValidation.ShouldHaveValidationErrorFor<CategoryTest, string>(x => x.Name, name).WithErrorMessage("O Nome deve conter entre 3 e 250 caracteres.");
        }

        [Theory]
        [InlineData(-2)]
        [InlineData(-1)]
        [Trait("Category", "IdSupplier")]
        public void Valide_IdSupplier_Null_And_Empty(int idSupplier)
        {
            var categoryValidation = CreateCategoryValidation();
            categoryValidation.ShouldHaveValidationErrorFor<CategoryTest, int>(x => x.IdSupplier, idSupplier).WithErrorMessage("O id do fornecedor deve ser maior que 0");
        }

        [Fact]
        [Trait("Category", "IdSupplier")]
        public void MustValidate_IdSupplier_Null()
        {
            var validation = CreateCategoryValidation();
            validation.ShouldHaveValidationErrorFor<CategoryTest, int>(x => x.IdSupplier, 0).WithErrorMessage("O id do fornecedor não pode ser nulo");
        }
    }
    internal class CategoryTest : Category
    {
        public CategoryTest()
        {
        }
    }
}
