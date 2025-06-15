
namespace MyPdfFillerApi.Models
{
    public class FormData
    {
        // Company Information
        public string CompanyName { get; set; } = "";
        public string CompanyCnpj { get; set; } = "";
        public string CompanyAddress { get; set; } = "";
        public string CompanyCity { get; set; } = "";
        public string CompanyEmail { get; set; } = "";

        // Person Information
        public string PersonName { get; set; } = "";
        public string PersonCpf { get; set; } = "";
        public string PersonAddress { get; set; } = "";
        public string PersonCity { get; set; } = "";
        public string PersonEmail { get; set; } = "";

        // Vehicle/Transaction Information
        public string VehicleId { get; set; } = "";
        public string TransactionId { get; set; } = "";
        public string Value { get; set; } = "";

        // Date Information
        public string Day { get; set; } = "";
        public string Month { get; set; } = "";
        public string Year { get; set; } = "";
        public string DocumentCity { get; set; } = "";
        public string MonthName { get; set; } = "";
        public string SignatureDay { get; set; } = "";
        public string SignatureYear { get; set; } = "";
    }
}
