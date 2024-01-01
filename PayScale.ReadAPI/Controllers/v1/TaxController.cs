using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PayScale.DataAccess.Repository.IRepository;
using PayScale.Models;
using PayScale.Models.ViewModels;


namespace PayScale.Read.API.Controllers.v1
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/")]
    [ApiVersion("1.0")]
    public class TaxController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="cache"></param>
        public TaxController(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        /// <summary>
        /// This action gets all the taxes postal codes
        /// </summary>
        /// <returns></returns>

        [Route("PostalCodes")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPostalCodes()
        {
            var key = nameof(GetPostalCodes);
            if (_cache.TryGetValue(key, out IEnumerable<PostalCodeTaxType>? data))
            {
                return Ok(data);
            }

            data = _unitOfWork.PostalCodeTaxType.GetAll(includeProperties: "PostalCode,TaxType");

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

            _cache.Set(key, data, cacheOptions);
                 

            return Ok(data);
        }
        /// <summary>
        /// This action gets taxes by postal codes
        /// </summary>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        [Route("ByPostalCode")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPostalCodesByCode(string postalCode)
        {

            var key = $"{nameof(GetPostalCodesByCode)}_{postalCode}";

            if (_cache.TryGetValue(key, out PostalCodeTaxType? data))
            {
                return Ok(data);
            }

            data = _unitOfWork.PostalCodeTaxType.Get(filter: w => w.PostalCode!.Code.Equals(postalCode), includeProperties: "PostalCode,TaxType");

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

            _cache.Set(key, data, cacheOptions);


            return Ok(data);
        }

        /// <summary>
        /// This action calculate tax  by amount and postalCode
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="postalCode"></param>
        /// <returns></returns>
        [Route("Calculator")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CalculateTax(decimal amount, string postalCode)
        {
            decimal? amountTaxRate = 0.0M;

            var rates = _unitOfWork.TaxRate.GetAll(includeProperties: "TaxType");

            var taxType = _unitOfWork.PostalCodeTaxType
                .Get(filter: w => w.PostalCode!.Code.Equals(postalCode), includeProperties: "PostalCode,TaxType");

            var amountTaxRates = rates.FirstOrDefault(w => w.TaxTypeId == taxType.TaxTypeId 
            && amount  >= w.From && amount <=  w.To);
           
          
            if (amountTaxRates != null)
            {

                amountTaxRate  = amountTaxRates?.Rate;

            }
            else
            {
                amountTaxRate = 0.17m;
            }

            var taxedAmount = amount * amountTaxRate;
            var amountAfterTax = amount - taxedAmount;

            var results = new TaxCalculationViewModel
            {
                TaxType = taxType?.TaxType?.TaxCalculationType,
                AmountAfterTax = amountAfterTax?? 0.0M,
                TaxPercentage = $"{amountTaxRate * 100}%",
            };

            return Ok(results);
        }

        /// <summary>
        /// This action saves the calculation into the database
        /// </summary>
        /// <param name="calculation"></param>
        /// <returns></returns>
        [Route("Calculation")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult SaveCalculation(TaxCalculation calculation) 
        {
            _unitOfWork.TaxCalculation.Add(calculation);
            _unitOfWork.Save();

            return Created();
        }
    }
}
