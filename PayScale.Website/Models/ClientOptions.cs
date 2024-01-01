namespace PayScale.Website.Models
{
    public class ClientOptions
    {
        public const string PayScaleAPI = "PayScaleAPI";

        public string Url { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;

        public ClientEndPoints? Endpoints { get; set; } 
    }

    public class ClientEndPoints
    {
        public const string Endpoints = "Endpoints";

        public string GetPostalCodes { get; set; } = string.Empty;
        public string GetTaxByPostalCode { get; set; } = string.Empty;
        public string GetTaxCalculator { get; set; } = string.Empty;
        public string PostCalculation { get; set; } = string.Empty;

    }
}
