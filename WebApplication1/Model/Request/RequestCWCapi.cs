using Azure.Core;
using System.Reflection.PortableExecutable;
using System.Text.Json.Serialization;

namespace SezApi.Model.Request
{
    public class RequestCWCapi
    {
        [JsonPropertyName("REQUEST1")]
        public List<Request1> REQUEST1 { get; set; }
    }
    public class Request1
    {
        [JsonPropertyName("HEADER")]
        public Header HEADER { get; set; }

        [JsonPropertyName("ITEM")]
        public List<Item> ITEM { get; set; }
    }
    public class Header
    {
        public string LINE_NO { get; set; }
        public string HEADER_TXT { get; set; }
        public string REF_DOC_NO { get; set; }
        public string COMP_CODE { get; set; }
        public string DOC_DATE { get; set; }
        public string PSTNG_DATE { get; set; }
        public string FISC_YEAR { get; set; }
        public string DOC_TYPE { get; set; }
        public string IRN_NO { get; set; }
        public string QR_CODE { get; set; }
        public string IRN_ACKN_NO { get; set; }
        public string IRN_ACKN_DATE { get; set; }
    }
    public class Item
    {
        public string LINE_NO { get; set; }
        public string ITEMNO_ACC { get; set; }
        public string GL_ACCOUNT { get; set; }
        public string PROFITSEG { get; set; }
        public string C_CTR_AREA { get; set; }
        public string VENDOR_NO { get; set; }
        public string CUSTOMER { get; set; }
        public string CUST_RECON_ACCOUNT { get; set; }
        public string SP_GL_IND { get; set; }
        public string WBS_ELEMENT { get; set; }
        public string COSTCENTER { get; set; }
        public string ORDERID { get; set; }
        public string PROFITCENTER { get; set; }
        public string ALLOC_NUMBER { get; set; }
        public string ITEM_TEXT { get; set; }
        public string BUSINESSPLACE { get; set; }
        public string SECTION_CODE { get; set; }
        public string DT_CT_INDICATOR { get; set; }
        public string AMT_DOCCUR { get; set; }
        public string DOC_CURRENCY { get; set; }
        public string AMT_LOCCUR { get; set; }
        public string TAX_CODE { get; set; }
        public string HSN_SAC { get; set; }
        public string WITHHOLD_TAX_TYPE { get; set; }
        public string WITHHOLD_TAX_CODE { get; set; }
        public string TDS_BASE_AMOUNT { get; set; }
        public string FUND { get; set; }
        public string VALUE_DATE { get; set; }
        public string SALES_ORDER { get; set; }
        public string SALES_ORDER_ITEM { get; set; }
        public string PLACE_OF_SUPPLY { get; set; }
    }

    public class GetInvoiceDtlforSAPRequest
    {
        public string InvoiceNo { get; set; }     
        public int IsIRN { get; set; }             
        public bool YardInvoice { get; set; }      
    }
}
