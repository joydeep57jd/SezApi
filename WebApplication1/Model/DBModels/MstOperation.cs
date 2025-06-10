using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SezApi.Model.DBModels
{
    [Table("mstoperation")]
    public class MstOperation
    {
        [Key]
        public int OperationId { get; set; }

        public int? BranchId { get; set; }

        public string OperationType { get; set; }

        public string OperationCode { get; set; }

        public int? SacId { get; set; }

        public string OperationSDesc { get; set; }

        public string OperationDesc { get; set; }

        public int? ClauseOrder { get; set; }

        public string PkgCount { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
