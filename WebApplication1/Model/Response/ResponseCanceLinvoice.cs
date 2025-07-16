using System.ComponentModel.DataAnnotations;

namespace SezApi.Model.Response
{
    public class ResponseCanceLinvoice
    {
            public int Id { get; set; }
            public int? invId { get; set; }
            public string? InvoiceNo { get; set; }
            public string? Remarks { get; set; }
            public string? cancelReason { get; set; }
            public DateTime? CancelledDate { get; set; }

            public string? Amount { get; set; }

            public DateTime? invoiceDate { get; set; }
            
            public string? PartyName { get; set; }   


    }
}
