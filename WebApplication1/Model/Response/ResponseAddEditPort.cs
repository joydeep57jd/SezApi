namespace SezApi.Model.Response
{
    public class ResponseAddEditPort
    {
        public int PortId { get; set; }
        public string PortName { get; set; }
        public string PortAlias { get; set; }
        public bool POD { get; set; }
        public int? Country { get; set; }
        public int? State { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CountryName { get; set; } 
        public string StateName { get; set; } 
    }
}
