using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.DBModels
{
    [Table("AppraisementContainerDetails")]
    public class AppraisementContainerDetails
    {
        [Key]
        public int Id { get; set; }
        public string? ContainerCBTNo { get; set; }
        public string? ICDCode { get; set; }
        public string? Size { get; set; }
        public string? FCL_LCL { get; set; }
        public string? ContainerCBTType { get; set; }
        public string? CargoType { get; set; }
        public string? RMS { get; set; }
        public string? LineNo { get; set; }
        public int? OBLNoId { get; set; }
        public DateTime? OBLDate { get; set; }
        public string? BOENo { get; set; }
        public DateTime? BOEDate { get; set; }
        public string? CHANameAddress { get; set; }
        public string? ImporterNameAddress { get; set; }
        public string? CargoDescription { get; set; }
        public decimal? CIFValue { get; set; }
        public decimal? Duty { get; set; }
        public int? NoOfPackages { get; set; }
        public decimal? GrossWeightKg { get; set; }
        public string? WithoutDOSealNo { get; set; }
        public int? CustomAppraisementId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }

        public int? chaId { get; set; }
        public int? importerId { get; set; }
    }
}
