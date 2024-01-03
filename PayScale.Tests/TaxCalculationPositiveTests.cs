using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using PayScale.API.Controllers.v1;
using PayScale.BusinessLayer.BusinessLayer;
using PayScale.DataAccess.Repository;
using PayScale.DataAccess.Repository.IRepository;
using PayScale.Models.ViewModels;

namespace PayScale.Tests
{
    public class TaxCalculationPositiveTests
    {
        private readonly Mock<IBusinessLogic> businessLogicService;
        private readonly Mock<IUnitOfWork> unitOfWorkService;
        private readonly Mock<IMemoryCache> memoryCacheService;

        public TaxCalculationPositiveTests()
        {
            businessLogicService = new Mock<IBusinessLogic>();
            unitOfWorkService = new Mock<IUnitOfWork>();
            memoryCacheService = new Mock<IMemoryCache>();
        }
        [Theory]
        [InlineData(10000, "A100")]
        public void CalculateFlatValueTaxPositiveTest(decimal amount, string postalCode)
        {
            //arrange
            var taxModel = new TaxCalculationViewModel
            {
                AmountAfterTax = 850,
                TaxPercentage = "5.00%",
                TaxType = "Flat Value"
            };

            businessLogicService.Setup(x => x.TaxCalculationLogic(amount, postalCode))
                .Returns(taxModel);

            var taxController = new TaxController(unitOfWorkService.Object,businessLogicService.Object, memoryCacheService.Object);
            //act
            var response =  taxController.CalculateTax(amount, postalCode);

            var okResult = response as OkObjectResult;
            var taxResults = okResult?.Value as TaxCalculationViewModel;
            //assert
            Assert.NotNull(taxResults);
            Assert.Equal(taxModel.AmountAfterTax, taxResults?.AmountAfterTax);
            Assert.True(taxModel.TaxPercentage == taxResults?.TaxPercentage);
        }

        [Theory]
        [InlineData(10000, "7000")]
        public void CalculateFlatRateTaxPositiveTest(decimal amount, string postalCode)
        {
            //arrange
            var taxModel = new TaxCalculationViewModel
            {
                AmountAfterTax = 850,
                TaxPercentage = "17.5%",
                TaxType = "Flat Rate"
            };

            businessLogicService.Setup(x => x.TaxCalculationLogic(amount, postalCode))
                .Returns(taxModel);

            var taxController = new TaxController(unitOfWorkService.Object, businessLogicService.Object, memoryCacheService.Object);
            //act
            var response = taxController.CalculateTax(amount, postalCode);

            var okResult = response as OkObjectResult;
            var taxResults = okResult?.Value as TaxCalculationViewModel;
            //assert
            Assert.NotNull(taxResults);
            Assert.Equal(taxModel.AmountAfterTax, taxResults?.AmountAfterTax);
            Assert.True(taxModel.TaxPercentage == taxResults?.TaxPercentage);
        }


        [Theory]
        [InlineData(8350, "7441")]
        public void CalculateProgressive1TaxPositiveTest(decimal amount, string postalCode)
        {
            //arrange
            var taxModel = new TaxCalculationViewModel
            {
                AmountAfterTax = 7800,
                TaxPercentage = "10.00%",
                TaxType = "Progressive"
            };

            businessLogicService.Setup(x => x.TaxCalculationLogic(amount, postalCode))
                .Returns(taxModel);

            var taxController = new TaxController(unitOfWorkService.Object, businessLogicService.Object, memoryCacheService.Object);
            //act
            var response = taxController.CalculateTax(amount, postalCode);

            var okResult = response as OkObjectResult;
            var taxResults = okResult?.Value as TaxCalculationViewModel;
            //assert
            Assert.NotNull(taxResults);
            Assert.Equal(taxModel.AmountAfterTax, taxResults?.AmountAfterTax);
            Assert.True(taxModel.TaxPercentage == taxResults?.TaxPercentage);
        }

        [Theory]
        [InlineData(8351, "1000")]
        public void CalculateProgressive2TaxPositiveTest(decimal amount, string postalCode)
        {
            //arrange
            var taxModel = new TaxCalculationViewModel
            {
                AmountAfterTax = 7700,
                TaxPercentage = "15.00%",
                TaxType = "Progressive"
            };

            businessLogicService.Setup(x => x.TaxCalculationLogic(amount, postalCode))
                .Returns(taxModel);

            var taxController = new TaxController(unitOfWorkService.Object, businessLogicService.Object, memoryCacheService.Object);
            //act
            var response = taxController.CalculateTax(amount, postalCode);

            var okResult = response as OkObjectResult;
            var taxResults = okResult?.Value as TaxCalculationViewModel;
            //assert
            Assert.NotNull(taxResults);
            Assert.Equal(taxModel.AmountAfterTax, taxResults?.AmountAfterTax);
            Assert.True(taxModel.TaxPercentage == taxResults?.TaxPercentage);
        }
    }
}