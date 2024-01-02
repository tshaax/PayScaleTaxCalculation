using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PayScale.BusinessLayer.BusinessLayer;
using PayScale.DataAccess.Repository.IRepository;
using PayScale.Models;
using PayScale.Models.ViewModels;


namespace PayScale.API.Controllers.v1
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
        private readonly IBusinessLogic _businessLogic;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="cache"></param>
        public TaxController(IUnitOfWork unitOfWork, IBusinessLogic businessLogic, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _businessLogic = businessLogic;
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
        /// This action gets taxes by postal code Id
        /// </summary>
        /// <param name="postalCodeId"></param>
        /// <returns></returns>
        [Route("ByPostalCodeId")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetPostalCodesById(int postalCodeId)
        {

            var key = $"{nameof(GetPostalCodesById)}_{postalCodeId}";

            if (_cache.TryGetValue(key, out PostalCodeTaxType? data))
            {
                return Ok(data);
            }

            data = _unitOfWork.PostalCodeTaxType.Get(filter: w => w.PostalCode!.Id.Equals(postalCodeId), includeProperties: "PostalCode,TaxType");

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

            _cache.Set(key, data, cacheOptions);


            return Ok(data.PostalCode);
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
            var response = _businessLogic.TaxCalculationLogic(amount, postalCode);
            return Ok(response);
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
          
           
            var calcTaxAmount = _businessLogic.TaxCalculationLogic(calculation.Amount, calculation.PostalCodeId);

            calculation.AmountAfterTax = calcTaxAmount.AmountAfterTax;

            _unitOfWork.TaxCalculation.Add(calculation);
            _unitOfWork.Save();

            var routeValues = new { id = calculation.Id };

            var response = _businessLogic.TaxCalculationLogic(routeValues.id);

            

            return CreatedAtAction(nameof(TaxCalculationById),routeValues, response);
        }

        /// <summary>
        /// This action gets calculated taxes by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("TaxCalculationById")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult TaxCalculationById(int id)
        {

            var key = $"{nameof(TaxCalculationById)}_{id}";

            if (_cache.TryGetValue(key, out TaxCalculationViewModel? data))
            {
                return Ok(data);
            }

            data = _businessLogic.TaxCalculationLogic(id);

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

            _cache.Set(key, data, cacheOptions);


            return Ok(data);
        }

        private TaxCalculationViewModel TaxCalculationLogic(int id)
        {
            TaxCalculationViewModel? data;
            var taxCalc = _unitOfWork.TaxCalculation.Get(filter: w => w.Id.Equals(id));

            var taxType = _unitOfWork.PostalCodeTaxType.Get(filter: w => w.PostalCode!.Id.Equals(taxCalc.PostalCodeId), includeProperties: "PostalCode,TaxType");
            var rates = _unitOfWork.TaxRate.Get(filter: w => w.TaxTypeId.Equals(taxType.Id), includeProperties: "TaxType");
            data = new TaxCalculationViewModel
            {
                AmountAfterTax = taxCalc.AmountAfterTax,
                TaxPercentage = $"{rates.Rate * 100}%",
                TaxType = rates.TaxType.TaxCalculationType,
            };
            return data;
        }
    }
}
