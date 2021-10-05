using FluentAssertions;
using ProductApplication.Domain.Entities;
using ProductApplication.Tests.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProductApplication.Tests.Unit.Domain.Entities
{
    public class CategoryEntityTest
    {
        [Fact]
        public void Must_BuildCategory()
        {
            var categoryTest = new Category("Refrigerante", 1);

            categoryTest.Should().NotBeNull();
            categoryTest.Name.Should().Be("Refrigerante");
            categoryTest.IdSupplier.Should().Be(1);
            categoryTest.Id.Should().Be(0);
            categoryTest.Deleted.Should().BeFalse();
        }

        [Fact]
        public void Must_DeleteSupplier()
        {
            var categoryTest = new Category("Refrigerante", 1);

            categoryTest.Delete();
            categoryTest.Deleted.Should().BeTrue();
        }

        [Fact]
        public void Must_UpdateSupplier()
        {
            var categoryTest = new CategoryBuilder()
            .UseName("Refrigerante")
            .UseIdSupplier(1)
            .UseId(1)
            .Build();

            categoryTest.Update("Suco", 2);

            categoryTest.Name.Should().Be("Suco");
            categoryTest.IdSupplier.Should().Be(2);
            categoryTest.Id.Should().Be(1);
            categoryTest.Deleted.Should().BeFalse();
        }
    }
}
