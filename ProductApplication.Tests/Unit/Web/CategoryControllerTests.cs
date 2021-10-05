using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using ProductApplication.Application.Services.Categories;
using ProductApplication.Domain.AppFlowControl;
using ProductApplication.Web.Controllers;
using System.Threading.Tasks;
using Xunit;

namespace ProductApplication.Tests.Unit.Web
{
    public class CategoryControllerTest
    {
        private const string COMMUNICATION_ERROR = "Erro na comunicação";

        private readonly CategoryController _categoryController;
        private readonly ICategoryService _categoryService;
        private readonly IFormFile _iIformFile;

        public CategoryControllerTest()
        {
            _categoryService = Substitute.For<ICategoryService>();
            _categoryController = new CategoryController(_categoryService);
            _iIformFile = Substitute.For<IFormFile>();
        }

        [Fact]
        public async Task Must_ExportCategory_Fail_RetunsException()
        {
            _categoryService.When(x => x.Export()).Do(
            x => { throw new ServicesException(COMMUNICATION_ERROR); });

            var ex = await Record.ExceptionAsync(() => _categoryController.Export());
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(COMMUNICATION_ERROR);
        }

        [Fact]
        public async Task Must_ImportCategory_Fail_RetunsException()
        {
            _categoryService.When(x => x.Import(_iIformFile)).Do(
            x => { throw new ServicesException(COMMUNICATION_ERROR); });

            var ex = await Record.ExceptionAsync(() => _categoryController.Import(_iIformFile));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(COMMUNICATION_ERROR);
        }
        [Fact]
        public async Task Must_ImportCategory_ReturnOK()
        {
            var fileMock = new Mock<IFormFile>();

            var result = await _categoryController.Import(fileMock.Object);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
            await _categoryService.Received(1).Import(fileMock.Object);
        }
    }
}
