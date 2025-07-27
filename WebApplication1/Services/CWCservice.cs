using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SezApi.Data;
using SezApi.Model.DBModels;
using SezApi.Model.Request;
using SezApi.Model.Response;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SezApi.Services
{
    public class CWCservice
    {
        private readonly SezApiDbContext _dbContext;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CWCservice(HttpClient httpClient, IConfiguration configuration, SezApiDbContext db)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _dbContext = db;
        }

        public async Task<ResponseCWCapi> PostInvoiceToCWCAsync(RequestCWCapi request)
        {
            try
            {
                string url = _configuration["CWCApi:BaseUrl"];
                string user = _configuration["CWCApi:UserId"];
                string pwd = _configuration["CWCApi:Password"];

                string json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{user}:{pwd}"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

                var httpResponse = await _httpClient.PostAsync(url, content);
                var responseJson = await httpResponse.Content.ReadAsStringAsync();

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new Exception($"CWC API returned error: {httpResponse.StatusCode} - {responseJson}");
                }

                var result = JsonSerializer.Deserialize<ResponseCWCapi>(responseJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while calling CWC API: {ex.Message}", ex);
            }
        }

        public async Task<SapResponse?> GetInvoiceDataFromSPAsync(GetInvoiceDtlforSAPRequest request, int invId)
        {
            try
            {
                var model = new RequestCWCapi
                {
                    REQUEST1 = new List<Request1>()
                };

                await using var conn = _dbContext.Database.GetDbConnection();
                await using var cmd = conn.CreateCommand();

                cmd.CommandText = "GetInvoiceDtlforSAP";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@in_InvoiceNo", request.InvoiceNo ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@in_IsIRN", request.IsIRN));
                cmd.Parameters.Add(new SqlParameter("@YardInvoice", request.YardInvoice));

                await conn.OpenAsync();
                await using var reader = await cmd.ExecuteReaderAsync();

                Header? header = null;
                if (await reader.ReadAsync())
                {
                    header = new Header
                    {
                        LINE_NO = reader["LINE_NO"]?.ToString() ?? string.Empty,
                        HEADER_TXT = reader["HEADER_TXT"]?.ToString() ?? string.Empty,
                        REF_DOC_NO = reader["REF_DOC_NO"]?.ToString() ?? string.Empty,
                        COMP_CODE = reader["COMP_CODE"]?.ToString() ?? string.Empty,
                        DOC_DATE = reader["DOC_DATE"]?.ToString() ?? string.Empty,
                        PSTNG_DATE = reader["PSTNG_DATE"]?.ToString() ?? string.Empty,
                        FISC_YEAR = reader["FISC_YEAR"]?.ToString() ?? string.Empty,
                        DOC_TYPE = reader["DOC_TYPE"]?.ToString() ?? string.Empty,
                        IRN_NO = reader["IRN_NO"]?.ToString() ?? string.Empty,
                        QR_CODE = reader["QR_CODE"]?.ToString() ?? string.Empty,
                        IRN_ACKN_NO = reader["IRN_ACKN_NO"]?.ToString() ?? string.Empty,
                        IRN_ACKN_DATE = reader["IRN_ACKN_DATE"]?.ToString() ?? string.Empty
                    };
                }

                var itemList = new List<Item>();
                if (await reader.NextResultAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        itemList.Add(new Item
                        {
                            LINE_NO = reader["LINE_NO"]?.ToString() ?? string.Empty,
                            ITEMNO_ACC = reader["ITEMNO_ACC"]?.ToString() ?? string.Empty,
                            GL_ACCOUNT = reader["GL_ACCOUNT"]?.ToString() ?? string.Empty,
                            PROFITSEG = reader["PROFITSEG"]?.ToString() ?? string.Empty,
                            C_CTR_AREA = reader["C_CTR_AREA"]?.ToString() ?? string.Empty,
                            VENDOR_NO = reader["VENDOR_NO"]?.ToString() ?? string.Empty,
                            CUSTOMER = reader["CUSTOMER"]?.ToString() ?? string.Empty,
                            CUST_RECON_ACCOUNT = reader["CUST_RECON_ACCOUNT"]?.ToString() ?? string.Empty,
                            SP_GL_IND = reader["SP_GL_IND"]?.ToString() ?? string.Empty,
                            WBS_ELEMENT = reader["WBS_ELEMENT"]?.ToString() ?? string.Empty,
                            COSTCENTER = reader["COSTCENTER"]?.ToString() ?? string.Empty,
                            ORDERID = reader["ORDERID"]?.ToString() ?? string.Empty,
                            PROFITCENTER = reader["PROFITCENTER"]?.ToString() ?? string.Empty,
                            ALLOC_NUMBER = reader["ALLOC_NUMBER"]?.ToString() ?? string.Empty,
                            ITEM_TEXT = reader["ITEM_TEXT"]?.ToString() ?? string.Empty,
                            BUSINESSPLACE = reader["BUSINESSPLACE"]?.ToString() ?? string.Empty,
                            SECTION_CODE = reader["SECTION_CODE"]?.ToString() ?? string.Empty,
                            DT_CT_INDICATOR = reader["DT_CT_INDICATOR"]?.ToString() ?? string.Empty,
                            AMT_DOCCUR = reader["AMT_DOCCUR"]?.ToString() ?? string.Empty,
                            DOC_CURRENCY = reader["DOC_CURRENCY"]?.ToString() ?? string.Empty,
                            AMT_LOCCUR = reader["AMT_LOCCUR"]?.ToString() ?? string.Empty,
                            TAX_CODE = reader["TAX_CODE"]?.ToString() ?? string.Empty,
                            HSN_SAC = reader["HSN_SAC"]?.ToString() ?? string.Empty,
                            WITHHOLD_TAX_TYPE = reader["WITHHOLD_TAX_TYPE"]?.ToString() ?? string.Empty,
                            WITHHOLD_TAX_CODE = reader["WITHHOLD_TAX_CODE"]?.ToString() ?? string.Empty,
                            TDS_BASE_AMOUNT = reader["TDS_BASE_AMOUNT"]?.ToString() ?? string.Empty,
                            FUND = reader["FUND"]?.ToString() ?? string.Empty,
                            VALUE_DATE = reader["VALUE_DATE"]?.ToString() ?? string.Empty,
                            SALES_ORDER = reader["SALES_ORDER"]?.ToString() ?? string.Empty,
                            SALES_ORDER_ITEM = reader["SALES_ORDER_ITEM"]?.ToString() ?? string.Empty
                        });
                    }
                }

                model.REQUEST1.Add(new Request1
                {
                    HEADER = header!,
                    ITEM = itemList
                });

                ResponseCWCapi? sapResponse;
                try
                {
                    sapResponse = await PostInvoiceToCWCAsync(model);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to post invoice to CWC API: {ex.Message}", ex);
                }

                if (sapResponse?.Response1 != null)
                {
                    try
                    {
                        var dbResponse = new SapResponse
                        {
                            InvoiceId = invId,
                            InvoiceNo = request.InvoiceNo ?? string.Empty,
                            SAP_DOC_NUMBER = sapResponse.Response1.SAPDocNumber,
                            REF_DOC_NO = sapResponse.Response1.RefDocNo,
                            STATUS = sapResponse.Response1.Status,
                            REMARK = sapResponse.Response1.Remark,
                            CreatedBy = 0,
                            CreatedOn = DateTime.UtcNow,
                            Module = "CWC",
                            InvoiceType = request.YardInvoice == true ? "YINV" : "GDINV"
                        };

                        _dbContext.SapResponse.Add(dbResponse);
                        var status = await _dbContext.SaveChangesAsync();

                        if (!string.IsNullOrEmpty(dbResponse.STATUS))
                        {
                            if (request.YardInvoice)
                            {
                                var invoice = await _dbContext.GetYardInvoiceList
                                    .FirstOrDefaultAsync(x => x.YardInvId == dbResponse.InvoiceId);

                                if (invoice != null)
                                {
                                    invoice.SAP_DOC_NUMBER = dbResponse.SAP_DOC_NUMBER;
                                    invoice.IsSAP = 1;
                                    invoice.UpdatedAt = DateTime.Now;

                                    await _dbContext.SaveChangesAsync();
                                }
                            }
                        }

                        return dbResponse;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Failed to save SAP response to database: {ex.Message}", ex);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in GetInvoiceDataFromSPAsync: {ex.Message}", ex);
            }
        }
    }
}