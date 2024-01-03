using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using PayScale.API.Controllers.v1;
using PayScale.BusinessLayer.BusinessLayer;
using PayScale.DataAccess.Repository;
using PayScale.DataAccess.Repository.IRepository;
using PayScale.Models.ViewModels;
using System.Net;

namespace PayScale.Tests
{
    public class TaxCalculationNegativeTests
    {
        private readonly Mock<IBusinessLogic> businessLogicService;
        private readonly Mock<IUnitOfWork> unitOfWorkService;
        private readonly Mock<IMemoryCache> memoryCacheService;

        public TaxCalculationNegativeTests()
        {
            businessLogicService = new Mock<IBusinessLogic>();
            unitOfWorkService = new Mock<IUnitOfWork>();
            memoryCacheService = new Mock<IMemoryCache>();
        }

        [Theory]
        [InlineData(8350, "7441")]
        public void CalculateProgressive1TaxNegativeTest(decimal amount, string postalCode)
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
            var response = taxController.CalculateTax(-10.0M, postalCode);

            var okResult = response as OkObjectResult;
            var taxResults = okResult?.Value as TaxCalculationViewModel;
            //assert
            Assert.True(taxResults is null);
        }

        [Theory]
        [InlineData(null, "1000")]
        public void CalculateProgressive2TaxNegativeTest(decimal? amount, string postalCode)
        {
            try
            {


                //arrange
                var taxModel = new TaxCalculationViewModel
                {
                    AmountAfterTax = 7700,
                    TaxPercentage = "15.00%",
                    TaxType = "Progressive"
                };

                businessLogicService.Setup(x => x.TaxCalculationLogic(amount.Value, postalCode))
                    .Returns(taxModel);

                var taxController = new TaxController(unitOfWorkService.Object, businessLogicService.Object, memoryCacheService.Object);
                //act
                var response = taxController.CalculateTax(amount.Value, postalCode);

                var okResult = response as OkObjectResult;
                var taxResults = okResult?.Value as TaxCalculationViewModel;

  
            }
            catch (Exception ex)
            {
                //assert
                Assert.NotNull(ex);
                Assert.True(ex.Message is "Nullable object must have a value.");
            }

        }
    }
}