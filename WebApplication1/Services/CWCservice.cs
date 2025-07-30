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

        public async Task<AddEditResponse> GetInvoiceDataFromSPAsync(GetInvoiceDtlforSAPRequest request, int invId)
        {
            AddEditResponse response = new AddEditResponse();
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
                var itemList = new List<Item>();

                // Read header
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

                // Read item list
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
                            SALES_ORDER_ITEM = reader["SALES_ORDER_ITEM"]?.ToString() ?? string.Empty,
                            PLACE_OF_SUPPLY = reader["PLACE_OF_SUPPLY"]?.ToString() ?? string.Empty
                        });
                    }
                }

                // Dispose reader and close connection
                await reader.DisposeAsync();
                await conn.CloseAsync();

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
                    response.Response = $"Error posting to CWC API: {ex.Message}";
                    return response;
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
                        await _dbContext.SaveChangesAsync();

                        // Update related invoice table if needed
                        if (!string.IsNullOrEmpty(dbResponse.STATUS) && request.YardInvoice)
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

                        response.Response = "success";
                        return response;
                    }
                    catch (Exception ex)
                    {
                        response.Response = $"Error saving SAP response: {ex.Message}";
                        return response;
                    }
                }

                response.Response = "No response from CWC API";
                return response;
            }
            catch (Exception ex)
            {
                response.Response = $"General error: {ex.Message}";
                return response;
            }
        }


        public async Task<AddEditResponse> GetReceiptDataFromSPAsync(GetCashReceiptDtlforSAPRequest request, int CashReceiptId)
        {
            AddEditResponse response = new AddEditResponse();
            try
            {
                var model = new RequestCWCapiReceipt
                {
                    REQUEST = new List<Request2>()
                };

                await using var conn = _dbContext.Database.GetDbConnection();
                await using var cmd = conn.CreateCommand();

                cmd.CommandText = "GetReceiptDtlforSAP";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@in_ReceiptNo", request.inReceiptNo ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@in_IsIRN", request.IsIRN));
                //cmd.Parameters.Add(new SqlParameter("@YardInvoice", request.YardInvoice));

                await conn.OpenAsync();
                await using var reader = await cmd.ExecuteReaderAsync();

                HeaderReceipt? header = null;
                var itemList = new List<ItemReceipt>();

                // Read header
                if (await reader.ReadAsync())
                {
                    header = new HeaderReceipt
                    {
                        DOC_NO = reader["DOC_NO"]?.ToString() ?? string.Empty,
                        USERNAME = reader["USERNAME"]?.ToString() ?? string.Empty,
                        HEADER_TXT = reader["HEADER_TXT"]?.ToString() ?? string.Empty,
                        COMP_CODE = reader["COMP_CODE"]?.ToString() ?? string.Empty,
                        DOC_DATE = reader["DOC_DATE"]?.ToString() ?? string.Empty,
                        PSTNG_DATE = reader["PSTNG_DATE"]?.ToString() ?? string.Empty,
                        FISC_YEAR = reader["FISC_YEAR"]?.ToString() ?? string.Empty,
                        FIS_PERIOD = reader["FIS_PERIOD"]?.ToString() ?? string.Empty,
                        DOC_TYPE = reader["DOC_TYPE"]?.ToString() ?? string.Empty,
                        REF_DOC_NO = reader["REF_DOC_NO"]?.ToString() ?? string.Empty,
                        CURRENCY = reader["CURRENCY"]?.ToString() ?? string.Empty,
                        NAME = reader["NAME"]?.ToString() ?? string.Empty,
                        NAME_2 = reader["NAME_2"]?.ToString() ?? string.Empty,
                        NAME_3 = reader["NAME_3"]?.ToString() ?? string.Empty,
                        NAME_4 = reader["NAME_4"]?.ToString() ?? string.Empty,
                        POSTL_CODE = reader["POSTL_CODE"]?.ToString() ?? string.Empty,
                        CITY = reader["CITY"]?.ToString() ?? string.Empty,
                        COUNTRY = reader["COUNTRY"]?.ToString() ?? string.Empty,
                        STREET = reader["STREET"]?.ToString() ?? string.Empty,
                        TAX_NO_1 = reader["TAX_NO_1"]?.ToString() ?? string.Empty,
                        TAX_NO_2 = reader["TAX_NO_2"]?.ToString() ?? string.Empty,
                        TAX_NO_3 = reader["TAX_NO_3"]?.ToString() ?? string.Empty,
                        TAX_NO_4 = reader["TAX_NO_4"]?.ToString() ?? string.Empty
                    };
                }

                // Read item list
                if (await reader.NextResultAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        itemList.Add(new ItemReceipt
                        {
                            DOC_NO = reader["DOC_NO"]?.ToString() ?? string.Empty,

                            // GL-related fields
                            GL_ITEMNO_ACC = reader["GL_ITEMNO_ACC"]?.ToString() ?? string.Empty,
                            GL_ACCOUNT = reader["GL_ACCOUNT"]?.ToString() ?? string.Empty,
                            GL_ITEM_TEXT = reader["GL_ITEM_TEXT"]?.ToString() ?? string.Empty,
                            GL_TAX_CODE = reader["GL_TAX_CODE"]?.ToString() ?? string.Empty,
                            GL_REF_KEY_1 = reader["GL_REF_KEY_1"]?.ToString() ?? string.Empty,
                            GL_REF_KEY_2 = reader["GL_REF_KEY_2"]?.ToString() ?? string.Empty,
                            GL_REF_KEY_3 = reader["GL_REF_KEY_3"]?.ToString() ?? string.Empty,
                            GL_PROFIT_CTR = reader["GL_PROFIT_CTR"]?.ToString() ?? string.Empty,
                            GL_COSTCENTER = reader["GL_COSTCENTER"]?.ToString() ?? string.Empty,
                            GL_DT_CT_INDICATOR = reader["GL_DT_CT_INDICATOR"]?.ToString() ?? string.Empty,
                            GL_AMT_DOCCUR = reader["GL_AMT_DOCCUR"]?.ToString() ?? string.Empty,

                            // Customer-related fields
                            CUST_ITEMNO_ACC = reader["CUST_ITEMNO_ACC"]?.ToString() ?? string.Empty, // Same as GL_ITEMNO_ACC if shared
                            CUSTOMER = reader["CUSTOMER"]?.ToString() ?? string.Empty,
                            RECON_GL_ACCOUNT = reader["RECON_GL_ACCOUNT"]?.ToString() ?? string.Empty,
                            CUST_REF_KEY_1 = reader["CUST_REF_KEY_1"]?.ToString() ?? string.Empty,
                            CUST_REF_KEY_2 = reader["CUST_REF_KEY_2"]?.ToString() ?? string.Empty,
                            CUST_REF_KEY_3 = reader["CUST_REF_KEY_3"]?.ToString() ?? string.Empty,
                            CUST_SP_GL_IND = reader["CUST_SP_GL_IND"]?.ToString() ?? string.Empty,
                            CUST_ALLOC_NMBR = reader["CUST_ALLOC_NMBR"]?.ToString() ?? string.Empty,
                            CUST_BUSINESSPLACE = reader["CUST_BUSINESSPLACE"]?.ToString() ?? string.Empty,
                            CUST_SECTIONCODE = reader["CUST_SECTIONCODE"]?.ToString() ?? string.Empty,
                            CUST_AMT_DOCCUR = reader["CUST_AMT_DOCCUR"]?.ToString() ?? string.Empty, // Could differ from GL_AMT_DOCCUR if needed
                            CUST_PROFIT_CTR = reader["CUST_PROFIT_CTR"]?.ToString() ?? string.Empty,

                            // Payment-related field
                            CUST_PAYMT_REF = reader["CUST_PAYMT_REF"]?.ToString() ?? string.Empty // Or "SALES_ORDER_ITEM" if more accurate
                        });
                    }
                }

                // Dispose reader and close connection
                await reader.DisposeAsync();
                await conn.CloseAsync();

                model.REQUEST.Add(new Request2
                {
                    HEADER = header!,
                    ITEM = itemList
                });

                ResponseCWCapiReceipt? sapResponse;
                try
                {
                    sapResponse = await PostReceiptToCWCAsync(model);
                }
                catch (Exception ex)
                {
                    response.Response = $"Error posting to CWC API: {ex.Message}";
                    return response;
                }

                if (sapResponse?.Response != null)
                {
                    try
                    {
                        var dbResponse = new SapResponse
                        {
                            InvoiceId = CashReceiptId,
                            InvoiceNo = request.inReceiptNo ?? string.Empty,
                            SAP_DOC_NUMBER = sapResponse.Response.SAPDocNumber,
                            REF_DOC_NO = sapResponse.Response.RefDocNo,
                            STATUS = sapResponse.Response.Status,
                            REMARK = sapResponse.Response.Remark,
                            CreatedBy = 0,
                            CreatedOn = DateTime.UtcNow,
                            Module = "CWC",
                            InvoiceType = "PRCP"
                        };

                        _dbContext.SapResponse.Add(dbResponse);
                        await _dbContext.SaveChangesAsync();

                        // Update related invoice table if needed
                        if (!string.IsNullOrEmpty(dbResponse.STATUS))
                        {
                            var PReceipt = await _dbContext.GetCashReceiptHdr
                                .FirstOrDefaultAsync(x => x.CashReceiptId == dbResponse.InvoiceId);

                            if (PReceipt != null)
                            {
                                PReceipt.SAP_DOC_NUMBER = dbResponse.SAP_DOC_NUMBER;
                                PReceipt.IsSAP = 1;
                                PReceipt.UpdatedOn = DateTime.Now;

                                await _dbContext.SaveChangesAsync();
                            }
                        }

                        response.Response = "success";
                        return response;
                    }
                    catch (Exception ex)
                    {
                        response.Response = $"Error saving SAP response: {ex.Message}";
                        return response;
                    }
                }

                response.Response = "No response from CWC API";
                return response;
            }
            catch (Exception ex)
            {
                response.Response = $"General error: {ex.Message}";
                return response;
            }
        }


        public async Task<ResponseCWCapiReceipt> PostReceiptToCWCAsync(RequestCWCapiReceipt request)
        {
            try
            {
                string url = _configuration["CWCApi:CustomerReceiptUrl"];
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

                var result = JsonSerializer.Deserialize<ResponseCWCapiReceipt>(responseJson, new JsonSerializerOptions
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


        public async Task<AddEditResponse> GetCreditNoteDataFromSPAsync(GetCreditNoteforSAPRequest request, int creditNoteId)
        {
            AddEditResponse response = new AddEditResponse();
            try
            {
                var model = new RequestCWCapiCreditNote
                {
                    REQUEST1 = new List<Request3>()
                };

                await using var conn = _dbContext.Database.GetDbConnection();
                await using var cmd = conn.CreateCommand();

                cmd.CommandText = "GetCrDrDtlforSAP";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@in_InvoiceNo", request.inInvoiceNo ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@in_IsIRN", request.IsIRN));
                //cmd.Parameters.Add(new SqlParameter("@YardInvoice", request.YardInvoice));

                await conn.OpenAsync();
                await using var reader = await cmd.ExecuteReaderAsync();

                HeaderCreditNote? header = null;
                var itemList = new List<ItemCreditNote>();

                // Read header
                if (await reader.ReadAsync())
                {
                    header = new HeaderCreditNote
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

                // Read item list
                if (await reader.NextResultAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        itemList.Add(new ItemCreditNote
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
                            DOC_CURRRENCY = reader["DOC_CURRENCY"]?.ToString() ?? string.Empty,
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

                // Dispose reader and close connection
                await reader.DisposeAsync();
                await conn.CloseAsync();

                model.REQUEST1.Add(new Request3
                {
                    HEADER = header!,
                    ITEM = itemList
                });

                ResponseCWCapi? sapResponse;
                try
                {
                    sapResponse = await PostCreditNoteToCWCAsync(model);
                }
                catch (Exception ex)
                {
                    response.Response = $"Error posting to CWC API: {ex.Message}";
                    return response;
                }

                if (sapResponse?.Response1 != null)
                {
                    try
                    {
                        var dbResponse = new SapResponse
                        {
                            InvoiceId = creditNoteId,
                            InvoiceNo = request.inInvoiceNo ?? string.Empty,
                            SAP_DOC_NUMBER = sapResponse.Response1.SAPDocNumber,
                            REF_DOC_NO = sapResponse.Response1.RefDocNo,
                            STATUS = sapResponse.Response1.Status,
                            REMARK = sapResponse.Response1.Remark,
                            CreatedBy = 0,
                            CreatedOn = DateTime.UtcNow,
                            Module = "CWC",
                            InvoiceType = "CRNT"
                            //InvoiceType = request.YardInvoice == true ? "YINV" : "GDINV"
                        };

                        _dbContext.SapResponse.Add(dbResponse);
                        await _dbContext.SaveChangesAsync();

                        // Update related invoice table if needed
                        if (!string.IsNullOrEmpty(dbResponse.STATUS) )
                        {
                            var creditNote = await _dbContext.CreditNote
                                .FirstOrDefaultAsync(x => x.CreditNoteId == dbResponse.InvoiceId);

                            if (creditNote != null)
                            {
                                creditNote.SAP_DOC_NUMBER = dbResponse.SAP_DOC_NUMBER;
                                creditNote.IsSAP = 1;
                                creditNote.UpdatedAt = DateTime.Now;

                                await _dbContext.SaveChangesAsync();
                            }
                        }

                        response.Response = "success";
                        return response;
                    }
                    catch (Exception ex)
                    {
                        response.Response = $"Error saving SAP response: {ex.Message}";
                        return response;
                    }
                }

                response.Response = "No response from CWC API";
                return response;
            }
            catch (Exception ex)
            {
                response.Response = $"General error: {ex.Message}";
                return response;
            }
        }


        public async Task<ResponseCWCapi> PostCreditNoteToCWCAsync(RequestCWCapiCreditNote request)
        {
            try
            {
                string url = _configuration["CWCApi:CreditNoteUrl"];
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




    }
}