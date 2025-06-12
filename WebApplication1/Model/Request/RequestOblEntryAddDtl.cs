using SezApi.Model.DBModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SezApi.Model.Request
{
    public class RequestOblEntryAddDtl
    {
        public int ID { get; set; }
        public int? AddID { get; set; }
        public int? IcesContId { get; set; }
        public string OBL_HBL_No { get; set; }
        public DateTime? OBL_HBL_Date { get; set; }
        public string SMTP_No { get; set; }
        public DateTime? SMTP_Date { get; set; }
        public string Cargo_Desc { get; set; }
        public string Commodity { get; set; }
        public string Cargo_Type { get; set; }
        public int? No_of_PKG { get; set; }
        public string PKG_Type { get; set; }
        public decimal? GR_WT_Kg { get; set; }
        public string Importer_Name { get; set; }
        public string IGM_Importer_Name { get; set; }
        public bool? IsProcessed { get; set; }
        public int? OBLEntryId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
