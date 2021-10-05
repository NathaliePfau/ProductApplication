using ProductApplication.Application.Models.Categories;
using ProductApplication.Application.Services.Categories;
using ProductApplication.Domain.Entities;
using ProductApplication.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using ProductApplication.Tests.Builder;
using ProductApplication.Domain.AppFlowControl;

namespace ProductApplication.Tests.Unit.Application.Services
{
    public class CategoryServicesTests
    {
        private const string EXCEPTION_IT_EXISTS = "Categoria já cadastrado, favor cadasatrar uma que não exista!";
        private const string EXCEPTION_COMMUNICATION_ERROR = "Ocorreu um Erro na Comunicação! ";

        private readonly ICategoryService _categoryService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFormFile _iIformFile;


        public CategoryServicesTests()
        {
            _categoryRepository = Substitute.For<ICategoryRepository>();
            _categoryService = new CategoryService(_categoryRepository);
            _iIformFile = Substitute.For<IFormFile>();
        }

        [Fact]
        public async Task Must_CreateCategory_Successfully()
        {
            var categoryTest = new CategoryRequestModel()
            {
                Name = "Refrigerante",
                IdSupplier = 1,
                TradeSupplier = "Sukita"
            };

            var categoryRetornado = await _categoryService.Create(categoryTest);
            await _categoryRepository.Received(1).Create(Arg.Is<Category>(f => !f.Deleted
                                                              && f.Id == 0
                                                              && f.Name == "Refrigerante"
                                                              && f.IdSupplier == 1));
            categoryRetornado.Should().NotBeNull();
            categoryRetornado.Name.Should().Be(categoryTest.Name);
            categoryRetornado.IdSupplier.Should().Be(categoryTest.IdSupplier);
            categoryRetornado.Deleted.Should().BeFalse();
            categoryRetornado.Id.Should().Be(0);
        }

        [Fact]
        public async Task Must_CreateCategory_AgainAndFail_RetunsException()
        {
            var categoryTest = new CategoryRequestModel()
            {
                Name = "Refrigerante",
                IdSupplier = 1,
                TradeSupplier = "Sukita"
            };

            _categoryRepository.ItExists(categoryTest.Name).Returns(true);
            var ex = await Record.ExceptionAsync(() => _categoryService.Create(categoryTest));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Be(EXCEPTION_IT_EXISTS);
        }

        [Fact]
        public async Task Must_CreateCategory_FailTheValidation_ReturnException()
        {
            var categoryTest = new CategoryRequestModel()
            {
                Name = "Re",
                IdSupplier = 1,
                TradeSupplier = "Sukita"
            };

            var ex = await Record.ExceptionAsync(() => _categoryService.Create(categoryTest));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Contain("O Nome deve conter entre 3 e 250 caracteres");
        }

        [Fact]
        public async Task Must_GetSupplier_ReturnSupplier()
        {
            var idCategoryTest = 9;
            var categoryTest = new CategoryBuilder()
                             .UseName("Refrigerante")
                             .UseIdSupplier(1)
                             .UseId(idCategoryTest)
                             .Build();
            _categoryRepository.Get(idCategoryTest)
                                 .Returns(categoryTest);

            var categoryRetornado = await _categoryService.Get(idCategoryTest);

            categoryRetornado.Should().NotBeNull();
            categoryRetornado.Name.Should().Be(categoryTest.Name);
            categoryRetornado.IdSupplier.Should().Be(categoryTest.IdSupplier);
            categoryRetornado.Deleted.Should().BeFalse();
            categoryRetornado.Id.Should().Be(9);
        }

        [Fact]
        public async Task Must_GetCategory_FailBecauseNotExists_ReturnException()
        {
            var ex = await Record.ExceptionAsync(() => _categoryService.Get(6));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Be("Não existe uma categoria com esse id");
        }

        [Fact]
        public async Task Must_GetCategory_FailBecauseDeleted_ReturnException()
        {
            var idCategoryTest = 1;
            var categoryTest = new CategoryBuilder()
                             .UseName("Refrigerante")
                             .UseIdSupplier(1)
                             .UseId(idCategoryTest)
                             .Build();

            categoryTest.Delete();

            _categoryRepository.Get(idCategoryTest).Returns(categoryTest);
            var ex = await Record.ExceptionAsync(() => _categoryService.Update(idCategoryTest, new CategoryRequestModel()));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Be("Essa cateogria foi inativada,favor escolher uma ativa");
        }

        [Fact]
        public async Task Must_GetAllCategories_ReturnsCategories()
        {
            var idCategoryTest1 = 1;
            var categoryTest = new CategoryBuilder()
                             .UseName("Refrigerante")
                             .UseIdSupplier(1)
                             .UseId(idCategoryTest1)
                             .Build();

            var idCategoryTest2 = 2;
            var categoryTest2 = new CategoryBuilder()
                             .UseName("Sucos")
                             .UseIdSupplier(1)
                             .UseId(idCategoryTest2)
                             .Build();

            var categories = new List<Category>
            {
                categoryTest,
                categoryTest2
            };

            _categoryRepository.GetAll()
                                 .Returns(categories);

            var categoriesRetornados = await _categoryService.GetAll();

            categoriesRetornados.Should().HaveCount(2);
            categoriesRetornados.Any(x => x.Name == categoryTest.Name &&
                                            x.IdSupplier == categoryTest.IdSupplier &&
                                            x.Id == idCategoryTest1 &&
                                            !x.Deleted).Should().BeTrue();

            categoriesRetornados.Any(x => x.Name == categoryTest2.Name &&
                                           x.IdSupplier == categoryTest2.IdSupplier &&
                                           x.Id == idCategoryTest2 &&
                                           !x.Deleted).Should().BeTrue();
        }

        [Fact]
        public async Task Must_UpdateCategory_ReturnUpdateCategory()
        {
            var idCategoryTest = 1;
            var categoryTest = new CategoryBuilder()
                             .UseName("Refrigerante")
                             .UseIdSupplier(1)
                             .UseId(idCategoryTest)
                             .Build();

            var modelTest = new CategoryRequestModel()
            {
                Name = "Sucos",
                IdSupplier = 2,
                TradeSupplier = "Sukita"
            };

            _categoryRepository.Get(idCategoryTest).Returns(categoryTest);
            var categoryRetornado = await _categoryService.Update(idCategoryTest, modelTest);
            categoryRetornado.Should().NotBeNull();
            categoryRetornado.Deleted.Should().BeFalse();
            categoryRetornado.Name.Should().Be("Sucos");
            categoryRetornado.IdSupplier.Should().Be(2);
            categoryRetornado.Id.Should().Be(idCategoryTest);

            await _categoryRepository.Received(1).Update(Arg.Is<Category>(f => !f.Deleted
                                                               && f.Id == idCategoryTest
                                                               && f.Name == "Sucos"
                                                               && f.IdSupplier == 2));
        }

        [Fact]
        public async Task Must_UpdateCategory_FailBecauseIsNotExists_ReturnsException()
        {
            var IdCategoryTest = 6;
            var ex = await Record.ExceptionAsync(() => _categoryService.Update(IdCategoryTest, new CategoryRequestModel()));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Be("Não existe uma categoria com esse id");
        }

        [Fact]
        public async Task Must_UpdateCategory_FailBecauseInactive_Returns()
        {
            var idCategoryTest = 1;
            var categoryTest = new CategoryBuilder()
                            .UseName("Refrigerante")
                            .UseIdSupplier(1)
                            .UseId(idCategoryTest)
                            .Build();

            categoryTest.Delete();
            _categoryRepository.Get(idCategoryTest).Returns(categoryTest);
            var ex = await Record.ExceptionAsync(() => _categoryService.Update(idCategoryTest, new CategoryRequestModel()));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Be("Essa cateogria foi inativada,favor escolher uma ativa");
        }

        [Fact]
        public async Task Must_UpdateCategory_ValidateFail_ReturnsExceptions()
        {
            var idCategoryTest = 1;
            var modelTest = new CategoryRequestModel()
            {
                Name = "Su",
                IdSupplier = 1,
                TradeSupplier = "Sukita"
            };

            var categoryTest = new CategoryBuilder()
                            .UseName("Su")
                            .UseIdSupplier(1)
                            .UseId(idCategoryTest)
                            .Build();

            _categoryRepository.Get(idCategoryTest).Returns(categoryTest);

            var ex = await Record.ExceptionAsync(() => _categoryService.Update(idCategoryTest, modelTest));

            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Contain("O Nome deve conter entre 3 e 250 caracteres");
        }


        [Fact]
        public async Task Must_DeleteCategory()
        {
            var idCategoryTest = 1;
            var categoryTest = new CategoryBuilder()
                          .UseName("Sucos")
                          .UseIdSupplier(1)
                          .UseId(idCategoryTest)
                          .Build();

            _categoryRepository.Get(idCategoryTest).Returns(categoryTest);

            await _categoryService.Delete(idCategoryTest);
            await _categoryRepository.Received(1)
                                       .Delete(categoryTest);
        }

        [Fact]
        public async Task Must_DeleteCategory_FailBecuseNoExists_RetunsException()
        {
            var idCategoryTest = 1;
            var ex = await Record.ExceptionAsync(() => _categoryService.Delete(idCategoryTest));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Be("Não existe uma categoria com esse id");
        }

        [Fact]
        public async Task Must_DeleteCategory_FailBecauseDeleted_ReturnException()
        {
            var idCategoryTest = 1;
            var categoryTest = new CategoryBuilder()
                          .UseName("Sucos")
                          .UseIdSupplier(1)
                          .UseId(idCategoryTest)
                          .Build();

            categoryTest.Delete();
            _categoryRepository.Get(idCategoryTest).Returns(categoryTest);
            var ex = await Record.ExceptionAsync(() => _categoryService.Delete(idCategoryTest));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Be("Essa cateogria foi inativada,favor escolher uma ativa");
        }

        [Fact]
        public async Task Must_ExportCategory()
        {
            var supplier = new SupplierBuilder().Build();
            var idCategoryTest1 = 1;
            var categoryTest = new CategoryBuilder()
                             .UseName("Refrigerante")
                             .UseIdSupplier(1)
                             .UseSupplier(supplier)
                             .UseId(idCategoryTest1)
                             .Build();

            var idCategoryTest2 = 2;
            var categoryTest2 = new CategoryBuilder()
                             .UseName("Sucos")
                             .UseIdSupplier(1)
                             .UseSupplier(supplier)
                             .UseId(idCategoryTest2)
                             .Build();

            var categories = new List<Category>
            {
                categoryTest,
                categoryTest2
            };

            _categoryRepository.GetAllSupplier().Returns(categories);
            var categoriesRetuns = await _categoryService.Export();
            categoriesRetuns.Should().NotBeNull();
        }

        [Fact]
        public async Task Must_ImportCategory()
        {
            var categoryHeader = "HEADER,HEADER \nSuco,1 \nCerveja,2";
            var fileDatas = new MemoryStream(Encoding.UTF8.GetBytes(categoryHeader));

            _iIformFile.OpenReadStream().Returns(fileDatas);
            var categoriaImport = await _categoryService.Import(_iIformFile);

            categoriaImport.Should().HaveCount(2);

            categoriaImport.Any(c => c.Name == "Suco" && c.IdSupplier == 1).Should().BeTrue();
            categoriaImport.Any(c => c.Name == "Cerveja" && c.IdSupplier == 2).Should().BeTrue();
        }

        [Fact]
        public async Task Must_ImportCategory_FailItExists_ReturnException()
        {
            var categoryHeader = "HEADER,HEADER \nSuco,1 \nCerveja,2";
            var fileDatas = new MemoryStream(Encoding.UTF8.GetBytes(categoryHeader));

            _iIformFile.OpenReadStream().Returns(fileDatas);
            _categoryRepository.ItExists("Suco").Returns(true);

            var ex = await Record.ExceptionAsync(() => _categoryService.Import(_iIformFile));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_IT_EXISTS);
        }

        [Fact]
        public async Task Must_ImportCategory_FailNullName_ReturnException()
        {
            var categoryHeader = "HEADER,HEADER \n ,1 \n ,2";
            var fileDatas = new MemoryStream(Encoding.UTF8.GetBytes(categoryHeader));

            _iIformFile.OpenReadStream().Returns(fileDatas);

            var ex = await Record.ExceptionAsync(() => _categoryService.Import(_iIformFile));
            ex.Should().BeOfType<FluentValidation.ValidationException>();
        }

        [Fact]
        public async Task Must_ImportCategory_FailNullIdSupplier_ReturnException()
        {
            var categoryHeader = "HEADER,HEADER \nSuco, \nCerveja,";
            var fileDatas = new MemoryStream(Encoding.UTF8.GetBytes(categoryHeader));

            _iIformFile.OpenReadStream().Returns(fileDatas);

            var ex = await Record.ExceptionAsync(() => _categoryService.Import(_iIformFile));
            ex.Should().BeOfType<FormatException>();
        }
    }
}
