
using Azure.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SezApi.Data;
using SezApi.Model.DBModels;
using SezApi.Model.Request;
using SezApi.Model.Response;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static SezApi.Model.Response.ResponseYardInvoiceFlat;
namespace SezApi.Services
{
    public class Services : IServices
    {
        private readonly SezApiDbContext _db;

        public Services(SezApiDbContext db)
        {
            _db = db;
        }

        public async Task AddTest(test product)
        {
            try
            {
                _db.test.Add(product);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<AddEditResponse> AddMststorageCharge(RequestMststorageCharge request)
        {
            var response = new AddEditResponse();
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                EXEC SP_AddMstStorageCharge
                    {request.StorageChargeId},
                    {request.BranchId},
                    {request.WarehouseType},
                    {request.ChargeType},
                    {request.RateSqMPerWeek},
                    {request.RateSqMeterPerMonth},
                    {request.RateMeterPerDay},
                    {request.RateCubMeterPerDay},
                    {request.RateCubMeterPerWeek},
                    {request.RateCubMeterPerMonth},
                    {request.EffectiveDate},
                    {request.DaysRangeFrom},
                    {request.DaysRangeTo},
                    {request.SacCode},
                    {request.CommodityType},
                    {request.CreatedBy},
                    {request.UpdatedBy},
                    {request.SurCharge}
            ").ToListAsync();

                response.Response = result.FirstOrDefault()?.Response ?? "No response";
            }
            catch (Exception ex)
            {
                response.Response = $"Error Occured {ex}";
            }

            return response;
        }

        public async Task<Response<List<mststoragecharge>>> GetMststorageCharge()
        {
            var response = new Response<List<mststoragecharge>>();
            try
            {
                var result = await _db.mststoragecharge.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<mststoragecharge>();
                response.Status = false;
                response.Message = $"Error Occured {ex}";
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditGetEntry(RequestGetEntry request)
        {
            try
            {
                var result = await _db.AddEditResponse
    .FromSqlInterpolated($@"
        EXEC dbo.Sp_AddEditGetEntry 
          @EntryId = {request.EntryId},
   @OperationName = {request.OperationName},
   @ReferenceNo = {request.ReferenceNo},
   @OperationType = {request.OperationType},
   @DeliveryType = {request.DeliveryType},
   @PartyId = {request.PartyId},
   @ShippingLine = {request.ShippingLine},
   @ContainerType = {request.ContainerType},
   @ContainerNo = {request.ContainerNo},
   @Size = {request.Size},
   @MaterialType = {request.MaterialType},
   @VehicleNo = {request.VehicleNo},
   @DriverName = {request.DriverName},
   @DriverLicenseNo = {request.DriverLicenseNo},
   @Remarks = {request.Remarks},
   @CreatedBy = {request.CreatedBy},
   @UpdatedBy = {request.UpdatedBy},
   @CFSNo = {request.CFSNo},
   @GateinDate = {request.GateinDate},
   @Reefer = {request.Reefer}
            ")
            .AsNoTracking()
            .ToListAsync();

                var response = result.FirstOrDefault();

                return response ?? new AddEditResponse { Response = "No response" };
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Failed to execute Sp_AddEditGetEntry", ex);
            }
        }

        public async Task<Response<List<GateEntry>>> GetAllEntries(int? page, int? size, string? ContainerNo)
        {
            var response = new Response<List<GateEntry>>();

            try
            {
                var query = _db.GetEntryList.AsQueryable();
                if (!string.IsNullOrEmpty(ContainerNo))
                {
                    query = query.Where(s => s.ContainerNo == ContainerNo);
                }
                var totalCount = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    int skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var data = await query.ToListAsync();

                response.Data = data;
                response.Status = true;
                response.TotalCount = totalCount;
            }
            catch (Exception ex)
            {
                response.Data = new List<GateEntry>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditMstOperation(RequestMstOperation request)
        {
            try
            {
                var result = await _db.AddEditResponse
                       .FromSqlInterpolated($@"
                          EXEC SP_AddMstOperation 
                          @OperationId = {request.OperationId},
                          @BranchId = {request.BranchId},
                          @OperationType = {request.OperationType},
                          @OperationCode = {request.OperationCode},
                          @SacId = {request.SacId},
                          @OperationSDesc = {request.OperationSDesc},
                          @OperationDesc = {request.OperationDesc},
                          @ClauseOrder = {request.ClauseOrder},
                          @PkgCount = {request.PkgCount},
                          @CreatedBy = {request.CreatedBy},
                          @UpdatedBy = {request.UpdatedBy}
                      ")
                  .AsNoTracking()
                  .ToListAsync();


                var response = result.FirstOrDefault();

                return response ?? new AddEditResponse { Response = "No response" };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEditMstOperation", ex);
            }
        }

        public async Task<Response<List<MstOperation>>> GetMstOperation(int? page, int? size)
        {
            var response = new Response<List<MstOperation>>();

            try
            {
                var query = _db.GetMstOperation.AsQueryable();

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var data = await query.ToListAsync();

                response.Data = data;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<MstOperation>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }


            return response;
        }

        public async Task<AddEditResponse> AddEditMstSac(RequestMstSac request) 
        {
            try
            {
                var result = await _db.AddEditResponse
                      .FromSqlInterpolated($@"
                      EXEC SP_AddMstSac 
                     @SacId = {request.SacId},
                     @BranchId = {request.BranchId},
                     @SacCode = {request.SacCode},
                     @Description = {request.Description},
                     @Gst = {request.Gst},
                     @Cess = {request.Cess},
                     @CreatedBy = {request.CreatedBy},
                     @UpdatedBy = {request.UpdatedBy}
                     ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response" };

            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEditMstOperation", ex);
            }
        }

        public async Task<Response<List<MstSac>>> GetMstSac()
        {
            var response = new Response<List<MstSac>>();

            try
            {
                var result = await _db.GetMstSac.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<MstSac>();
                response.Status = false;
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditMstEntryFee(RequestMstEntryFee request)
        {
            try
            {
                var result = await _db.AddEditResponse
            .FromSqlInterpolated($@"
            EXEC SP_AddEditMstEntryFee 
            @EntryFeeId = {request.EntryFeeId},
            @OperationType = {request.OperationType},
            @EffectiveDate = {request.EffectiveDate},
            @SacCodeId = {request.SacCodeId},
            @RatePerPacket = {request.RatePerPacket},
            @MinimumRate = {request.MinimumRate},
            @MaximumRate = {request.MaximumRate},
            @CreatedBy = {request.CreatedBy},
            @UpdatedBy = {request.UpdatedBy}
           ")
                      .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response" };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute SP_AddMstEntryFee", ex);
            }

        }
        
        public async Task<Response<List<MstEntryFee>>> GetMstEntryFee()
        {
            var response = new Response<List<MstEntryFee>>();

            try
            {
                var result = await _db.GetMstEntryFee.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<MstEntryFee>();
                response.Status = false;
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditHTCharges(HTChargesRequest request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                    EXEC dbo.Sp_AddEditHTCharges 
                        @HTChargesID = {request.HTChargesID},
                        @EffectiveDate = {request.EffectiveDate},
                        @SacCodeId = {request.SacCodeId},
                        @OperationType = {request.OperationType},
                        @RateperPacket = {request.RateperPacket},
                        @WeightForAdditionalCharges = {request.WeightForAdditionalCharges},
                        @RateForAdditionalCharges = {request.RateForAdditionalCharges},
                        @MinimumRate = {request.MinimumRate},
                        @CreatedBy = {request.CreatedBy},
                        @UpdatedBy = {request.UpdatedBy}
                       
                ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEntryHTCharges", ex);
            }
        }

        public async Task<Response<List<HTCharges>>> GetAllHTEntries(int? page, int? size)
        {
            var response = new Response<List<HTCharges>>();

            try
            {
                var query = _db.HTChargesList.AsQueryable();

                var totalRecords = await query.CountAsync();
                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var data = await query.ToListAsync();

                response.Data = data;
                response.Status = true;
                response.TotalCount = totalRecords;

            }
            catch (Exception ex)
            {
                response.Data = new List<HTCharges>();
                response.Status = false;
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditFSCTHCCharges(RequestFscThcChargeRequest request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                    EXEC dbo.Sp_AddEditFscthcCharges 
                        @FSCChargesID = {request.FSCChargesID},
                        @OperationId = {request.OperationId},
                        @ContainerType = {request.ContainerType},
                        @Type = {request.Type},
                            @Size = {request.Size},
                            @MaxDistance = {request.MaxDistance},
                            @CommodityType = {request.CommodityType},
                            @ContainerLoadType = {request.ContainerLoadType},
                            @TransportFrom = {request.TransportFrom},
                            @EximType = {request.EximType},
                            @LocationId = {request.LocationId},
                            @FromMetric = {request.FromMetric},
                            @ToMetric = {request.ToMetric},
                            @RateCWC = {request.RateCWC},
                            @ContractorRate = {request.ContractorRate},
                            @EffectiveDate = {request.EffectiveDate},
                            @BranchId = {request.BranchId},
                            @CreatedBy = {request.CreatedBy},
                            @UpdatedBy = {request.UpdatedBy}
                             ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEntryHTCharges", ex);
            }
        }

        public async Task<Response<List<FSCTHCcharges>>> GetAllFSCTHCCharges()
        {
            var response = new Response<List<FSCTHCcharges>>();

            try
            {
                var result = await _db.FSCTHCchargesList.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<FSCTHCcharges>();
                response.Status = false;
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditReeferCharges(RequestReeferCharges request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                    EXEC dbo.Sp_AddEditReeferChrg 
                        @ReeferChrgId = {request.ReeferChrgId},
                        @EffectiveDate = {request.EffectiveDate},
                        @ElectricityCharge = {request.ElectricityCharge},
                        @SacCode = {request.SacCode},
                        @ContainerSize = {request.ContainerSize},
                        @CreatedBy = {request.CreatedBy},
                        @UpdatedBy = {request.UpdatedBy}
                        
                ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEntryHTCharges", ex);
            }
        }

        public async Task<Response<List<ReeferCharges>>> GetAllReeferCharges()
        {
            var response = new Response<List<ReeferCharges>>();

            try
            {
                var result = await _db.GetReeferChargesList.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<ReeferCharges>();
                response.Status = false;
            }
          return response;
        }

        public async Task<AddEditResponse> AddEditMovementChrg(RequestMovementCharges request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                    EXEC dbo.Sp_AddEditMovementChrg 
                        @MovementChargeId = {request.MovementChargeId},
                        @MovementBy = {request.MovementBy},
                        @Origin = {request.Origin},
                        @MovementVia = {request.MovementVia},
                        @Size = {request.Size},
                        @CargoType = {request.CargoType},
                        @MovementRate = {request.MovementRate},
                        @EffectiveDate = {request.EffectiveDate},
                        @CreatedBy = {request.CreatedBy},
                        @ModifiedBy = {request.ModifiedBy}
                        
                ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEntryHTCharges", ex);
            }
        }

        public async Task<Response<List<MovementCharge>>> GetAllMovementCharges()
        {
            var response = new Response<List<MovementCharge>>();

            try
            {
                var result = await _db.GetMovementChargesList.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<MovementCharge>();
                response.Status = false;
            }
            return response;
        }
        public async Task<AddEditResponse> AddEditFumigationChrg(RequestFumigationCharges request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                    EXEC dbo.Sp_AddEditfumigationChrg 
                        @FumigationChargeId = {request.FumigationChargeId},
                        @ChargesFor = {request.ChargesFor},
                        @ContainerSize = {request.ContainerSize},
                        @FromWeight = {request.FromWeight},
                        @ToWeight = {request.ToWeight},
                        @Rate = {request.Rate}
                       
                        
                ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEntryHTCharges", ex);
            }
        }
        public async Task<Response<List<FumigationCharge>>> GetAllFumigationCharges()
        {
            var response = new Response<List<FumigationCharge>>();

            try
            {
                var result = await _db.GetFumigationChargesList.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<FumigationCharge>();
                response.Status = false;
            }
            return response;
        }

        public async Task<AddEditResponse> AddEditRTChargesDtl(RequestRTChargesDtl request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                    EXEC dbo.Sp_AddEditRTChargesDtl 
                        @RTChargesDtlID = {request.RTChargesDtlID},
                        @RTChargesID = {request.RTChargesID},
                        @WtSlabId = {request.WtSlabId},
                        @FromWtSlabCharge = {request.FromWtSlabCharge},
                        @ToWtSlabCharge = {request.ToWtSlabCharge},
                        @RateCWC = {request.RateCWC},
                        @WeightSlab = {request.WeightSlab},
                        @PortName = {request.PortName},
                        @PortId = {request.PortId}             
                ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEntryHTCharges", ex);
            }
        }
        public async Task<Response<List<RTRChargeDetails>>> GetAllRTChargesDtl()
        {
            var response = new Response<List<RTRChargeDetails>>();

            try
            {
                var result = await _db.GetRTRChargesDetailsList.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<RTRChargeDetails>();
                response.Status = false;
            }
            return response;
        }



        public async Task<AddEditResponse> AddEditMstGroundRent(RequestMstGroundRent request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
            EXEC dbo.Sp_AddEditGroundRent 
                @GroundRentId = {request.GroundRentId},
                @ContainerType = {request.ContainerType},
                @CommodityType = {request.CommodityType},
                @DaysRangeFrom = {request.DaysRangeFrom},
                @DaysRangeTo = {request.DaysRangeTo},
                @RentAmount = {request.RentAmount},
                @ElectricityCharge = {request.ElectricityCharge},
                @Size = {request.Size},
                @OperationType = {request.OperationType},
                @EffectiveDate = {request.EffectiveDate},
                @BranchId = {request.BranchId},
                @SacCode = {request.SacCode},
                @CreatedBy = {request.CreatedBy},
                @UpdatedBy = {request.UpdatedBy},
                @FclLcl = {request.FclLcl}
                ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEditGroundRent", ex);
            }

        }

        public async Task<Response<List<MstGroundRent>>> GetMstGroundRent()
        {
            var response = new Response<List<MstGroundRent>>();

            try
            {
                var result = await _db.GetMstGroundRent.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<MstGroundRent>();
                response.Status = false;
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditMstInsurance(RequestMstInsurance request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
            EXEC dbo.Sp_AddEditInsurance 
                @InsuranceId = {request.InsuranceId},
                @Rate = {request.Rate},
                @EffectiveDate = {request.EffectiveDate},
                @BranchId = {request.BranchId},
                @SacCodeId = {request.SacCodeId},
                @CreatedBy = {request.CreatedBy},
                @UpdatedBy = {request.UpdatedBy}
        ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEditInsurance", ex);
            }

        }

        public async Task<Response<List<MstInsurance>>> GetMstInsurance(int? page, int? size)
        {
            var response = new Response<List<MstInsurance>>();

            try
            {
                var query = _db.GetMstInsurance.AsQueryable();

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var data = await query.ToListAsync();

                response.Data = data;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<MstInsurance>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;

        }

        public async Task<AddEditResponse> AddEditMstMiscellaneouse(RequestMstMiscellaneous request)
        {
            try
            {
                var result = await _db.AddEditResponse
                        .FromSqlInterpolated($@"
                        EXEC dbo.Sp_AddEditMiscellaneous 
                        @MiscellaneousId = {request.MiscellaneousId},
                        @BranchId = {request.BranchId},
                        @Fumigation = {request.Fumigation},
                        @Washing = {request.Washing},
                        @Reworking = {request.Reworking},
                        @Bagging = {request.Bagging},
                        @Palletizing = {request.Palletizing},
                        @Printing = {request.Printing},
                        @Banking = {request.Banking},
                        @PhotoCopy = {request.PhotoCopy},
                        @ChequeReturn = {request.ChequeReturn},
                        @Others = {request.Others},
                        @EffectiveDate = {request.EffectiveDate},
                        @SacCode = {request.SacCode},
                        @CreatedBy = {request.CreatedBy},
                        @UpdatedBy = {request.UpdatedBy},
                        @FumigationChargeType = {request.FumigationChargeType}
                      ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEditMiscellaneous", ex);
            }

        }

        public async Task<Response<List<MstMiscellaneous>>> GetMstMiscellaneous()
        {
            var response = new Response<List<MstMiscellaneous>>();

            try
            {
                var result = await _db.GetMstMiscellaneous.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<MstMiscellaneous>();
                response.Status = false;
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditMstRailFreightFees(RequestMstRailFreightFees request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                EXEC dbo.Sp_AddEditRailFreightFees 
                @RailFreightId = {request.RailFreightId},
                @ContainerType = {request.ContainerType},
                @CommodityType = {request.CommodityType},
                @OperationType = {request.OperationType},
                @Reefer = {request.Reefer},
                @Rate = {request.Rate},
                @ContainerSize = {request.ContainerSize},
                @Port = {request.Port},
                @LocationId = {request.LocationId},
                @FromMetric = {request.FromMetric},
                @ToMetric = {request.ToMetric},
                @BranchId = {request.BranchId},
                @CreatedBy = {request.CreatedBy},
                @UpdatedBy = {request.UpdatedBy}
              ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEditRailFreightFees", ex);
            }

        }

        public async Task<Response<List<MstRailFreightFees>>> GetMstRailFreightFees(int? page, int? size)
        {
            var response = new Response<List<MstRailFreightFees>>();

            try
            {
                var query = _db.GetMstRailFreightFees.AsQueryable();

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var data = await query.ToListAsync();

                response.Data = data;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<MstRailFreightFees>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<MstParty>>> GetMstParty(int? page, int? size, string? partyType)
        {
            var response = new Response<List<MstParty>>();

            try
            {
                var query = _db.GetMstEximTraderMaster.AsQueryable();

                //operation type wise filter
                if (partyType is not null)
                {
                    query = _db.GetMstEximTraderMaster
                        .Where(x => x.OperationType.ToLower() == partyType.ToLower())
                        .AsQueryable();
                }               


                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var data = await query.ToListAsync();

                // Map to MstParty
                var mappedParties = data.Select(trader => new MstParty
                {
                    PartyId = trader.TraderId,
                    PartyName = trader.EximTraderName ?? "" // Use PartyCode as PartyName
                }).ToList();

                response.Data = mappedParties;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<MstParty>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;

        }

        public async Task<ResponsePort> AddEditPort(RequestPort request)
        {
            try
            {
                var result = await _db.ResponsePort
                    .FromSqlInterpolated($@"
                    EXEC dbo.AddEditMstPort 
                        @PortId = {request.PortId},
                        @PortName = {request.PortName},
                        @PortAlias = {request.PortAlias},
                        @POD = {request.POD},
                        @Country = {request.Country},
                        @State = {request.State},
                        @CreatedBy = {request.CreatedBy},
                        @UpdatedBy = {request.UpdatedBy}          
                ")
                    .AsNoTracking()
                    .ToListAsync();

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute AddEditMstPort", ex);
            }
        }

        public async Task<Response<List<ResponseAddEditPort>>> GetPort(int? page, int? size)
        {
            var response = new Response<List<ResponseAddEditPort>>();

            try
            {
                var query = from port in _db.GetPort
                            join state in _db.GetState on port.State equals state.Id into stateGroup
                            from state in stateGroup.DefaultIfEmpty()
                            join country in _db.GetCountryList on port.Country equals country.Id into countryGroup
                            from country in countryGroup.DefaultIfEmpty()
                            select new ResponseAddEditPort
                            {
                                PortId = port.PortId,
                                PortName = port.PortName,
                                PortAlias = port.PortAlias,
                                POD = port.POD,
                                Country = port.Country,
                                State = port.State,
                                CreatedBy = port.CreatedBy,
                                CreatedOn = port.CreatedOn,
                                CountryName = country != null ? country.Name : null,
                                StateName = state != null ? state.Name : null
                            };

                var result = await query.ToListAsync();


                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var data = await query.ToListAsync();

                response.Data = data;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<ResponseAddEditPort>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }


        public async Task<Response<List<MstEximTraderMaster>>> GetMstEximTraderMaster(int? page, int? size)
        {
            var response = new Response<List<MstEximTraderMaster>>();

            try
            {
                var query = _db.GetMstEximTraderMaster.AsQueryable();

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var data = await query.ToListAsync();

                response.Data = data;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<MstEximTraderMaster>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditMstCommodity(RequestMstCommodity request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
            EXEC dbo.SP_AddMstCommodity 
                @CommodityId = {request.CommodityId},
                @CommodityName = {request.CommodityName},
                @CommodityType = {request.CommodityType},
                @Alias = {request.Alias},
                @IsTaxExempted = {request.IsTaxExempted},
                @IsFumigationChemical = {request.IsFumigationChemical},
                @CreatedBy = {request.CreatedBy},
                @UpdatedBy = {request.UpdatedBy}
                 ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute SP_AddMstCommodity", ex);
            }

        }
        public async Task<Response<List<MstCommodity>>> GetMstCommodity(int? page, int? size)
        {
            var response = new Response<List<MstCommodity>>();

            try
            {
                var query = _db.GetMstCommodity.AsQueryable();

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var data = await query.ToListAsync();

                response.Data = data;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<MstCommodity>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }
        public async Task<Response<List<State>>> GetState(int? id)
        {
            var response = new Response<List<State>>();

            try
            {
                var query = _db.GetState.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.CountryId == id.Value);
                }

                var result = await query.ToListAsync();

                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<State>();
                response.Status = false;
            }

            return response;
        }
        public async Task<AddEditResponse> AddEditGoDown(RequestGoDown request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                 EXEC dbo.AddOrUpdateGodown 
                @GodownId = {request.GodownId},
                @GodownName = {request.GodownName},
                @LocationAlias = {request.LocationAlias},
                @CreatedBy = {request.CreatedBy},
                @UpdatedBy = {request.UpdatedBy}
                 ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute AddOrUpdateGodown", ex);
            }

        }

        public async Task<Response<List<GoDown>>> GetMstGoDown(int? page, int? size)
        {
            var response = new Response<List<GoDown>>();

            try
            {
                var query = _db.GetMstGoDown.AsQueryable();

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var data = await query.ToListAsync();

                response.Data = data;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<GoDown>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }
        public async Task<AddEditResponse> AddEditOBLEntry(RequestOBLEntry request)
        {
            var response = new AddEditResponse();

            try
            {
                var mainResult = await _db.Set<ResponseOBLEntry>()
                    .FromSqlInterpolated($@"
                EXEC dbo.SP_AddOrUpdateOBLEntry 
                    @Id = {request.Id},
                    @ContainerCBTType = {request.ContainerCBTType},
                    @ContainerCBTNo = {request.ContainerCBTNo},
                    @ContainerCBTSize = {request.ContainerCBTSize},
                    @IGMNo = {request.IGMNo},
                    @IGMDate = {request.IGMDate},
                    @TPNo = {request.TPNo},
                    @TPDate = {request.TPDate},
                    @MovementType = {request.MovementType},
                    @Port = {request.Port},
                    @Country = {request.Country},
                    @ShippingLine = {request.ShippingLine},
                    @CreatedBy = {request.CreatedBy},
                    @UpdatedBy = {request.UpdatedBy}
            ")
                    .AsNoTracking()
                    .ToListAsync();

                var spResult = mainResult.FirstOrDefault();
                if (spResult == null || spResult.Id == 0)
                {
                    response.Response = "Main SP failed or returned no ID.";
                    return response;
                }

                if (request.requestOblEntryAddDtls?.Any() == true)
                {
                    foreach (var detail in request.requestOblEntryAddDtls)
                    {
                        await _db.AddEditResponse
                            .FromSqlInterpolated($@"
                        EXEC dbo.SP_AddOrUpdateOblEntryAdditionalDetails
                            @ID = {detail.ID},
                            @AddID = {detail.AddID},
                            @IcesContId = {detail.IcesContId},
                            @OBL_HBL_No = {detail.OBL_HBL_No},
                            @OBL_HBL_Date = {detail.OBL_HBL_Date},
                            @SMTP_No = {detail.SMTP_No},
                            @SMTP_Date = {detail.SMTP_Date},
                            @Cargo_Desc = {detail.Cargo_Desc},
                            @Commodity = {detail.Commodity},
                            @Cargo_Type = {detail.Cargo_Type},
                            @No_of_PKG = {detail.No_of_PKG},
                            @PKG_Type = {detail.PKG_Type},
                            @GR_WT_Kg = {detail.GR_WT_Kg},
                            @Importer_Name = {detail.Importer_Name},
                            @IGM_Importer_Name = {detail.IGM_Importer_Name},
                            @IsProcessed = {detail.IsProcessed},
                            @OBLEntryId = {spResult.Id},
                            @CreatedBy = {detail.CreatedBy},
                            @UpdatedBy = {detail.UpdatedBy}
                    ")
                            .AsNoTracking()
                            .ToListAsync();
                    }
                }

                response.Response = "OK";
            }
            catch (Exception ex)
            {
                response.Response = $"Error: {ex.Message}";
            }
            return response;
        }
        public async Task<Response<List<OBLEntry>>> GetOblEntry(int? id, int? page, int? size)
        {
            var response = new Response<List<OBLEntry>>();

            try
            {
                var query = _db.GetOBLEntry.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.Id == id.Value);
                }

                var totalRecords = await query.CountAsync(); 

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var result = await query.ToListAsync();

                response.Data = result;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<OBLEntry>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<Country>>> GetCountry(int? page, int? size)
        {
            var response = new Response<List<Country>>();

            try
            {
                var query = _db.GetCountryList.AsQueryable();

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var data = await query.ToListAsync();

                response.Data = data;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<Country>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditYardInvoice(RequestYardInvocie request)
        {
            // insert to yard invoice 
            try
            {
                var result = await _db.Set<ResponseAddEdityard>()
                    .FromSqlInterpolated($@"
                EXEC dbo.AddEditYardInvoice 
                    @YardInvId = {request.YardInvId},
                    @TaxInvoice = {request.TaxInvoice},
                    @BillOfSupply = {request.BillOfSupply},
                    @InvoiceNo = {request.InvoiceNo},
                    @DeliveryDate = {request.DeliveryDate},
                    @ApplicationId = {request.ApplicationId},
                    @InvoiceDate = {request.InvoiceDate},
                    @PartyId = {request.PartyId},
                    @PayeeId = {request.PayeeId},
                    @GSTNo = {request.GSTNo},
                    @PaymentMode = {request.PaymentMode},
                    @FactoryDestuffing = {request.FactoryDestuffing},
                    @DirectDestuffing = {request.DirectDestuffing},
                    @PlaceOfSupply = {request.PlaceOfSupply},
                    @SEZId = {request.SEZId},
                    @OTHours = {request.OTHours},
                    @Container = {request.Container},
                    @CreatedBy = {request.CreatedBy},
                    @UpdatedBy = {request.UpdatedBy},
                    @PayeeName = {request.PayeeName},
                    @ExaminationChargeType = {request.ExaminationChargeType},
                    @Remarks = {request.Remarks}

            ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                AddEditResponse resultres = null;
                if (response == null || response.YardInvId == 0)
                {
                    resultres.Response = "Main SP failed or returned no ID.";
                    return resultres;
                }
                
                // insert yard charges  
                if (response != null && response.YardInvId != 0 && request.jsonData != null)
                {
                    var result1 = await _db.Set<AddEditResponse>()
                        .FromSqlInterpolated($@"
                    EXEC dbo.SP_AddYardInvoiceChargesJson
                        @YardInvId = {response.YardInvId},
                        @jsonData = {request.jsonData}
                ")
                        .AsNoTracking()
                        .ToListAsync();

                    resultres = result1.FirstOrDefault();
                }

                return resultres;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute AddEditYardInvoice", ex);
            }
        }


        public async Task<Response<List<InvoiceYard>>> GetYardInvoice(int? page, int? size, string? PayeeName)
        {
            var response = new Response<List<InvoiceYard>>();

            try
            {
                var query = _db.GetYardInvoiceList.AsQueryable();

                if (!string.IsNullOrEmpty(PayeeName))
                {
                    query = query.Where(x => x.PayeeName.Contains(PayeeName));
                }
                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var data = await query.ToListAsync();

                response.Data = data;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<InvoiceYard>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<OblEntryAdditionalDetails>>> GetOblEntryAdditionalDetails(int? id, int? OBLEntryId)
        {
            var response = new Response<List<OblEntryAdditionalDetails>>();

            try
            {
                var query = _db.GetOblEntryAdditionalDetails.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.ID == id.Value);
                }
                if (OBLEntryId.HasValue)
                {
                    query = query.Where(s => s.OBLEntryId == OBLEntryId.Value);
                }


                var result = await query.ToListAsync();

                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<OblEntryAdditionalDetails>();
                response.Status = false;
            }

            return response;
        }
        public async Task<AddEditResponse> RemoveOblEntryAdditionalDetails(int id)
        {
            var response = new AddEditResponse();

            try
            {
                // Fetch records to be deleted
                var recordsToDelete = await _db.GetOblEntryAdditionalDetails
                    .Where(x => x.ID == id)
                    .ToListAsync();

                if (recordsToDelete.Any())
                {
                    _db.GetOblEntryAdditionalDetails.RemoveRange(recordsToDelete);
                    await _db.SaveChangesAsync();

                    response.Response = "Records deleted successfully.";
                }
                else
                {
                    response.Response = "No records found for the given OBLEntryId.";
                }
            }
            catch (Exception ex)
            {
                response.Response = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<AddEditResponse> RemoveEntries(int id)
        {
            var response = new AddEditResponse();

            try
            {
                var recordsToDelete = await _db.GetEntryList
                    .Where(x => x.EntryId == id)
                    .ToListAsync();

                if (recordsToDelete.Any())
                {
                    _db.GetEntryList.RemoveRange(recordsToDelete);
                    await _db.SaveChangesAsync();

                    response.Response = "Records deleted successfully.";
                }
                else
                {
                    response.Response = "No records found for the given OBLEntryId.";
                }
            }
            catch (Exception ex)
            {
                response.Response = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditHandlingCharges(RequestHandlingCharges request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                    EXEC dbo.Sp_AddEditHandlingCharges 
                    @HandlingChargesID = {request.HandlingChargesID},
                    @EffectiveDate = {request.EffectiveDate},
                    @SacCodeId = {request.SacCodeId},
                    @Rate = {request.Rate},
                    @MinRateperSBBOE = {request.MinRateperSBBOE},
                    @CreatedBy = {request.CreatedBy},
                    @UpdatedBy = {request.UpdatedBy}
                       
                ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEntryHTCharges", ex);
            }
        }

        public async Task<Response<List<HandlingChargescs>>> GetAllHandlingCharges(int? page, int? size)
        {
            var response = new Response<List<HandlingChargescs>>();

            try
            {
                var query = _db.GetHandlinghargesList.AsQueryable();

                var totalRecords = await query.CountAsync();
                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var data = await query.ToListAsync();

                response.Data = data;
                response.Status = true;
                response.TotalCount = totalRecords;

            }
            catch (Exception ex)
            {
                response.Data = new List<HandlingChargescs>();
                response.Status = false;
            }

            return response;
        }


        public async Task<AddEditResponse> AddEditOverTimeCharge(RequestOverTimeCharge request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
            EXEC dbo.Sp_AddEditOverTimeCharge 
                @OverTimeChargeId = {request.OverTimeChargeId},
                @EffectiveDate = {request.EffectiveDate},
                @SACCodeId = {request.SACCodeId},
                @OperationType = {request.OperationType},
                @Holiday = {request.Holiday},
                @Time = {request.Time},
                @Rate = {request.Rate},
                @MaxMinHours = {request.MaxMinHours},
                @CreatedBy = {request.CreatedBy},
                @ModifiedBy = {request.ModifiedBy}
        ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEditOverTimeCharge", ex);
            }

        }

        public async Task<Response<List<OverTimeCharge>>> GetOverTimeCharge(int? id, int? page, int? size)
        {
            var response = new Response<List<OverTimeCharge>>();

            try
            {
                var query = _db.GetOverTimeCharge.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.OverTimeChargeId == id.Value);
                }

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var result = await query.ToListAsync();

                response.Data = result;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<OverTimeCharge>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }


        public async Task<Response<List<ResponseOBLContauner>>> GetOBLContainerList(int? page, int? size,string? containerNo,string? oblHblNo)
        {
            var response = new Response<List<ResponseOBLContauner>>();

            try
            {
                var query = from obl in _db.GetOBLEntry
                            join obldetails in _db.GetOblEntryAdditionalDetails
                                on obl.Id equals obldetails.OBLEntryId
                            join gateentry in _db.GetEntryList
                               on obl.ContainerCBTNo equals gateentry.ContainerNo
                            join AppContainerDetails in _db.GetAppraisementContainerDetails
                                on obl.Id equals AppContainerDetails.OBLNoId
                            join AppDoDetails in _db.GetAppraisementDoDetails
                                on AppContainerDetails.CustomAppraisementId equals AppDoDetails.CustomAppraisementId
                            where
                          (string.IsNullOrEmpty(containerNo) || gateentry.ContainerNo == containerNo) &&
                          (string.IsNullOrEmpty(oblHblNo) || obldetails.OBL_HBL_No == oblHblNo)
                            select new ResponseOBLContauner
                            {
                                ICDNo = gateentry.CFSNo,
                                ContainerCBTNo = obl.ContainerCBTNo,
                                Size = gateentry.Size,
                                Reefer = gateentry.Reefer,
                                OBL_HBL_No = obldetails.OBL_HBL_No,
                                CargoType = AppContainerDetails.CargoType,
                                NoOfPackage = obldetails.No_of_PKG,
                                GrWt = obldetails.GR_WT_Kg,
                                DoValidateDate = AppDoDetails.DoValidDate
                            };

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var data = await query.ToListAsync();

                response.Data = data;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<ResponseOBLContauner>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }


        public async Task<AddEditResponse> AddEditExaminationCharge(RequestExaminationCharge request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
            EXEC dbo.Sp_AddEditExaminationCharge 
                @ExaminationChargeId = {request.ExaminationChargeId},
                @EffectiveDate = {request.EffectiveDate},
                @SACCodeId = {request.SACCodeId},
                @ExaminationFor = {request.ExaminationFor},
                @ExaminationPercent = {request.ExaminationPercent},
                @RatePerPacket = {request.RatePerPacket},
                @MinimumCharges = {request.MinimumCharges},
                @WeightForAdditionalCharges = {request.WeightForAdditionalCharges},
                @RateForAdditionalCharges = {request.RateForAdditionalCharges},
                @CreatedBy = {request.CreatedBy},
                @ModifiedBy = {request.ModifiedBy}
        ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEditExaminationCharge", ex);
            }

        }

        public async Task<Response<List<ExaminationCharge>>> GetExaminationCharge(int? id, int? page, int? size)
        {
            var response = new Response<List<ExaminationCharge>>();

            try
            {
                var query = _db.GetExaminationCharge.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.ExaminationChargeId == id.Value);
                }

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var result = await query.ToListAsync();

                response.Data = result;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<ExaminationCharge>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<ResponseCbcContainerList>>> GetCbtContainerDetailsList(int? page, int? size)
        {
            var response = new Response<List<ResponseCbcContainerList>>();

            try
            {
                var query = from obl in _db.GetOBLEntry
                            join obldetails in _db.GetOblEntryAdditionalDetails
                                on obl.Id equals obldetails.OBLEntryId
                            select new ResponseCbcContainerList
                            {
                                ContainerCBTNo = obl.ContainerCBTNo,
                                OBL_HBL_No = obldetails.OBL_HBL_No,
                                Cargo_Type=obldetails.Cargo_Type,
                                No_of_PKG=obldetails.No_of_PKG,
                                GR_WT_Kg=obldetails.GR_WT_Kg
                            };

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var data = await query.ToListAsync();

                response.Data = data;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<ResponseCbcContainerList>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }


        public async Task<AddEditResponse> AddEditCustomAppraisementApplicationHeader(RequestCustomAppraisementApplicationHeader request)
        {
            var response = new AddEditResponse();
            try
            {
                var mainResult = await _db.Set<ResponseCustomAppraisementApplicationHeader>()
               .FromSqlInterpolated($@"
                     EXEC dbo.SP_AddOrUpdateCustomAppraisementApplicationHeader 
                     @ID = {request.ID},
                     @AppraisementNo = {request.AppraisementNo},
                     @AppraisementDate = {request.AppraisementDate},
                     @ShippingLineId = {request.ShippingLineId},
                     @CHAId = {request.CHAId},
                     @Vessel = {request.Vessel},
                     @Voyage = {request.Voyage},
                     @Rotation = {request.Rotation},
                     @DeliveryType = {request.DeliveryType},
                     @DOStatus = {request.DOStatus},
                     @AppraisementStatus = {request.AppraisementStatus},
                     @CreatedBy = {request.CreatedBy},
                     @ModifiedBy = {request.ModifiedBy},
                     @ExaminationPercentage = {request.ExaminationPercentage}
                     ")
                 .AsNoTracking()
                .ToListAsync();
                var spResult = mainResult.FirstOrDefault();
                if (spResult == null || spResult.Id == 0)
                {
                    response.Response = "Main SP failed or returned no ID.";
                    return response;
                }
                if (request.AppraisementDoDetailsList?.Any() == true)
                {
                    foreach (var detail in request.AppraisementDoDetailsList)
                    {
                        var doResult = await _db.Set<AddEditResponse>()
                        .FromSqlInterpolated($@"
                        EXEC dbo.SP_AddOrUpdateAppraisementDoDetails 
                        @Id = {detail.Id},
                        @DoIssuedBy = {detail.DoIssuedBy},
                        @CargosDeliveredTo = {detail.CargosDeliveredTo},
                        @ValidType = {detail.ValidType},
                        @DoValidDate = {detail.DoValidDate},
                        @CustomAppraisementId = {spResult.Id},
                        @CreatedBy = {detail.CreatedBy},
                        @ModifiedBy = {detail.ModifiedBy}
                         ")
                        .AsNoTracking()
                        .ToListAsync();

                        var doStatus = doResult.FirstOrDefault();

                    }
                }
                if (request.AppraisementContainerDetailsList?.Any() == true)
                {
                    foreach (var detail in request.AppraisementContainerDetailsList)
                    {
                        var containerResult = await _db.Set<AddEditResponse>()
                       .FromSqlInterpolated($@"
                        EXEC dbo.SP_AddOrUpdateAppraisementContainerDetails 
                        @Id = {detail.Id},
                        @ContainerCBTNo = {detail.ContainerCBTNo},
                        @ICDCode = {detail.ICDCode},
                        @Size = {detail.Size},
                        @FCL_LCL = {detail.FCL_LCL},
                        @ContainerCBTType = {detail.ContainerCBTType},
                        @CargoType = {detail.CargoType},
                        @RMS = {detail.RMS},
                        @LineNo = {detail.LineNo},
                        @OBLNoId = {detail.OBLNoId},
                        @OBLDate = {detail.OBLDate},
                        @BOENo = {detail.BOENo},
                        @BOEDate = {detail.BOEDate},
                        @CHANameAddress = {detail.CHANameAddress},
                        @ImporterNameAddress = {detail.ImporterNameAddress},
                        @CargoDescription = {detail.CargoDescription},
                        @CIFValue = {detail.CIFValue},
                        @Duty = {detail.Duty},
                        @NoOfPackages = {detail.NoOfPackages},
                        @GrossWeightKg = {detail.GrossWeightKg},
                        @WithoutDOSealNo = {detail.WithoutDOSealNo},
                        @CustomAppraisementId = {spResult.Id},
                        @CreatedBy = {detail.CreatedBy},
                        @ModifiedBy = {detail.ModifiedBy},
                        @chaId = {detail.chaId},
                        @importerId = {detail.importerId}
                       ")
                      .AsNoTracking()
                      .ToListAsync();

                        var containerStatus = containerResult.FirstOrDefault();

                    }
                }
                response.Response = "OK";
            }
            catch (Exception ex)
            {
                response.Response = $"Error: {ex.Message}";
            }
            return response;
        }

        public async Task<Response<List<CustomAppraisementApplicationHeader>>> GetCustomAppraisementApplicationHeader(int? id, int? page, int? size)
        {
            var response = new Response<List<CustomAppraisementApplicationHeader>>();

            try
            {
                var query = _db.CustomAppraisementApplicationHeaderList.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.ID == id.Value);
                }

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var result = await query.ToListAsync();

                response.Data = result;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<CustomAppraisementApplicationHeader>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<AppraisementDoDetails>>> GetAppraisementDoDetails(int? id, int? page, int? size, int? CustAppId)
        {
            var response = new Response<List<AppraisementDoDetails>>();

            try
            {
                var query = _db.GetAppraisementDoDetails.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.Id == id.Value);
                }
                if (CustAppId.HasValue)
                {
                    query = query.Where(s => s.CustomAppraisementId == CustAppId.Value);
                }

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var result = await query.ToListAsync();

                response.Data = result;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<AppraisementDoDetails>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<AppraisementContainerDetails>>> GetAppraisementContainerDetails(int? id, int? page, int? size, int? CustAppId)
        {
            var response = new Response<List<AppraisementContainerDetails>>();

            try
            {
                var query = _db.GetAppraisementContainerDetails.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.Id == id.Value);
                }
                if (CustAppId.HasValue)
                {
                    query = query.Where(s => s.CustomAppraisementId == CustAppId.Value);
                }

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var result = await query.ToListAsync();

                response.Data = result;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<AppraisementContainerDetails>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<ResponseOBLEntryWithDetailsDto>>> GetOBLEntriesWithDetails(int? id = null, string containerNo = null, int? page = null, int? size = null)
        {
            var response = new Response<List<ResponseOBLEntryWithDetailsDto>>();

            try
            {
                var query = from obl in _db.GetOBLEntry
                            join details in _db.GetOblEntryAdditionalDetails
                                on obl.Id equals details.OBLEntryId into joined
                            from detail in joined.DefaultIfEmpty()
                            select new ResponseOBLEntryWithDetailsDto
                            {
                                Id = obl.Id,
                                ContainerCBTType = obl.ContainerCBTType,
                                ContainerCBTNo = obl.ContainerCBTNo,
                                ContainerCBTSize = obl.ContainerCBTSize,
                                IGMNo = obl.IGMNo,
                                IGMDate = obl.IGMDate,
                                TPNo = obl.TPNo,
                                TPDate = obl.TPDate,
                                MovementType = obl.MovementType,
                                Port = obl.Port,
                                Country = obl.Country,
                                ShippingLine = obl.ShippingLine,

                                OBL_HBL_No = detail.OBL_HBL_No,
                                OBL_HBL_Date = detail.OBL_HBL_Date,
                                SMTP_No = detail.SMTP_No,
                                SMTP_Date = detail.SMTP_Date,
                                Cargo_Desc = detail.Cargo_Desc,
                                Commodity = detail.Commodity,
                                Cargo_Type = detail.Cargo_Type,
                                No_of_PKG = detail.No_of_PKG,
                                PKG_Type = detail.PKG_Type,
                                GR_WT_Kg = detail.GR_WT_Kg,
                                Importer_Name = detail.Importer_Name,
                                IGM_Importer_Name = detail.IGM_Importer_Name
                            };

                if (id.HasValue)
                {
                    query = query.Where(x => x.Id == id.Value);
                }

                if (!string.IsNullOrEmpty(containerNo))
                {
                    query = query.Where(x => x.ContainerCBTNo.Contains(containerNo));
                }

                var totalRecords = await query.CountAsync();

                if (page.HasValue && size.HasValue && page > 0 && size > 0)
                {
                    int skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var result = await query.ToListAsync();

                response.Data = result;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<ResponseOBLEntryWithDetailsDto>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }



        public async Task<Response<List<ResponseImportChargesCalc>>> GetImportChargesCalcAsync(string containerOBLList, int partyId, int typeOfCharge)
        {
            var response = new Response<List<ResponseImportChargesCalc>>();

            try
            {
                var results = await _db
                    .Set<ResponseImportChargesCalc>()
                    .FromSqlInterpolated($"EXEC [dbo].[ImportChargesCalc] {containerOBLList}, {partyId}, {typeOfCharge}")
                    .AsNoTracking()
                    .ToListAsync();

                response.Data = results;
                response.Status = true;
                response.TotalCount = results.Count;
            }
            catch (Exception ex)
            {
                response.Data = new List<ResponseImportChargesCalc>();
                response.Status = false;
                response.Message = $"Error: {ex.Message}";
                response.TotalCount = 0;
            }

            return response;
        }


        public async Task<Response<List<ChargesTypes>>> GetAllChargesTypes()
        {
            var response = new Response<List<ChargesTypes>>();

            try
            {
                var result = await _db.ListChargesTypes.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<ChargesTypes>();
                response.Status = false;
            }

            return response;
        }


        public async Task<AddEditResponse> AddCashReceiptAsync(RequestCashReceiptCreate request)
        {
            var response = new AddEditResponse();
            try
            {
                var CashReceiptHdrresult = await _db.Set<ResponseCustom>()
                 .FromSqlInterpolated($@"
                 EXEC dbo.SP_AddOrUpdateCashReceiptHdr
                 @CashReceiptId = {request.CashReceiptId},
                 @BranchId = {request.BranchId},
                 @AutoCashRcptNo = {request.AutoCashRcptNo},
                 @ReceiptNo = {request.ReceiptNo},
                 @ReceiptDate = {request.ReceiptDate},
                 @InvoiceId = {request.InvoiceId},
                 @PartyId = {request.PartyId},
                 @PayByPdaId = {request.PayByPdaId},
                 @payeeName = {request.PayeeName},
                 @PdaAdjust = {request.PdaAdjust},
                 @FolioNo = {request.FolioNo},
                 @PdaAdjustedAmount = {request.PdaAdjustedAmount},
                 @PdaOpening = {request.PdaOpening},
                 @PdaClosing = {request.PdaClosing},
                 @TotalPaymentReceipt = {request.TotalPaymentReceipt},
                 @TdsAmount = {request.TdsAmount},
                 @InvoiceValue = {request.InvoiceValue},
                 @CompYear = {request.CompYear},
                 @Remarks = {request.Remarks},
                 @PdaAccountDetailsID = {request.PdaAccountDetailsID},
                 @fromPDA = {request.FromPDA},
                 @CashReceiptHtml = {request.CashReceiptHtml},
                 @IsCancelled = {request.IsCancelled},
                 @CancelledReason = {request.CancelledReason},
                 @CancelledOn = {request.CancelledOn},
                 @CancelledBy = {request.CancelledBy},
                 @InvoiceDebitNote = {request.InvoiceDebitNote},
                 @OnlineFacAmt = {request.OnlineFacAmt},
                 @Area = {request.Area},
                 @TransId = {request.TransId},
                 @IsSAP = {request.IsSAP},
                 @IsSAPRev = {request.IsSAPRev},
                 @SAP_DOC_NUMBER = {request.SAP_DOC_NUMBER},
                 @CreatedBy = {request.CreatedBy},
                 @UpdatedBy = {request.UpdatedBy}
                 ")
                .AsNoTracking()
                .ToListAsync();

                var spResult = CashReceiptHdrresult.FirstOrDefault();
                if (spResult == null || spResult.Id == 0)
                {
                    response.Response = "CashReceiptHdrresult failed or returned no ID.";
                    return response;
                }
                if (request.PaymentDetails?.Any() == true)
                {
                    foreach (var detail in request.PaymentDetails)
                    {
                        var result = await _db.Set<ResponseCustom>()
                         .FromSqlInterpolated($@"
                         EXEC dbo.SP_AddOrUpdateCashReceiptDtl
                        @CashReceiptDtlId = {detail.CashReceiptDtlId},
                        @CashReceipthdrId = {spResult.Id},
                        @PayMode = {detail.PayMode},
                        @InstrumentNo = {detail.InstrumentNo},
                        @DraweeBank = {detail.DraweeBank},
                        @Date = {detail.Date},
                        @Amount = {detail.Amount},
                        @IsChqCancelled = {detail.IsChqCancelled},
                        @CreatedBy = {detail.CreatedBy},
                        @UpdatedBy = {detail.UpdatedBy}
                         ")
                        .AsNoTracking()
                        .ToListAsync();

                    }
                }
                if (request.InvoiceDetails?.Any() == true)
                {
                    foreach (var detail in request.InvoiceDetails)
                    {
                        var result = await _db.Set<ResponseCustom>()
                          .FromSqlInterpolated($@"
                           EXEC dbo.SP_AddOrUpdateCashReceiptInvDtls
                           @CashRcptInvDtlsId = {detail.CashRcptInvDtlsId},
                           @CashReceiptId = {spResult.Id},
                           @PartyId = {detail.PartyId},
                           @InvoiceId = {detail.InvoiceId},
                           @InvoiceNo = {detail.InvoiceNo},
                           @InvoiceDate = {detail.InvoiceDate},
                           @InvoiceAmt = {detail.InvoiceAmt},
                           @DueAmt = {detail.DueAmt},
                            @AdjustmentAmt = {detail.AdjustmentAmt}
                           ")
                          .AsNoTracking()
                          .ToListAsync();
                    }
                }

                response.Response = "OK";
            }
            catch (Exception ex)
            {
                response.Response = $"Error: {ex.Message}";
            }
            return response;
        }

        public async Task<Response<List<CashReceiptInvDtls>>> GetInvoiceDetails(int? id, int? page, int? size, int? CashReceiptId)
        {
            var response = new Response<List<CashReceiptInvDtls>>();

            try
            {
                var query = _db.GetCashReceiptInvDtls.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.CashRcptInvDtlsId == id.Value);
                }
                if (CashReceiptId.HasValue)
                {
                    query = query.Where(s => s.CashReceiptId == CashReceiptId.Value);
                }

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var result = await query.ToListAsync();

                response.Data = result;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<CashReceiptInvDtls>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }


        public async Task<Response<List<CashReceiptDtl>>> GetPaymentDetails(int? id, int? page, int? size, int? CashReceiptId)
        {
            var response = new Response<List<CashReceiptDtl>>();

            try
            {
                var query = _db.GetCashReceiptDtl.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.CashReceiptDtlId == id.Value);
                }
                if (CashReceiptId.HasValue)
                {
                    query = query.Where(s => s.CashReceipthdrId == CashReceiptId.Value);
                }

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var result = await query.ToListAsync();

                response.Data = result;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<CashReceiptDtl>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<CashReceiptHdr>>> GetPaymentReceiptHeader(int? id, int? page, int? size)
        {
            var response = new Response<List<CashReceiptHdr>>();

            try
            {
                var query = _db.GetCashReceiptHdr.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.CashReceiptId == id.Value);
                }

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var result = await query.ToListAsync();

                response.Data = result;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<CashReceiptHdr>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<YardInvoiceCharges>>> GetYardInvoiceCharge(int? id, int? InoviceId, int? page, int? size)
        {
            var response = new Response<List<YardInvoiceCharges>>();

            try
            {
                var query = _db.GetYardInvoiceCharges.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.YardInvoiceChargeId == id.Value);
                }
                if (InoviceId.HasValue)
                {
                    query = query.Where(s => s.InoviceId == InoviceId.Value);
                }

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var result = await query.ToListAsync();

                response.Data = result;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<YardInvoiceCharges>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<ResponseYardInvoiceFlat>>> GetPaymentReceiptInvoiceDetails(int? id, string? PayeeName, int? payeeId, int? page, int? size)
        {
            var response = new Response<List<ResponseYardInvoiceFlat>>();

            try
            {
                var query = from inv in _db.GetYardInvoiceList
                            join charges in _db.GetYardInvoiceCharges
                                on inv.YardInvId equals charges.InoviceId into chargeGroup
                            from ch in chargeGroup.DefaultIfEmpty()
                            where (!id.HasValue || inv.YardInvId == id.Value)
                                  && (string.IsNullOrEmpty(PayeeName) || inv.PayeeName == PayeeName)
                                  && (!payeeId.HasValue || inv.PayeeId == payeeId.Value)
                            select new ResponseYardInvoiceFlat
                            {
                                // From InvoiceYard
                                YardInvId = inv.YardInvId,
                                TaxInvoice = inv.TaxInvoice,
                                BillOfSupply = inv.BillOfSupply,
                                InvoiceNo = inv.InvoiceNo,
                                DeliveryDate = inv.DeliveryDate,
                                ApplicationId = inv.ApplicationId,
                                InvoiceDate = inv.InvoiceDate,
                                PartyId = inv.PartyId,
                                PayeeId = inv.PayeeId,
                                GSTNo = inv.GSTNo,
                                PaymentMode = inv.PaymentMode,
                                FactoryDestuffing = inv.FactoryDestuffing,
                                DirectDestuffing = inv.DirectDestuffing,
                                PlaceOfSupply = inv.PlaceOfSupply,
                                SEZId = inv.SEZId,
                                OTHours = inv.OTHours,
                                Container = inv.Container,
                                CreatedBy = inv.CreatedBy,
                                UpdatedBy = inv.UpdatedBy,
                                CreatedAt = inv.CreatedAt,
                                UpdatedAt = inv.UpdatedAt,
                                PayeeName = inv.PayeeName,

                                // From YardInvoiceCharges (can be null due to left join)
                                YardInvoiceChargeId = ch != null ? ch.YardInvoiceChargeId : null,
                                ChargesTypeId = ch.ChargesTypeId,
                                InoviceId = ch.InoviceId,
                                OperationId = ch.OperationId,
                                Clause = ch.Clause,
                                ChargeType = ch.ChargeType,
                                ChargeName = ch.ChargeName,
                                SACCode = ch.SACCode,
                                Quantity = ch.Quantity,
                                Rate = ch.Rate,
                                Amount = ch.Amount,
                                Discount = ch.Discount,
                                Taxable = ch.Taxable,
                                IGSTPer = ch.IGSTPer,
                                IGSTAmt = ch.IGSTAmt,
                                CGSTPer = ch.CGSTPer,
                                CGSTAmt = ch.CGSTAmt,
                                SGSTPer = ch.SGSTPer,
                                SGSTAmt = ch.SGSTAmt,
                                Total = ch.Total
                            };

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    int skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var result = await query.ToListAsync();

                response.Data = result;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<ResponseYardInvoiceFlat>();
                response.Status = false;
                response.Message = $"Error: {ex.Message}";
                response.TotalCount = 0;
            }

            return response;
        }

        public async Task<Response<ResponseImportChargesInvoice>> GetImportChargesInvoice(string? InvoiceNo)
        {
            var response = new Response<ResponseImportChargesInvoice>();

            try
            {
                var flatRows = await _db
                    .Set<FlatImportChargesRow>()
                    .FromSqlInterpolated($"EXEC dbo.ImportChargesReport {InvoiceNo}")
                    .AsNoTracking()
                    .ToListAsync();

                var first = flatRows.FirstOrDefault();
                if (first == null)
                {
                    response.Data = null;
                    response.Status = false;
                    response.Message = "No data found";
                    return response;
                }

                var result = new ResponseImportChargesInvoice
                {
                    // Header Info
                    CompanyName = first.CompanyName,
                    CompanyAddress = first.CompanyAddress,
                    EmailAddress = first.EmailAddress,
                    CWCGSTNO = first.CWCGSTNO,
                    InvNo = first.InvNo,
                    InvDate = first.InvDate,
                    PartyName = first.PartyName,
                    PartyAddress = first.PartyAddress,
                    PartyGST = first.PartyGST,
                    StateName = first.StateName,
                    StateCode = first.StateCode,
                    PlaceOfSupply = first.PlaceOfSupply,
                    IsService = first.IsService,
                    PayerName = first.PayerName,
                    Remarks = first.Remarks,
                    PrintedBy = first.PrintedBy,

                    // Group unique containers
                    ContainerCharges = flatRows
                        .GroupBy(x => x.ContainerCBTNo)
                        .Select(g => new ContainerChargeDto
                        {
                            ICDNo = g.First().ICDNo,
                            ContainerCBTNo = g.Key,
                            Size = g.First().Size,
                            Reefer = g.First().Reefer,
                            OBLHBLNo = g.First().OBLHBLNo,
                            CargoType = g.First().CargoType,
                            NoOfPackage = g.First().NoOfPackage,
                            GrWt = g.First().GrWt,
                            DoValidateDate = g.First().DoValidateDate
                        })
                        .ToList(),

                    // All charges
                    Charges = flatRows
                        .Select(r => new ChargeDetailDto
                        {
                            ChargeCode = r.ChargeCode,
                            Descripton = r.Descripton,
                            SACCode = r.SACCode,
                            Rate = r.Rate,
                            TaxableAmt = r.TaxableAmt,
                            CGSTRate = r.CGSTRate,
                            CGSTAmt = r.CGSTAmt,
                            SGSTRate = r.SGSTRate,
                            SGSTAmt = r.SGSTAmt,
                            IGSTRate = r.IGSTRate,
                            IGSTAmt = r.IGSTAmt,
                            Total = r.Total
                        })
                        .ToList()
                };

                response.Data = result;
                response.Status = true;
                response.TotalCount = flatRows.Count;
            }
            catch (Exception ex)
            {
                response.Status = false;
                response.Message = $"Error: {ex.Message}";
                response.Data = null;
                response.TotalCount = 0;
            }

            return response;
        }

        public async Task<AddEditResponse> CreateGatePassAsync(GatePassRequest request)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                var response = new AddEditResponse();

                var outputId = new SqlParameter
                {
                    ParameterName = "@NewGatePassId",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };

                await _db.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC sp_Insert_GatePass 
                @GatePassId = {request.GatePass.GatePassId},
                @GatePassNo = {request.GatePass.GatePassNo},
                @GatePssDate = {request.GatePass.GatePssDate},
                @ExpDate = {request.GatePass.ExpDate},
                @ChaName = {request.GatePass.ChaName},
                @ImpExpName = {request.GatePass.ImpExpName},
                @ShippingLineName = {request.GatePass.ShippingLineName},
                @Remarks = {request.GatePass.Remarks},
                @InvoiceId = {request.GatePass.InvoiceId},
                @BranchId = {request.GatePass.BranchId},
                @CreatedBy = {request.GatePass.CreatedBy},
                @DepartureDate = {request.GatePass.DepartureDate},
                @ArrivalDate = {request.GatePass.ArrivalDate},
                @FileName = {request.GatePass.FileName},
                @FileCode = {request.GatePass.FileCode},
                @NewGatePassId = {outputId} OUTPUT
        ");

                int newGatePassId = (int)outputId.Value;

             
                var xmlData = XmlConvertercs.ConvertToXmlGatePassDtl(request.GatePassDetails);

             
                await _db.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC sp_Insert_GatePassDtl_XML 
                @GatepassId = {newGatePassId},
                @XmlData = {xmlData}
        ");

                await transaction.CommitAsync();

             
                response.Response = $"GatePass saved successfully. GatePassId: {newGatePassId}";
                return response;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("An error occurred while saving GatePass.", ex);
            }
        }




        public async Task<AddEditResponse> AddEditTransportationCharges(RequestTransportationCharges request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                EXEC dbo.Sp_AddEditTransportationCharges 
                    @TransportationChargesID = {request.TransportationChargesID},
                    @EffectiveDate = {request.EffectiveDate},
                    @SacCodeId = {request.SacCodeId},
                    @ApplicableForId = {request.ApplicableForId},
                    @ApplicableForName = {request.ApplicableForName},
                    @ValueId = {request.ValueId},
                    @Rate = {request.Rate},
                    @AdditionalRatePerPacket = {request.AdditionalRatePerPacket},
                    @CreatedBy = {request.CreatedBy},
                    @UpdatedBy = {request.UpdatedBy}
                   ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEditTransportationCharges", ex);
            }
        }

        public async Task<Response<List<TransportationCharges>>> GetTransportationCharges(int? id, int? page, int? size)
        {
            var response = new Response<List<TransportationCharges>>();

            try
            {
                var query = _db.GetTransportationCharges.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.TransportationChargesID == id.Value);
                }

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var result = await query.ToListAsync();

                response.Data = result;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<TransportationCharges>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditStorageChargesGodown(RequestStorageChargesGodown request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                EXEC dbo.Sp_AddEditStorageChargesGodown 
                    @StorageChargeID = {request.StorageChargeID},
                    @EffectiveDate = {request.EffectiveDate},
                    @SacCodeId = {request.SacCodeId},
                    @StorageForId = {request.StorageForId},
                    @StorageForName = {request.StorageForName},
                    @AreaTypeId = {request.AreaTypeId},
                    @AreaTypeName = {request.AreaTypeName},
                    @BasisId = {request.BasisId},
                    @BasisName = {request.BasisName},
                    @RatePerSqmWeek = {request.RatePerSqmWeek},
                    @RatePerSqmMonth = {request.RatePerSqmMonth},
                    @CreatedBy = {request.CreatedBy},
                    @UpdatedBy = {request.UpdatedBy}
                ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEditStorageChargesGodown", ex);
            }
        }

        public async Task<Response<List<StorageChargesGodown>>> GetStorageChargesGodown(int? id, int? page, int? size)
        {
            var response = new Response<List<StorageChargesGodown>>();

            try
            {
                var query = _db.GetStorageChargesGodown.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.StorageChargeID == id.Value);
                }

                var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

                var result = await query.ToListAsync();

                response.Data = result;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<StorageChargesGodown>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<ResponseGatePassGateOut>>> GetGatePassGateOut( int? GatePassDtlId)
        {
            var response = new Response<List<ResponseGatePassGateOut>>();

            try
            {
                var query = from GPassHeader in _db.GatePassHeader
                            join GPassDetails in _db.GatePassDetails
                                on GPassHeader.GatePassId equals GPassDetails.GatepassId
                            join YardInv in _db.GetYardInvoiceList
                                on GPassHeader.InvoiceId equals YardInv.YardInvId
                                join AppContDetails in _db.GetAppraisementContainerDetails
                                on GPassDetails.ContainerNo equals AppContDetails.ContainerCBTNo
                            where
                                (!GatePassDtlId.HasValue || GPassDetails.GatepassDtlId == GatePassDtlId)
                            select new ResponseGatePassGateOut
                            {
                                GatePassNo = GPassHeader.GatePassNo,
                                VehicleNo = GPassDetails.VehicleNo,
                                Importer = GPassHeader.ImpExpName,
                                ShipplingLine = GPassHeader.ShippingLineName,
                                GatePassDateTime = GPassHeader.GatePssDate,
                                ContainerNo = GPassDetails.ContainerNo,
                                ContainerSize = GPassDetails.Size,
                                CHAName = GPassHeader.ChaName,
                                InvoiceNo = YardInv.InvoiceNo,
                                GatePassValidity = GPassHeader.ExpDate,
                                BoeNo= AppContDetails.BOENo
                            };
        

        var data = await query.ToListAsync();

                response.Data = data;
                response.Status = true;
                response.TotalCount = data.Count;
            }
            catch (Exception ex)
            {
                response.Data = new List<ResponseGatePassGateOut>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }


    }
}
