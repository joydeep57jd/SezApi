using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("mstEximTraderMaster")]
    public class MstEximTraderMaster
    {
        [Key]
        public int TraderId { get; set; }

        [MaxLength(50)]
        public string OperationType { get; set; }

        [MaxLength(255)]
        public string? EximTraderName { get; set; }

        [MaxLength(50)]
        public string? EximTraderAlias { get; set; }

        public string? Address { get; set; }

        [MaxLength(100)]
        public string? CountryName { get; set; }

        [MaxLength(100)]
        public string? StateName { get; set; }

        [MaxLength(100)]
        public string? CityName { get; set; }

        [MaxLength(10)]
        public string? Pincode { get; set; }

        [MaxLength(20)]
        public string? PhoneNo { get; set; }

        [MaxLength(20)]
        public string? FaxNo { get; set; }

        [MaxLength(100)]
        public string? ContactPerson { get; set; }

        [MaxLength(100)]
        public string? EmailId { get; set; }

        [MaxLength(20)]
        public string? MobileNo { get; set; }

        [MaxLength(20)]
        public string? PAN { get; set; }

        [MaxLength(20)]
        public string? AadhaarNo { get; set; }

        [MaxLength(20)]
        public string? GSTNo { get; set; }

        [MaxLength(20)]
        public string? TAN { get; set; }

        [MaxLength(50)]
        public string? SapCustomerNo { get; set; }
    }
}
