namespace SezApi.Model.Response
{
    public class ResponseOBLEntryWithDetailsDto
    {
        public int Id { get; set; }
        public string ContainerCBTType { get; set; }
        public string ContainerCBTNo { get; set; }
        public string ContainerCBTSize { get; set; }
        public string IGMNo { get; set; }
        public DateTime? IGMDate { get; set; }
        public string TPNo { get; set; }
        public DateTime? TPDate { get; set; }
        public string MovementType { get; set; }
        public int? Port { get; set; }
        public int? Country { get; set; }
        public string ShippingLine { get; set; }

        // AdditionalDetails fields
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
    }
}
