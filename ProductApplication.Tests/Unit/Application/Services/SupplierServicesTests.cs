using FluentAssertions;
using NSubstitute;
using ProductApplication.Application.Models.Address;
using ProductApplication.Application.Models.Supplieries;
using ProductApplication.Application.Services.Suppliers;
using ProductApplication.Domain.AppFlowControl;
using ProductApplication.Domain.ComplexType;
using ProductApplication.Domain.Entities;
using ProductApplication.Domain.Interfaces;
using ProductApplication.Tests.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductApplication.Tests.Unit.Application.Services
{
    public class SupplierServicesTests
    {
        private const string EXCEPTION_IT_EXISTS = "Fornecedor já cadastrado, favor cadasatrar um que não exista!";
        private const string EXCEPTION_COMMUNICATION_ERROR = "Ocorreu um Erro na Comunicação! ";

        private readonly Address addressTest = new Address("89033333134", "Vila Nova", "Rua Theodoro Holtrup", "Blumenau", "SC", "982", "apto. 2");
        private readonly ISupplierService _supplierService;
        private readonly ISupplierRepository _supplierRepository;

        public SupplierServicesTests()
        {
            _supplierRepository = Substitute.For<ISupplierRepository>();
            _supplierService = new SupplierService(_supplierRepository);
        }

        [Fact]
        public async Task Must_CreateSupplier_Successfully()
        {
            var supplierTest = new SupplierRequestModel()
            {
                CompanyName = "Ambev SA",
                CNPJ = "07526557000100",
                Trade = "Ambev",
                Address = new AddressModel()
                {
                    ZipCode = "89013444444",
                    Neighborhood = "Vila Nova",
                    Street = "Rua Theodoro Holtrup",
                    City = "Blumenau",
                    State = "SC",
                    Number = "982",
                    Complement = "apto 32"
                },
                ContactEmail = "ambev@gmail.com",
                Telephone = "(47)99772-5524"
            };

            var supplierRetornado = await _supplierService.Create(supplierTest);
            await _supplierRepository.Received(1).Create(Arg.Is<Supplier>(f => !f.Deleted
                                                              && f.Id == 0
                                                              && f.CompanyName == "Ambev SA"
                                                              && f.CNPJ == "07526557000100"
                                                              && f.Trade == "Ambev"
                                                              && f.Telephone == "(47)99772-5524"
                                                              && f.Address.ZipCode == "89013444444"
                                                              && f.Address.Neighborhood == "Vila Nova"
                                                              && f.Address.Street == "Rua Theodoro Holtrup"
                                                              && f.Address.City == "Blumenau"
                                                              && f.Address.State == "SC"
                                                              && f.Address.Number == "982"
                                                              && f.Address.Complement == "apto 32"
                                                              && f.ContactEmail == "ambev@gmail.com"));
            supplierRetornado.Should().NotBeNull();
            supplierRetornado.CompanyName.Should().Be(supplierTest.CompanyName);
            supplierRetornado.CNPJ.Should().Be(supplierTest.CNPJ);
            supplierRetornado.Trade.Should().Be(supplierTest.Trade);
            supplierRetornado.Address.ZipCode.Should().Be(supplierTest.Address.ZipCode);
            supplierRetornado.Address.Neighborhood.Should().Be(supplierTest.Address.Neighborhood);
            supplierRetornado.Address.Street.Should().Be(supplierTest.Address.Street);
            supplierRetornado.Address.City.Should().Be(supplierTest.Address.City);
            supplierRetornado.Address.State.Should().Be(supplierTest.Address.State);
            supplierRetornado.Address.Number.Should().Be(supplierTest.Address.Number);
            supplierRetornado.Deleted.Should().BeFalse();
            supplierRetornado.ContactEmail.Should().Be(supplierTest.ContactEmail);
            supplierRetornado.Telephone.Should().Be(supplierTest.Telephone);
            supplierRetornado.Id.Should().Be(0);
        }

        [Fact]
        public async Task Must_CreateSupplier_AgainAndFail_RetunsException()
        {
            var supplier = new SupplierRequestModel()
            {
                CompanyName = "Ambev SA",
                CNPJ = "07526557000100",
                Trade = "Ambev",
                Address = new AddressModel()
                {
                    ZipCode = "890134",
                    Neighborhood = "Vila Nova",
                    Street = "Rua Theodoro Holtrup",
                    City = "Blumenau",
                    State = "SC",
                    Number = "982"
                },
                ContactEmail = "ambev@gmail.com",
                Telephone = "997725524"
            };

            _supplierRepository.ItExists(supplier.CNPJ).Returns(true);

            var ex = await Record.ExceptionAsync(() => _supplierService.Create(supplier));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Be(EXCEPTION_IT_EXISTS);
        }

        [Fact]
        public async Task Must_CreateSupplier_FailTheValidation_ReturnException()
        {
            var supplier = new SupplierRequestModel()
            {
                CompanyName = "Ambev SA",
                CNPJ = "0752655746434841000100",
                Trade = "Ambev",
                Address = new AddressModel()
                {
                    ZipCode = "89055555514",
                    Neighborhood = "Vila Nova",
                    Street = "Rua Theodoro Holtrup",
                    City = "Blumenau",
                    State = "SC",
                    Number = "982"
                },
                ContactEmail = "ambev@gmail.com",
                Telephone = "(47)99772-5524"
            };

            var ex = await Record.ExceptionAsync(() => _supplierService.Create(supplier));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Contain("O CNPJ é inválido ou possui caracteres incorretos!");
        }

        [Fact]
        public async Task Must_GetSupplier_ReturnSupplier()
        {
            var idSupplierTest = 9;
            var supplierTest = new SupplierBuilder()
                             .UseCompanyName("Supplier1")
                             .UseCnpj("79.379.491/0001-83")
                             .UseTrade("Supplier")
                             .UseContactEmail("supplier@gmail.com")
                             .UseTelephone("997725524")
                             .UseAddress(addressTest)
                             .UseId(idSupplierTest)
                             .Build();
            _supplierRepository.Get(idSupplierTest)
                                 .Returns(supplierTest);

            var supplierRetornado = await _supplierService.Get(idSupplierTest);

            supplierRetornado.CompanyName.Should().Be(supplierTest.CompanyName);
            supplierRetornado.CNPJ.Should().Be(supplierTest.CNPJ);
            supplierRetornado.Trade.Should().Be(supplierTest.Trade);
            supplierRetornado.Address.ZipCode.Should().Be(supplierTest.Address.ZipCode);
            supplierRetornado.Address.Neighborhood.Should().Be(supplierTest.Address.Neighborhood);
            supplierRetornado.Address.Street.Should().Be(supplierTest.Address.Street);
            supplierRetornado.Address.City.Should().Be(supplierTest.Address.City);
            supplierRetornado.Address.State.Should().Be(supplierTest.Address.State);
            supplierRetornado.Address.Number.Should().Be(supplierTest.Address.Number);
            supplierRetornado.Deleted.Should().BeFalse();
            supplierRetornado.ContactEmail.Should().Be(supplierTest.ContactEmail);
            supplierRetornado.Telephone.Should().Be(supplierTest.Telephone);
            supplierRetornado.Id.Should().Be(idSupplierTest);
        }

        [Fact]
        public async Task Must_GetSupplier_FailBecauseNotExists_ReturnException()
        {
            var ex = await Record.ExceptionAsync(() => _supplierService.Get(6));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Be("Não existe um fornecedor com esse id");
        }

        [Fact]
        public async Task Must_GetSupplier_FailBecauseDeleted_ReturnException()
        {
            var IdDoSupplierTest = 1;

            var supplierTest = new SupplierBuilder()
                .UseCompanyName("Supplier1")
                .UseCnpj("79379491000183")
                .UseTrade("Supplier")
                .UseContactEmail("supplier@gmail.com")
                .UseTelephone("997725524")
                .UseAddress(addressTest)
                .Build();

            supplierTest.Delete();

            _supplierRepository.Get(IdDoSupplierTest).Returns(supplierTest);
            var ex = await Record.ExceptionAsync(() => _supplierService.Update(IdDoSupplierTest, new SupplierRequestModel()));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Be("Esse fornecedor foi inativado,favor escolher um ativo");
        }

        [Fact]
        public async Task Must_GetAllSuppliers_ReturnsSuppliers()
        {
            Address address2 = new Address("890134", "Vila Nova", "Rua Theodoro Holtrup", "Blumenau", "SC", "982", "apto. 2");

            var idSupplier1 = 1;
            var supplierTest1 = new SupplierBuilder()
                               .UseCompanyName("Supplier1")
                               .UseCnpj("79.379.491/0001-83")
                               .UseTrade("Supplier")
                               .UseContactEmail("supplier@gmail.com")
                               .UseTelephone("(47)99772-5524")
                               .UseAddress(addressTest)
                               .UseId(idSupplier1)
                               .Build();

            var idSupplier2 = 2;
            var supplierTest2 = new SupplierBuilder()
                               .UseCompanyName("Ambev SA")
                               .UseCnpj("07.526.557/0001-00")
                               .UseTrade("Ambev")
                               .UseContactEmail("ambev@gmail.com")
                               .UseTelephone("(47)99772-5524")
                               .UseId(idSupplier2)
                               .UseAddress(address2)
                               .Build();

            var suppliers = new List<Supplier>
            {
                supplierTest1,
                supplierTest2
            };

            _supplierRepository.GetAll()
                                 .Returns(suppliers);

            var supplieresRetornados = await _supplierService.GetAll();

            supplieresRetornados.Should().HaveCount(2);

            supplieresRetornados.Any(x => x.CompanyName == supplierTest1.CompanyName &&
                                            x.CNPJ == supplierTest1.CNPJ &&
                                            x.Trade == supplierTest1.Trade &&
                                            x.Telephone == supplierTest1.Telephone &&
                                            x.Address.ZipCode == supplierTest1.Address.ZipCode &&
                                            x.Address.Neighborhood == supplierTest1.Address.Neighborhood &&
                                            x.Address.City == supplierTest1.Address.City &&
                                            x.Address.Street == supplierTest1.Address.Street &&
                                            x.Address.Number == supplierTest1.Address.Number &&
                                            x.Address.State == supplierTest1.Address.State &&
                                            x.Address.Complement == supplierTest1.Address.Complement &&
                                            x.ContactEmail == supplierTest1.ContactEmail &&
                                            x.Id == idSupplier1 &&
                                            !x.Deleted).Should().BeTrue();

            supplieresRetornados.Any(x => x.CompanyName == supplierTest2.CompanyName &&
                                            x.CNPJ == supplierTest2.CNPJ &&
                                            x.Trade == supplierTest2.Trade &&
                                            x.Telephone == supplierTest2.Telephone &&
                                            x.Address.ZipCode == supplierTest2.Address.ZipCode &&
                                            x.Address.Neighborhood == supplierTest2.Address.Neighborhood &&
                                            x.Address.City == supplierTest2.Address.City &&
                                            x.Address.Street == supplierTest2.Address.Street &&
                                            x.Address.Number == supplierTest2.Address.Number &&
                                            x.Address.State == supplierTest2.Address.State &&
                                            x.Address.Complement == supplierTest2.Address.Complement &&
                                            x.ContactEmail == supplierTest2.ContactEmail &&
                                            x.Id == idSupplier2 &&
                                            !x.Deleted).Should().BeTrue();
        }

        [Fact]
        public async Task Must_UpdateSupplier_ReturnUpdateSupplier()
        {
            var IdDoSupplierTest = 1;
            var addressTest2 = new AddressModel()
            {
                ZipCode = "43536666666",
                Neighborhood = "Escola Agricula",
                Street = "Rua joão Holtrup",
                City = "Florianopolis",
                State = "PR",
                Number = "54"
            };

            var supplierTest = new SupplierBuilder()
            .UseCompanyName("Supplier1")
            .UseCnpj("79379491000183")
            .UseTrade("Supplier")
            .UseContactEmail("supplier@gmail.com")
            .UseAddress(addressTest)
            .UseId(IdDoSupplierTest)
            .UseTelephone("(47)99772-5524")
            .Build();

            var modelTest = new SupplierRequestModel()
            {
                CompanyName = "Ambev SA",
                CNPJ = "07526557000100",
                Trade = "Ambev",
                Address = addressTest2,
                ContactEmail = "ambev@gmail.com",
                Telephone = "(47)99913-1466"
            };

            _supplierRepository.Get(IdDoSupplierTest).Returns(supplierTest);

            var supplierRetornado = await _supplierService.Update(IdDoSupplierTest, modelTest);

            supplierRetornado.Should().NotBeNull();
            supplierRetornado.Deleted.Should().BeFalse();
            supplierRetornado.CompanyName.Should().Be("Ambev SA");
            supplierRetornado.CNPJ.Should().Be("07526557000100");
            supplierRetornado.Trade.Should().Be("Ambev");
            supplierRetornado.Telephone.Should().Be("(47)99913-1466");
            supplierRetornado.Address.ZipCode.Should().Be("43536666666");
            supplierRetornado.Address.Neighborhood.Should().Be("Escola Agricula");
            supplierRetornado.Address.Street.Should().Be("Rua joão Holtrup");
            supplierRetornado.Address.City.Should().Be("Florianopolis");
            supplierRetornado.Address.State.Should().Be("PR");
            supplierRetornado.Address.Number.Should().Be("54");
            supplierRetornado.ContactEmail.Should().Be("ambev@gmail.com");
            supplierRetornado.Id.Should().Be(IdDoSupplierTest);

            await _supplierRepository.Received(1).Update(Arg.Is<Supplier>(f => !f.Deleted
                                                               && f.Id == IdDoSupplierTest
                                                               && f.CompanyName == "Ambev SA"
                                                               && f.CNPJ == "07526557000100"
                                                               && f.Trade == "Ambev"
                                                               && f.Telephone == "(47)99913-1466"
                                                               && f.Address.ZipCode == "43536666666"
                                                               && f.Address.Neighborhood == "Escola Agricula"
                                                               && f.Address.Street == "Rua joão Holtrup"
                                                               && f.Address.City == "Florianopolis"
                                                               && f.Address.State == "PR"
                                                               && f.Address.Number == "54"
                                                               && f.ContactEmail == "ambev@gmail.com"));
        }

        [Fact]
        public async Task Must_UpdateSupplier_FailBecauseIsNotExists_ReturnsException()
        {
            var IdDoSupplierTest = 6;

            var ex = await Record.ExceptionAsync(() => _supplierService.Update(IdDoSupplierTest, new SupplierRequestModel()));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Be("Não existe um fornecedor com esse id");
        }

        [Fact]
        public async Task Must_UpdateSupplier_FailBecauseInactive_Returns()
        {
            var IdDoSupplierTest = 1;

            var supplierTest = new SupplierBuilder()
                .UseCompanyName("Supplier1")
                .UseCnpj("79379491000183")
                .UseTrade("Supplier")
                .UseContactEmail("supplier@gmail.com")
                .UseTelephone("997725524")
                .UseAddress(addressTest)
                .Build();

            supplierTest.Delete();

            _supplierRepository.Get(IdDoSupplierTest).Returns(supplierTest);
            var ex = await Record.ExceptionAsync(() => _supplierService.Update(IdDoSupplierTest, new SupplierRequestModel()));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Be("Esse fornecedor foi inativado,favor escolher um ativo");
        }

        [Fact]
        public async Task Must_UpdateSupplier_CNPJRepeat_ReturnsExceptions()
        {
            var IdDoSupplierTest = 1;
            var addressTest2 = new AddressModel()
            {
                ZipCode = "890134",
                Neighborhood = "Vila Nova",
                Street = "Rua Theodoro Holtrup",
                City = "Blumenau",
                State = "SC",
                Number = "982"
            };

            var modelTest = new SupplierRequestModel()
            {
                CompanyName = "Ambev SA",
                CNPJ = "07526557000100",
                Trade = "Ambev",
                Address = addressTest2,
                ContactEmail = "ambev@gmail.com",
                Telephone = "997725524"
            };

            var supplierTest = new SupplierBuilder()
            .UseCompanyName("Supplier1")
            .UseCnpj("07526557000101")
            .UseTrade("Supplier")
            .UseContactEmail("supplier@gmail.com")
            .UseAddress(addressTest)
            .Build();

            _supplierRepository.Get(IdDoSupplierTest).Returns(supplierTest);
            _supplierRepository.OtherCNPJ(IdDoSupplierTest, modelTest.CNPJ).Returns(true);

            var ex = await Record.ExceptionAsync(() => _supplierService.Update(IdDoSupplierTest, modelTest));

            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Contain(EXCEPTION_IT_EXISTS);
        }

        [Fact]
        public async Task Must_UpdateSupplier_ValidateFail_ReturnsExceptions()
        {
            var IdDoSupplierTest = 1;
            var addressTest2 = new AddressModel()
            {
                ZipCode = "890888888134",
                Neighborhood = "Vila Nova",
                Street = "Rua Theodoro Holtrup",
                City = "Blumenau",
                State = "SC",
                Number = "982"
            };

            var modelTest = new SupplierRequestModel()
            {
                CompanyName = "Ambev SA",
                CNPJ = "07526557000100",
                Trade = "Ambev",
                Address = addressTest2,
                ContactEmail = "ambev@gmail.com",
                Telephone = "(47)99772-5524"
            };

            var supplierTest = new SupplierBuilder()
            .UseCompanyName("Supplier1")
            .UseCnpj("0752655555555557000100")
            .UseTrade("Supplier")
            .UseContactEmail("supplier@gmail.com")
            .UseAddress(addressTest)
            .Build();

            _supplierRepository.Get(IdDoSupplierTest).Returns(supplierTest);

            var ex = await Record.ExceptionAsync(() => _supplierService.Update(IdDoSupplierTest, modelTest));

            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Contain("CEP deve conter 11 caracteres.");
        }


        [Fact]
        public async Task Must_DeleteSupplier()
        {
            var IdDoSupplierTest = 1;

            var supplierTest = new SupplierBuilder()
            .UseCompanyName("Supplier1")
            .UseCnpj("79.379.491/0001-83")
            .UseTrade("Supplier")
            .UseContactEmail("supplier@gmail.com")
            .UseAddress(addressTest)
            .Build();

            _supplierRepository.Get(IdDoSupplierTest).Returns(supplierTest);

            await _supplierService.Delete(IdDoSupplierTest);
            await _supplierRepository.Received(1)
                                       .Delete(supplierTest);
        }

        [Fact]
        public async Task Must_DeleteSupplier_FailBecuseNoExists_RetunsException()
        {
            var IdDoSupplierTest = 1;
            var ex = await Record.ExceptionAsync(() => _supplierService.Delete(IdDoSupplierTest));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Be("Não existe um fornecedor com esse id");
        }

        [Fact]
        public async Task Must_DeleteSupplier_FailBecauseDeleted_ReturnException()
        {
            var IdDoSupplierTest = 1;
            var supplierTest = new SupplierBuilder()
            .UseCompanyName("Supplier1")
            .UseId(IdDoSupplierTest)
            .UseCnpj("79.379.491/0001-83")
            .UseTrade("Supplier")
            .UseContactEmail("supplier@gmail.com")
            .UseAddress(addressTest)
            .UseTelephone("997725524")
            .Build();

            supplierTest.Delete();

            _supplierRepository.Get(IdDoSupplierTest).Returns(supplierTest);
            var ex = await Record.ExceptionAsync(() => _supplierService.Delete(IdDoSupplierTest));
            ex.Should().BeOfType<ServicesException>();
            ex.Message.Should().Be(EXCEPTION_COMMUNICATION_ERROR);
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Message.Should().Be("Esse fornecedor foi inativado,favor escolher um ativo");
        }
    }
}
