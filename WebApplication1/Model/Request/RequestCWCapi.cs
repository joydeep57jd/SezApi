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
    public class GetCashReceiptDtlforSAPRequest
    {
        public string inReceiptNo { get; set; }
        public int IsIRN { get; set; }
        //public bool YardInvoice { get; set; }
    }
    public class GetCreditNoteforSAPRequest
    {
        public string inInvoiceNo { get; set; }
        public int IsIRN { get; set; }
        //public bool YardInvoice { get; set; }
    }

    public class RequestCWCapiReceipt
    {
        [JsonPropertyName("REQUEST")]
        public List<Request2> REQUEST { get; set; }
    }
    public class Request2
    {
        [JsonPropertyName("HEADER")]
        public HeaderReceipt HEADER { get; set; }

        [JsonPropertyName("ITEM")]
        public List<ItemReceipt> ITEM { get; set; }
    }
    public class HeaderReceipt
    {
        public string DOC_NO { get; set; }
        public string USERNAME { get; set; }
        public string HEADER_TXT { get; set; }
        public string COMP_CODE { get; set; }
        public string DOC_DATE { get; set; }
        public string PSTNG_DATE { get; set; }
        public string FISC_YEAR { get; set; }
        public string FIS_PERIOD { get; set; }
        public string DOC_TYPE { get; set; }
        public string REF_DOC_NO { get; set; }
        public string CURRENCY { get; set; }
        public string NAME { get; set; }
        public string NAME_2 { get; set; }
        public string NAME_3 { get; set; }
        public string NAME_4 { get; set; }
        public string POSTL_CODE { get; set; }
        public string CITY { get; set; }
        public string COUNTRY { get; set; }
        public string STREET { get; set; }
        public string TAX_NO_1 { get; set; }
        public string TAX_NO_2 { get; set; }
        public string TAX_NO_3 { get; set; }
        public string TAX_NO_4 { get; set; }
       
    }
    public class ItemReceipt
    {
        public string DOC_NO { get; set; }
        public string GL_ITEMNO_ACC { get; set; }
        public string GL_ACCOUNT { get; set; }
        public string GL_ITEM_TEXT { get; set; }
        public string GL_TAX_CODE { get; set; }
        public string GL_REF_KEY_1 { get; set; }
        public string GL_REF_KEY_2 { get; set; }
        public string GL_REF_KEY_3 { get; set; }
        public string GL_PROFIT_CTR { get; set; }
        public string GL_COSTCENTER { get; set; }
        public string GL_DT_CT_INDICATOR { get; set; }
        public string GL_AMT_DOCCUR { get; set; }
       public string CUST_ITEMNO_ACC { get; set; }
        public string CUSTOMER { get; set; }
        public string RECON_GL_ACCOUNT { get; set; }
        public string CUST_REF_KEY_1 { get; set; }
         public string CUST_REF_KEY_2 { get; set; }
        public string CUST_REF_KEY_3 { get; set; }
       public string CUST_SP_GL_IND { get; set; }
        public string CUST_ALLOC_NMBR { get; set; }
        public string CUST_BUSINESSPLACE { get; set; }
        public string CUST_SECTIONCODE { get; set; }
        public string CUST_AMT_DOCCUR { get; set; }
        public string CUST_PROFIT_CTR { get; set; }
        public string CUST_PAYMT_REF { get; set; }
        
    }

    public class RequestCWCapiCreditNote
    {
        [JsonPropertyName("REQUEST1")]
        public List<Request3> REQUEST1 { get; set; }
    }
    public class Request3
    {
        [JsonPropertyName("HEADER")]
        public HeaderCreditNote HEADER { get; set; }

        [JsonPropertyName("ITEM")]
        public List<ItemCreditNote> ITEM { get; set; }
    }
    public class HeaderCreditNote
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
    public class ItemCreditNote
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
        public string DOC_CURRRENCY { get; set; }
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

    }

}
