using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("mstgroundrent")]
    public class MstGroundRent
    {
        [Key]
        public int GroundRentId { get; set; }
        public int? ContainerType { get; set; }
        public int? CommodityType { get; set; }
        public int? DaysRangeFrom { get; set; }
        public int? DaysRangeTo { get; set; }
        public decimal? RentAmount { get; set; }
        public decimal? ElectricityCharge { get; set; }
        public string Size { get; set; }
        public int? OperationType { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int? BranchId { get; set; }
        public string SacCode { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string FclLcl { get; set; }
    }
}
