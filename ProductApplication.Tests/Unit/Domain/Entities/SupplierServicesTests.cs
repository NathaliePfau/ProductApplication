using FluentAssertions;
using ProductApplication.Domain.ComplexType;
using ProductApplication.Domain.Entities;
using ProductApplication.Tests.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProductApplication.Tests.Unit.Domain.Entities
{
    public class SupplierEntityTest
    {
        private readonly Address addressTest = new Address("89.037-506", "Escola Agricola", "Rua Hermann Spernau", "Blumenau", "SC", "60", "");

        [Fact]
        public void Must_BuildSupplier()
        {
            var fornecedorTest = new Supplier("Supplier1", "79.379.491/0001-83", "Supplier", addressTest, "fornecedor@gmail.com", "99772-5524");

            fornecedorTest.Should().NotBeNull();
            fornecedorTest.CompanyName.Should().Be("Supplier1");
            fornecedorTest.CNPJ.Should().Be("79.379.491/0001-83");
            fornecedorTest.Trade.Should().Be("Supplier");
            fornecedorTest.ContactEmail.Should().Be("fornecedor@gmail.com");
            fornecedorTest.Telephone.Should().Be("99772-5524");
            fornecedorTest.Address.Neighborhood.Should().Be("Escola Agricola");
            fornecedorTest.Address.ZipCode.Should().Be("89.037-506");
            fornecedorTest.Address.City.Should().Be("Blumenau");
            fornecedorTest.Address.Number.Should().Be("60");
            fornecedorTest.Address.Street.Should().Be("Rua Hermann Spernau");
            fornecedorTest.Address.State.Should().Be("SC");
            fornecedorTest.Id.Should().Be(0);
            fornecedorTest.Deleted.Should().BeFalse();
        }

        [Fact]
        public void Must_DeleteSupplier()
        {
            var fornecedorTest = new Supplier("SupplierMagico", "79.379.491/0001-83", "Supplier", addressTest, "fornecedor@gmail.com", "99772-5524");
            fornecedorTest.Delete();
            fornecedorTest.Deleted.Should().BeTrue();
        }

        [Fact]
        public void Must_UpdateSupplier()
        {
            Address addressTest2 = new Address("89.307-506", "Vila Nova", "Rua São Sebastião", "Curitiba", "PR", "30");
            var fornecedorTest = new SupplierBuilder()
            .UseCompanyName("Supplier1")
            .UseCnpj("79.379.491/0001-83")
            .UseTrade("Supplier")
            .UseContactEmail("fornecedor@gmail.com")
            .UseAddress(addressTest)
            .UseId(1)
            .Build();

            fornecedorTest.Update("SupplierMagico", "79.309.491/0001-83", "Supplier SA", addressTest2, "supplier@gmail.com", "99913-1466");

            fornecedorTest.CompanyName.Should().Be("SupplierMagico");
            fornecedorTest.CNPJ.Should().Be("79.309.491/0001-83");
            fornecedorTest.Trade.Should().Be("Supplier SA");
            fornecedorTest.ContactEmail.Should().Be("supplier@gmail.com");
            fornecedorTest.Telephone.Should().Be("99913-1466");
            fornecedorTest.Id.Should().Be(1);
            fornecedorTest.Deleted.Should().BeFalse();
            fornecedorTest.Address.Neighborhood.Should().Be("Vila Nova");
            fornecedorTest.Address.ZipCode.Should().Be("89.307-506");
            fornecedorTest.Address.City.Should().Be("Curitiba");
            fornecedorTest.Address.Number.Should().Be("30");
            fornecedorTest.Address.Street.Should().Be("Rua São Sebastião");
            fornecedorTest.Address.State.Should().Be("PR");
        }
    }
}
