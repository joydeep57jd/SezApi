using System.Text.Json.Serialization;

namespace SezApi.Model.Response
{
        public class ResponseCWCapi
        {
            [JsonPropertyName("RESPONSE1")]
            public Response1 Response1 { get; set; }
        }

        public class Response1
        {
            [JsonPropertyName("SAP_DOC_NUMBER")]
            public string SAPDocNumber { get; set; }

            [JsonPropertyName("REF_DOC_NO")]
            public string RefDocNo { get; set; }

            [JsonPropertyName("STATUS")]
            public string Status { get; set; }

            [JsonPropertyName("REMARK")]
            public string Remark { get; set; }
        }
    
}
