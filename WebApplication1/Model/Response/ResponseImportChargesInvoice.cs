using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
    public class FlatImportChargesRow
    {
        // Header Info
        [Key]
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string EmailAddress { get; set; }
        public string CWCGSTNO { get; set; }

        public string InvNo { get; set; }
        public DateTime InvDate { get; set; }

        public string PartyName { get; set; }
        public string PartyAddress { get; set; }
        public string PartyGST { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }

        public string PlaceOfSupply { get; set; }
        public bool IsService { get; set; }
        public string PayerName { get; set; }
        public string Remarks { get; set; }
        public string PrintedBy { get; set; }

        // Container Info
        public string ICDNo { get; set; }
        public string ContainerCBTNo { get; set; }
        public string Size { get; set; }
        public bool Reefer { get; set; }
        public string OBLHBLNo { get; set; }
        public string CargoType { get; set; }
        public int NoOfPackage { get; set; }
        public decimal GrWt { get; set; }
        public DateTime DoValidateDate { get; set; }

        // Charge Info
        public string ChargeCode { get; set; }
        public string Descripton { get; set; }
        public string SACCode { get; set; }

        public decimal Rate { get; set; }
        public decimal TaxableAmt { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal CGSTAmt { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal SGSTAmt { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal IGSTAmt { get; set; }
        public decimal Total { get; set; }
    }

    public class ResponseImportChargesInvoice
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string EmailAddress { get; set; }
        public string CWCGSTNO { get; set; }

        public string InvNo { get; set; }
        public DateTime InvDate { get; set; }

        public string PartyName { get; set; }
        public string PartyAddress { get; set; }
        public string PartyGST { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }

        public string PlaceOfSupply { get; set; }
        public bool IsService { get; set; }
        public string PayerName { get; set; }
        public string Remarks { get; set; }
        public string PrintedBy { get; set; }
        public List<ContainerChargeDto> ContainerCharges { get; set; }
        public List<ChargeDetailDto> Charges { get; set; }
    }
    public class ContainerChargeDto
    {
        public string ICDNo { get; set; }
        public string ContainerCBTNo { get; set; }
        public string Size { get; set; }
        public bool Reefer { get; set; }
        public string OBLHBLNo { get; set; }
        public string CargoType { get; set; }
        public int NoOfPackage { get; set; }
        public decimal GrWt { get; set; }
        public DateTime DoValidateDate { get; set; }
    }
    public class ChargeDetailDto
    {
        public string ChargeCode { get; set; }
        public string Descripton { get; set; }
        public string SACCode { get; set; }

        public decimal Rate { get; set; }
        public decimal TaxableAmt { get; set; }

        public decimal CGSTRate { get; set; }
        public decimal CGSTAmt { get; set; }

        public decimal SGSTRate { get; set; }
        public decimal SGSTAmt { get; set; }

        public decimal IGSTRate { get; set; }
        public decimal IGSTAmt { get; set; }

        public decimal Total { get; set; }
    }
}
