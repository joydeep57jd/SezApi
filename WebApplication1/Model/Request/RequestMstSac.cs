namespace SezApi.Model.Request
{
    public class RequestMstSac
    {
        public int SacId { get; set; }
        public int? BranchId { get; set; }
        public string SacCode { get; set; }
        public string Description { get; set; }
        public decimal? Gst { get; set; }
        public decimal? Cess { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
