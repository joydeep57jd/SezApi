
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
				var parameters = new[]
                      {
	                    new SqlParameter("@InsuranceId", (object?)request.InsuranceId ?? DBNull.Value),
	                        new SqlParameter("@Rate", SqlDbType.Decimal)
	                            {
	                 	           Precision = 10,
		                           Scale = 3,
		                           Value = (object?)request.Rate ?? DBNull.Value
                              	},
	                        new SqlParameter("@EffectiveDate", (object?)request.EffectiveDate ?? DBNull.Value),
	                        new SqlParameter("@BranchId", (object?)request.BranchId ?? DBNull.Value),
                           	new SqlParameter("@SacCodeId", (object?)request.SacCodeId ?? DBNull.Value),
                           	new SqlParameter("@CreatedBy", (object?)request.CreatedBy ?? DBNull.Value),
	                        new SqlParameter("@UpdatedBy", (object?)request.UpdatedBy ?? DBNull.Value),
                        };


				var result = await _db.AddEditResponse
					.FromSqlRaw("EXEC dbo.Sp_AddEditInsurance @InsuranceId, @Rate, @EffectiveDate, @BranchId, @SacCodeId, @CreatedBy, @UpdatedBy", parameters)
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
                    @Remarks = {request.Remarks},
                    @MoveToId = {request.MoveToId},
                    @IsLoadContainerInvoice = {request.IsLoadContainerInvoice}

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


        public async Task<Response<List<InvoiceYard>>> GetYardInvoice(int? page, int? size, string? PayeeName, bool? IsLoadContainerInvoice)
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
                if (IsLoadContainerInvoice.HasValue)
                {
                    query = query.Where(x => x.IsLoadContainerInvoice == IsLoadContainerInvoice.Value);
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
                    @UpdatedBy = {request.UpdatedBy},
                    @BasisId = {request.BasisId},
                    @BasisName =  {request.BasisName},
                    @Weight = {request.Weight},
                    @AdditionalPktCharges = {request.AdditionalPktCharges},
                     @Maxvalue_CRORE = {request.Maxvalue_CRORE}
                       
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


        public async Task<Response<List<ResponseOBLContauner>>> GetOBLContainerList(int? page, int? size, string? containerNo, string? oblHblNo, string? AppNo)
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
							join AppraisementApplicationHeader in _db.CustomAppraisementApplicationHeaderList
							   on AppContainerDetails.CustomAppraisementId equals AppraisementApplicationHeader.ID
							join AppDoDetails in _db.GetAppraisementDoDetails
                                on AppContainerDetails.CustomAppraisementId equals AppDoDetails.CustomAppraisementId
                                into AppDoDetailsGroup
                            from AppDoDetails in AppDoDetailsGroup.DefaultIfEmpty()
                            where
                          (string.IsNullOrEmpty(containerNo) || gateentry.ContainerNo == containerNo) &&
                          (string.IsNullOrEmpty(oblHblNo) || obldetails.OBL_HBL_No == oblHblNo) &&
						  (string.IsNullOrEmpty(AppNo) || AppraisementApplicationHeader.AppraisementNo == AppNo)
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
                                Cargo_Type = obldetails.Cargo_Type,
                                No_of_PKG = obldetails.No_of_PKG,
                                GR_WT_Kg = obldetails.GR_WT_Kg
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

        public async Task<Response<List<ResponseCustomerHeaderForList>>> GetCustomAppraisementApplicationHeader(int? id, int? page, int? size,bool? isInvoiceCheck)
        {
            var response = new Response<List<ResponseCustomerHeaderForList>>();

            try
            {
                var query = _db.CustomAppraisementApplicationHeaderList.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.ID == id.Value);
                }

				if (isInvoiceCheck == true)
				{
					var usedAppraisementNos = _db.GetYardInvoiceList    
												  .Select(x => x.ApplicationId)
												  .Distinct();

					query = query.Where(x => !usedAppraisementNos.Contains(x.ID));
				}

				var totalRecords = await query.CountAsync();

                if (page.HasValue && page > 0 && size.HasValue && size > 0)
                {
                    var skip = (page.Value - 1) * size.Value;
                    query = query.Skip(skip).Take(size.Value);
                }

				// var result = await query.ToListAsync();

				var result = await query
			.Select(h => new ResponseCustomerHeaderForList
			{
				ID = h.ID,
                AppraisementNo = h.AppraisementNo,
				AppraisementDate = h.AppraisementDate,
				ShippingLineId = h.ShippingLineId,
				CHAId = h.CHAId,
				Vessel = h.Vessel,
				Voyage = h.Voyage,
				Rotation = h.Rotation,
				DeliveryType = h.DeliveryType,
				DOStatus = h.DOStatus,
				AppraisementStatus = h.AppraisementStatus,
				CreatedDate = h.CreatedDate,
				CreatedBy = h.CreatedBy,
				ModifiedDate = h.ModifiedDate,
				ModifiedBy = h.ModifiedBy,
				ExaminationPercentage = h.ExaminationPercentage,
				ContainerCBTNo = _db.GetAppraisementContainerDetails
									 .Where(c => c.CustomAppraisementId == h.ID)
									 .Select(c => c.ContainerCBTNo)
									 .FirstOrDefault()
			})
			.ToListAsync();

				response.Data = result;
                response.Status = true;
                response.TotalCount = totalRecords;
            }
            catch (Exception ex)
            {
                response.Data = new List<ResponseCustomerHeaderForList>();
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

        public async Task<AddEditResponse> AddEditRequestRentOfficeSpaceCharges(RequestRentOfficeSpaceCharges request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                EXEC dbo.Sp_AddEditRentOfficeSpaceCharges 
                    @RentOfficeSpaceID = {request.RentOfficeSpaceID},
                    @EffectiveDate = {request.EffectiveDate},
                    @SacCodeId = {request.SacCodeId},
                    @Rate = {request.Rate},
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
                throw new ApplicationException("Failed to execute Sp_AddEditRentOfficeSpaceCharges", ex);
            }
        }

        public async Task<Response<List<RentOfficeSpaceCharges>>> GetRentOfficeSpaceCharges(int? id, int? page, int? size)
        {
            var response = new Response<List<RentOfficeSpaceCharges>>();

            try
            {
                var query = _db.GetRentOfficeSpaceCharges.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.RentOfficeSpaceID == id.Value);
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
                response.Data = new List<RentOfficeSpaceCharges>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditRentTableSpaceCharges(RequestRentTableSpaceCharges request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
            EXEC dbo.Sp_AddEditRentTableSpaceCharge 
                @RentTableSpaceID = {request.RentTableSpaceID},
                @EffectiveDate = {request.EffectiveDate},
                @SacCodeId = {request.SacCodeId},
                @Rate = {request.Rate},
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
                throw new ApplicationException("Failed to execute Sp_AddEditRentTableSpaceCharges", ex);
            }

        }
        public async Task<Response<List<RentTableSpaceCharges>>> GetRentTableSpaceCharges(int? id, int? page, int? size)
        {
            var response = new Response<List<RentTableSpaceCharges>>();

            try
            {
                var query = _db.GetRentTableSpaceCharges.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.RentTableSpaceID == id.Value);
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
                response.Data = new List<RentTableSpaceCharges>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }
        public async Task<Response<List<ResponseGatePassGateOut>>> GetGatePassGateOut(int? GatePassDtlId)
        {
            var response = new Response<List<ResponseGatePassGateOut>>();

            try
            {
                var query = from GPassHeader in _db.GatePassHeader

                            join GPassDetailsTemp in _db.GatePassDetails
                                on GPassHeader.GatePassId equals GPassDetailsTemp.GatepassId into GPassDetailsJoin
                            from GPassDetails in GPassDetailsJoin.DefaultIfEmpty()

                            join YardInvTemp in _db.GetYardInvoiceList
                                on GPassHeader.InvoiceId equals YardInvTemp.YardInvId into YardInvJoin
                            from YardInv in YardInvJoin.DefaultIfEmpty()

                            join AppContDetailsTemp in _db.GetAppraisementContainerDetails
                                on GPassDetails.ContainerNo equals AppContDetailsTemp.ContainerCBTNo into AppContDetailsJoin
                            from AppContDetails in AppContDetailsJoin.DefaultIfEmpty()

                            where (!GatePassDtlId.HasValue || GPassDetails.GatepassDtlId == GatePassDtlId)

                            select new ResponseGatePassGateOut
                            {
                                GatePassNo = GPassHeader.GatePassNo,
                                VehicleNo = GPassDetails != null ? GPassDetails.VehicleNo : null,
                                Importer = GPassHeader.ImpExpName,
                                ShipplingLine = GPassHeader.ShippingLineName,
                                GatePassDateTime = GPassHeader.GatePssDate,
                                ContainerNo = GPassDetails != null ? GPassDetails.ContainerNo : null,
                                ContainerSize = GPassDetails != null ? GPassDetails.Size : null,
                                CHAName = GPassHeader.ChaName,
                                InvoiceNo = YardInv != null ? YardInv.InvoiceNo : null,
                                GatePassValidity = GPassHeader.ExpDate,
                                BoeNo = AppContDetails != null ? AppContDetails.BOENo : null
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

        public async Task<AddEditResponse> CreateExitThroughGate(RequestExitThroughGate request)
        {
            var response = new AddEditResponse();
            try
            {
                var result = await _db.Set<ResponseCustomForExitThroughGate>()
                .FromSqlInterpolated($@"
                  EXEC dbo.Sp_AddEditExitThroughGateHeader
                  @ExitIdHeaderId = {request.ExitThroughGateHeader.ExitIdHeaderId},
                  @GateExitNo = {request.ExitThroughGateHeader.GateExitNo},
                  @GateExitDateTime = {request.ExitThroughGateHeader.GateExitDateTime},
                  @GatePassId = {request.ExitThroughGateHeader.GatePassId},
                  @GatePassNo = {request.ExitThroughGateHeader.GatePassNo},
                  @GatePassDate = {request.ExitThroughGateHeader.GatePassDate},
                  @ExpectedTime = {request.ExitThroughGateHeader.ExpectedTime},
                  @CBTNo = {request.ExitThroughGateHeader.CBTNo},
                  @Size = {request.ExitThroughGateHeader.Size},
                  @ShippingLine = {request.ExitThroughGateHeader.ShippingLine},
                  @CHAName = {request.ExitThroughGateHeader.CHAName},
                  @CargoDescription = {request.ExitThroughGateHeader.CargoDescription},
                  @CreatedBy = {request.ExitThroughGateHeader.CreatedBy},
                  @CreatedOn = {request.ExitThroughGateHeader.CreatedOn},
                  @UpdatedBy = {request.ExitThroughGateHeader.UpdatedBy},
                  @UpdatedOn = {request.ExitThroughGateHeader.UpdatedOn},
                  @BranchId = {request.ExitThroughGateHeader.BranchId},
                  @MsgFlag = {request.ExitThroughGateHeader.MsgFlag},
                  @Actual_File_Name = {request.ExitThroughGateHeader.Actual_File_Name},
                  @RuleCode = {request.ExitThroughGateHeader.RuleCode},
                  @DTMsgStatus = {request.ExitThroughGateHeader.DTMsgStatus},
                  @DTAmendStatus = {request.ExitThroughGateHeader.DTAmendStatus}
                    ")
                  .AsNoTracking()
                  .ToListAsync();


                var spResult = result.FirstOrDefault();
                if (spResult == null || spResult.Id == 0)
                {
                    response.Response = "CashReceiptHdrresult failed or returned no ID.";
                    return response;
                }
                if (request.ExitThroughGateDetails?.Any() == true)
                {

                    var xmlData = XmlConvertercs.ConvertToXmlExitThroughGateDetails(request.ExitThroughGateDetails);


                    await _db.Database.ExecuteSqlInterpolatedAsync($@"
                      EXEC sp_AddEditExitThroughGateDetails_XML 
                        @ExitIdHeader= {spResult.Id},
                       @XmlData = {xmlData}
                      ");
                }

                response.Response = "OK";
            }
            catch (Exception ex)
            {
                response.Response = $"Error: {ex.Message}";
            }
            return response;
        }

        public async Task<Response<List<GatePass>>> GetPassHeader(int? id, int? page, int? size)
        {
            var response = new Response<List<GatePass>>();

            try
            {
                var query = _db.GatePassHeader.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.GatePassId == id.Value);
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
                response.Data = new List<GatePass>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<GatePassDtl>>> GetPassDetails(int? id, int? gatepassId, int? page, int? size)
        {
            var response = new Response<List<GatePassDtl>>();

            try
            {
                var query = _db.GatePassDetails.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.GatepassDtlId == id.Value);
                }

                if (gatepassId.HasValue)
                {
                    query = query.Where(s => s.GatepassId == gatepassId.Value);
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
                response.Data = new List<GatePassDtl>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<ExitThroughGateHeader>>> GetExitThroughHeader(int? id, int? page, int? size)
        {
            var response = new Response<List<ExitThroughGateHeader>>();

            try
            {
                var query = _db.EThroughGateHeader.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.ExitIdHeaderId == id.Value);
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
                response.Data = new List<ExitThroughGateHeader>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }
        public async Task<Response<List<ExitThroughGateDetails>>> GetExitThroughDetails(int? id, int? page, int? size, int? GateExitHeaderId)
        {
            var response = new Response<List<ExitThroughGateDetails>>();

            try
            {
                var query = _db.EThroughGateDetails.AsQueryable();
                if (GateExitHeaderId.HasValue)
                {
                    query = query.Where(s => s.ExitIdHeader == GateExitHeaderId.Value);
                }

                if (id.HasValue)
                {
                    query = query.Where(s => s.ExitIdDtls == id.Value);
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
                response.Data = new List<ExitThroughGateDetails>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditCCINEntry(RequestCCINAddEdit request)
        {
            try
            {
                var result = await _db.AddEditResponse
                    .FromSqlInterpolated($@"
                EXEC dbo.Sp_AddEditCCINEntry
                    @CCINId = {request.CCINId},
                    @CCINNo = {request.CCINNo},
                    @CCINDate = {request.CCINDate},
                    @SBNo = {request.SBNo},
                    @SBDate = {request.SBDate},
                    @SBType = {request.SBType},
                    @ExporterId = {request.ExporterId},
                    @ShippingLineId = {request.ShippingLineId},
                    @CHAId = {request.CHAId},
                    @ConsigneeName = {request.ConsigneeName},
                    @ConsigneeAdd = {request.ConsigneeAdd},
                    @CountryId = {request.CountryId},
                    @StateId = {request.StateId},
                    @CityId = {request.CityId},
                    @PortOfLoadingId = {request.PortOfLoadingId},
                    @PortOfDischarge = {request.PortOfDischarge},
                    @Package = {request.Package},
                    @Weight = {request.Weight},
                    @FOB = {request.FOB},
                    @CommodityId = {request.CommodityId},
                    @CreatedBy = {request.CreatedBy},
                    @UpdatedBy = {request.UpdatedBy},
                    @InvoiceId = {request.InvoiceId},
                    @Remarks = {request.Remarks},
                    @IsApproved = {request.IsApproved},
                    @ApprovedBy = {request.ApprovedBy},
                    @ApprovedDate = {request.ApprovedDate},
                    @CargoType = {request.CargoType},
                    @GodownId = {request.GodownId},
                    @GodownName = {request.GodownName},
                    @PortofDestId = {request.PortofDestId},
                    @OTHr = {request.OTHr},
                    @IsCancelled = {request.IsCancelled},
                    @EximappID = {request.EximappID},
                    @PackageType = {request.PackageType},
                    @PackUQCCode = {request.PackUQCCode},
                    @PackUQCDesc = {request.PackUQCDesc},
                    @SEZ = {request.SEZ}
            ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute Sp_AddEditCCINEntry", ex);
            }
        }

        public async Task<Response<List<CCINEntry>>> GetCCINEntry(int? id, int? page, int? size)
        {
            var response = new Response<List<CCINEntry>>();

            try
            {
                var query = _db.CCINEntryDetails.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.CCINId == id.Value);
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
                response.Data = new List<CCINEntry>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }


        public async Task<AddEditResponse> AddEditDestuffingEntry(RequestDestuffingEntry request)
        {
            var response = new AddEditResponse();
            try
            {
                var result = await _db.Set<ResponseCustomFor>()
                                 .FromSqlInterpolated($@"
                                  EXEC dbo.Sp_AddEditImpDestuffingEntryHdr
                                   @DestuffingEntryId = {request.DestuffingEntryHdr.DestuffingEntryId},
                                   @DestuffingEntryNo = {request.DestuffingEntryHdr.DestuffingEntryNo},
                                   @StartDate = {request.DestuffingEntryHdr.StartDate},
                                   @DestuffingEntryDate = {request.DestuffingEntryHdr.DestuffingEntryDate},
                                   @TallySheetId = {request.DestuffingEntryHdr.TallySheetId},
                                   @ContainerId = {request.DestuffingEntryHdr.ContainerId},
                                   @ContainerNo = {request.DestuffingEntryHdr.ContainerNo},
                                   @Size = {request.DestuffingEntryHdr.Size},
                                   @CFSCode = {request.DestuffingEntryHdr.CFSCode},
                                   @ShippingLineId = {request.DestuffingEntryHdr.ShippingLineId},
                                   @CHAId = {request.DestuffingEntryHdr.CHAId},
                                   @Rotation = {request.DestuffingEntryHdr.Rotation},
                                   @DeliveryType = {request.DestuffingEntryHdr.DeliveryType},
                                   @DOType = {request.DestuffingEntryHdr.DOType},
                                   @GodownId = {request.DestuffingEntryHdr.GodownId},
                                   @BranchId = {request.DestuffingEntryHdr.BranchId},
                                   @CreatedBy = {request.DestuffingEntryHdr.CreatedBy},
                                   @CreatedOn = {request.DestuffingEntryHdr.CreatedOn},
                                   @UpdatedBy = {request.DestuffingEntryHdr.UpdatedBy},
                                   @UpdatedOn = {request.DestuffingEntryHdr.UpdatedOn},
                                   @CargoDelivery = {request.DestuffingEntryHdr.CargoDelivery}
                                     ")
                                  .AsNoTracking()
                                .ToListAsync();



                var spResult = result.FirstOrDefault();
                if (spResult == null || spResult.Id == 0)
                {
                    response.Response = "CashReceiptHdrresult failed or returned no ID.";
                    return response;
                }
                if (request.DestuffingEntryDtl?.Any() == true)
                {

                    var xmlData = XmlConvertercs.ConvertToXmlImpDestuffingEntryDtls(request.DestuffingEntryDtl);


                    await _db.Database.ExecuteSqlInterpolatedAsync($@"
                      EXEC Sp_AddEditImpDestuffingEntryDtl_XML
                        @DestuffingEntryId= {spResult.Id},
                       @XmlData = {xmlData}
                      ");
                }


                response.Response = "OK";
            }
            catch (Exception ex)
            {
                response.Response = $"Error: {ex.Message}";
            }
            return response;
        }

        public async Task<Response<List<ImpDestuffingEntryHdr>>> GetDestuffingEntryHdr(int? id, int? page, int? size)
        {
            var response = new Response<List<ImpDestuffingEntryHdr>>();

            try
            {
                var query = _db.ResponseImpDestuffingEntryHdr.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.DestuffingEntryId == id.Value);
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
                response.Data = new List<ImpDestuffingEntryHdr>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<ImpDestuffingEntryDtl>>> GetDestuffingEntryDtl(int? id, int? DestuffingEntryId, int? page, int? size)
        {
            var response = new Response<List<ImpDestuffingEntryDtl>>();

            try
            {
                var query = _db.ResponseImpDestuffingEntryDtl.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.DestuffingEntryDtlId == id.Value);
                }
                if (DestuffingEntryId.HasValue)
                {
                    query = query.Where(s => s.DestuffingEntryId == DestuffingEntryId.Value);
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
                response.Data = new List<ImpDestuffingEntryDtl>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<ResponseImportTransportChargesCalc> GetImportTransportChargesCalc(string ContainerOBLList, int PartyId)
        {
            try
            {
                var resultList = await _db.ResponseImportTransportChargesCalc
                    .FromSqlInterpolated($@"
            EXEC dbo.ImportTransportChargesCalc 
                @ContainerOBLList = {ContainerOBLList}, 
                @PartyId = {PartyId}
            ")
                    .AsNoTracking()
                    .ToListAsync();

                var result = resultList.FirstOrDefault();

                return result ?? new ResponseImportTransportChargesCalc();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute ImportTransportChargesCalc procedure", ex);
            }


        }

        public async Task<Response<List<ResponseGetinContainer>>> GetGetInContainerList()
        {
            var response = new Response<List<ResponseGetinContainer>>();

            try
            {
                var query = _db.GetEntryList.AsQueryable();

                var totalCount = await query.CountAsync();

                var data = await query
                    .Select(x => new ResponseGetinContainer
                    {
                        ContainerNo = x.ContainerNo
                    }).Distinct()
                    .ToListAsync();

                response.Data = data;
                response.Status = true;
                response.TotalCount = totalCount;
            }
            catch (Exception ex)
            {
                response.Data = new List<ResponseGetinContainer>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;

        }

        public async Task<AddEditResponse> CreateLoadContainerRequest(RequestLoadContainerRequest request)
        {
            var response = new AddEditResponse();

            try
            {
                var result = await _db.ResponseLoadContainerRequest
                    .FromSqlInterpolated($@"
                EXEC Sp_AddEditLoadContainerRequestHeader
                    @LoadContReqId = {request.LoadContainerHeader.LoadContReqId},
                    @LoadContReqNo = {request.LoadContainerHeader.LoadContReqNo},
                    @LoadContReqDate = {request.LoadContainerHeader.LoadContReqDate},
                    @CHAId = {request.LoadContainerHeader.CHAId},
                    @FinalDestinationLocationID = {request.LoadContainerHeader.FinalDestinationLocationID},
                    @FinalDestinationLocation = {request.LoadContainerHeader.FinalDestinationLocation},
                    @Remarks = {request.LoadContainerHeader.Remarks},
                    @Movement = {request.LoadContainerHeader.Movement},
                    @ExamType = {request.LoadContainerHeader.ExamType},
                    @BranchId = {request.LoadContainerHeader.BranchId},
                    @CreatedBy = {request.LoadContainerHeader.CreatedBy},
                    @CreatedOn = {request.LoadContainerHeader.CreatedOn},
                    @UpdatedBy = {request.LoadContainerHeader.UpdatedBy},
                    @UpdatedOn = {request.LoadContainerHeader.UpdatedOn},
                    @IsApproved = {request.LoadContainerHeader.IsApproved},
                    @SFMsgStatus = {request.LoadContainerHeader.SFMsgStatus},
                    @Origin = {request.LoadContainerHeader.Origin},
                    @Via = {request.LoadContainerHeader.Via},
                    @TransactionType = {request.LoadContainerHeader.TransactionType},
                    @SFSend = {request.LoadContainerHeader.SFSend}
            ")
                    .AsNoTracking()
                    .ToListAsync();

                var spResult = result?.FirstOrDefault();
                if (spResult == null || spResult.Id == 0)
                {
                    response.Response = $"Stored procedure failed. Message: {spResult?.Response ?? "No response"}";
                    return response;
                }

                // Save details only if header was inserted successfully
                if (request.LoadContainerRequestDetails?.Any() == true)
                {
                    var xmlData = XmlConvertercs.ConvertToXmlLoadContainerRequestDetails(request.LoadContainerRequestDetails);

                    await _db.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC Sp_AddEditLoadContainerRequestDetails_XML 
                    @LoadContReqId = {spResult.Id},
                    @XmlData = {xmlData}
            ");
                }

                response.Response = "OK";
            }
            catch (Exception ex)
            {
                response.Response = $"Error: {ex.Message}";
            }

            return response;
        }


        public async Task<Response<List<LoadContainerRequestHeader>>> GetLoadContainerHeader(int? id, int? page, int? size)
        {
            var response = new Response<List<LoadContainerRequestHeader>>();

            try
            {
                var query = _db.LoadContainerRtHeader.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.LoadContReqId == id.Value);
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
                response.Data = new List<LoadContainerRequestHeader>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }
        public async Task<Response<List<LoadContainerRequestDetails>>> GetLoadContainerDetails(int? id, int? page, int? size, int? LoaderHeaderId)
        {
            var response = new Response<List<LoadContainerRequestDetails>>();

            try
            {
                var query = _db.LoadContainerRDetails.AsQueryable();
                if (LoaderHeaderId.HasValue)
                {
                    query = query.Where(s => s.LoadContReqId == LoaderHeaderId.Value);
                }

                if (id.HasValue)
                {
                    query = query.Where(s => s.LoadContReqDetlId == id.Value);
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
                response.Data = new List<LoadContainerRequestDetails>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }





        public async Task<AddEditResponse> AddEditDeliveryApplication(RequestImpDeliveryApplication request)
        {
            var response = new AddEditResponse();

            try
            {
                // Step 1: Call SP to insert/update header
                var headerResult = await _db.Set<ResponseCustomFor>()
                    .FromSqlInterpolated($@"
                EXEC dbo.Sp_AddEditImpDeliveryApplicationHdr
                    @DeliveryId = {request.ImpDeliveryApplicationHdr.DeliveryId},
                    @DeliveryNo = {request.ImpDeliveryApplicationHdr.DeliveryNo},
                    @DestuffingId = {request.ImpDeliveryApplicationHdr.DestuffingId},
                    @CHAId = {request.ImpDeliveryApplicationHdr.CHAId},
                    @ImporterId = {request.ImpDeliveryApplicationHdr.ImporterId},
                    @CreatedBy = {request.ImpDeliveryApplicationHdr.CreatedBy},
                    @CreatedOn = {request.ImpDeliveryApplicationHdr.CreatedOn},
                    @UpdatedBy = {request.ImpDeliveryApplicationHdr.UpdatedBy},
                    @UpdatedOn = {request.ImpDeliveryApplicationHdr.UpdatedOn}
            ")
                    .AsNoTracking()
                    .ToListAsync();

                var spResult = headerResult.FirstOrDefault();

                if (spResult == null || spResult.Id == 0)
                {
                    response.Response = "Header creation failed or returned no ID.";
                    return response;
                }

                if (request.ImpDeliveryApplicationDtl?.Any() == true)
                {
                    var xmlData = XmlConvertercs.ConvertToXmlImpDeliveryApplicationDtls(request.ImpDeliveryApplicationDtl);

                    await _db.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC dbo.Sp_AddEditImpDeliveryApplicationDtl_XML
                    @DeliveryHdrId = {spResult.Id},
                    @XmlData = {xmlData}
            ");
                }

                response.Response = "OK";
            }
            catch (Exception ex)
            {
                response.Response = $"Error: {ex.Message}";
            }

            return response;

        }

        public async Task<Response<List<ImpDeliveryApplicationHdr>>> GetImpDeliveryApplicationHdr(int? id, int? page, int? size)
        {
            var response = new Response<List<ImpDeliveryApplicationHdr>>();

            try
            {
                var query = _db.RequestImpDeliveryApplicationHdr.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.DeliveryId == id.Value);
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
                response.Data = new List<ImpDeliveryApplicationHdr>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<ImpDeliveryApplicationDtl>>> GetImpDeliveryApplicationDtl(int? id, int? DeliveryId, int? page, int? size)
        {
            var response = new Response<List<ImpDeliveryApplicationDtl>>();

            try
            {
                var query = _db.RequestImpDeliveryApplicationDtl.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.DeliveryDtlId == id.Value);
                }

                if (DeliveryId.HasValue)
                {
                    query = query.Where(s => s.DeliveryId == DeliveryId.Value);
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
                response.Data = new List<ImpDeliveryApplicationDtl>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }


        public async Task<AddEditResponse> AddEditContainerStuffing(RequestContainerStuffing request)
        {
            var response = new AddEditResponse();

            try
            {
                var headerResult = await _db.Set<ResponseContainerStuffing>()
                    .FromSqlInterpolated($@"
                EXEC dbo.Sp_AddEditContainerStuffingHeader
                    @StuffingReqId = {request.ContainerStuffingHeader.StuffingReqId},
                    @ByTrain = {request.ContainerStuffingHeader.ByTrain},
                    @ByRoad = {request.ContainerStuffingHeader.ByRoad},
                    @StuffingReqNo = {request.ContainerStuffingHeader.StuffingReqNo},
                    @StuffingReqNoId = {request.ContainerStuffingHeader.StuffingReqNoId},
                    @RequestDate = {request.ContainerStuffingHeader.RequestDate},
                    @StuffingNo = {request.ContainerStuffingHeader.StuffingNo},
                    @StuffingDate = {request.ContainerStuffingHeader.StuffingDate},
                    @ContainerNo = {request.ContainerStuffingHeader.ContainerNo},
                    @ICDCode = {request.ContainerStuffingHeader.ICDCode},
                    @ContainerSize = {request.ContainerStuffingHeader.ContainerSize},
                    @FCL = {request.ContainerStuffingHeader.FCL},
                    @LCL = {request.ContainerStuffingHeader.LCL},
                    @POD = {request.ContainerStuffingHeader.POD},
                    @PODId = {request.ContainerStuffingHeader.PODId},
                    @Origin = {request.ContainerStuffingHeader.Origin},
                    @OriginId = {request.ContainerStuffingHeader.OriginId},
                    @ContPOL = {request.ContainerStuffingHeader.ContPOL},
                    @ContPOLId = {request.ContainerStuffingHeader.ContPOLId},
                    @Via = {request.ContainerStuffingHeader.Via},
                    @ViaId = {request.ContainerStuffingHeader.ViaId},
                    @ShippingLine = {request.ContainerStuffingHeader.ShippingLine},
                    @ShippingSeal = {request.ContainerStuffingHeader.ShippingSeal},
                    @CustomSeal = {request.ContainerStuffingHeader.CustomSeal},
                    @FinalDestinationLocation = {request.ContainerStuffingHeader.FinalDestinationLocation},
                    @FinalDestinationLocationId = {request.ContainerStuffingHeader.FinalDestinationLocationId},
                    @EquipmentSealType = {request.ContainerStuffingHeader.EquipmentSealType},
                    @EquipmentSealTypeId = {request.ContainerStuffingHeader.EquipmentSealTypeId},
                    @EquipmentStatus = {request.ContainerStuffingHeader.EquipmentStatus},
                    @EquipmentStatusId = {request.ContainerStuffingHeader.EquipmentStatusId},
                    @EquipmentQUC = {request.ContainerStuffingHeader.EquipmentQUC},
                    @EquipmentQUCId = {request.ContainerStuffingHeader.EquipmentQUCId},
                    @Remarks = {request.ContainerStuffingHeader.Remarks},
                    @SEZ = {request.ContainerStuffingHeader.SEZ},
                    @DirectStuffing = {request.ContainerStuffingHeader.DirectStuffing}
            ")
                    .AsNoTracking()
                    .ToListAsync();

                var spResult = headerResult.FirstOrDefault();

                if (spResult == null || spResult.Id == 0)
                {
                    response.Response = "Header creation failed or returned no ID.";
                    return response;
                }

                // Step 2: Insert/Update ContainerStuffingDetails using XML
                if (request.ContainerStuffingDetails?.Any() == true)
                {
                    var xmlData = XmlConvertercs.ConvertToXmlContainerStuffingDetails(request.ContainerStuffingDetails);

                    await _db.Database.ExecuteSqlInterpolatedAsync($@"
                EXEC dbo.Sp_AddEditContainerStuffingDetails_XML
                    @StuffingHdrId = {spResult.Id},
                    @XmlData = {xmlData}
            ");
                }

                response.Response = "OK";
            }
            catch (Exception ex)
            {
                response.Response = $"Error: {ex.Message}";
            }

            return response;
        }


        public async Task<Response<List<ContainerStuffingHeader>>> GetContainerStuffingHdr(int? id, int? page, int? size)
        {
            var response = new Response<List<ContainerStuffingHeader>>();

            try
            {
                var query = _db.ContainerStuffingHeader.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.StuffingReqId == id.Value);
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
                response.Data = new List<ContainerStuffingHeader>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<Response<List<ContainerStuffingDetails>>> GetContainerStuffingDtl(int? id, int? StuffingId, int? page, int? size)
        {
            var response = new Response<List<ContainerStuffingDetails>>();

            try
            {
                var query = _db.ContainerStuffingDetails.AsQueryable();

                if (id.HasValue)
                {
                    query = query.Where(s => s.StuffingDtlId == id.Value);
                }

                if (StuffingId.HasValue)
                {
                    query = query.Where(s => s.StuffingReqId == StuffingId.Value);
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
                response.Data = new List<ContainerStuffingDetails>();
                response.Status = false;
                response.TotalCount = 0;
                response.Message = $"Error: {ex.Message}";
            }

            return response;
        }

        public async Task<AddEditResponse> AddEditGodownInvoice(RequestGodownInvoice request)
        {
            // insert to yard invoice 
            try
            {
                var result = await _db.Set<ResponseCustomFor>()
                .FromSqlInterpolated($@"
             EXEC dbo.AddEditGodownInvoice 
            @GodownInvId = {request.GodownInvId},
            @IsTaxInvoice = {request.IsTaxInvoice},
            @IsBillOfSupply = {request.IsBillOfSupply},
            @InvoiceNo = {request.InvoiceNo},
            @DeliveryDate = {request.DeliveryDate},
            @ApplicationNo = {request.ApplicationNo},
            @InvoiceDate = {request.InvoiceDate},
            @PartyName = {request.PartyName},
            @PartyId = {request.PartyId},
            @PayeeName = {request.PayeeName},
            @PayeeId = {request.PayeeId},
            @GSTNo = {request.GSTNo},
            @OTHours = {request.OTHours},
            @PaymentMode = {request.PaymentMode},
            @Remarks = {request.Remarks},
            @CreatedBy = {request.CreatedBy},
            @UpdatedBy = {request.UpdatedBy}
                     ")
               .AsNoTracking()
              .ToListAsync();


                var response = result.FirstOrDefault();
                AddEditResponse resultres = null;
                if (response == null || response.Id == 0)
                {
                    resultres.Response = "Main SP failed or returned no ID.";
                    return resultres;
                }

                // insert yard charges  
                if (response != null && response.Id != 0 && request.jsonData != null)
                {
                    var result1 = await _db.Set<AddEditResponse>()
                        .FromSqlInterpolated($@"
					               EXEC dbo.SP_AddGodownInvoiceChargesJson
					                   @GodownInvId = {response.Id},
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
                throw new ApplicationException("Failed to execute GodownInvoice", ex);
            }
        }

        public async Task<Response<List<ResponseStorageChargesCalc>>> GetImportStorageChargesCalc(string containerOBLList, int partyId, DateTime InvoiceDate)
        {
            var response = new Response<List<ResponseStorageChargesCalc>>();

            try
            {
                var results = await _db
                    .Set<ResponseStorageChargesCalc>()
                    .FromSqlInterpolated($"EXEC [dbo].[ImportStorageChargesCalc] {containerOBLList}, {partyId}, {InvoiceDate}")
                    .AsNoTracking()
                    .ToListAsync();

                response.Data = results;
                response.Status = true;
                response.TotalCount = results.Count;
            }
            catch (Exception ex)
            {
                response.Data = new List<ResponseStorageChargesCalc>();
                response.Status = false;
                response.Message = $"Error: {ex.Message}";
                response.TotalCount = 0;
            }

            return response;
        }

        public async Task<Response<List<ResponseImportInsuaranceCharges>>> GetImportInsuranceChargesCalc(string containerOBLList, int partyId, DateTime InvoiceDate)
        {
            var response = new Response<List<ResponseImportInsuaranceCharges>>();

            try
            {
                var results = await _db
                    .Set<ResponseImportInsuaranceCharges>()
                    .FromSqlInterpolated($"EXEC [dbo].[ImportInsuranceChargesCalc] {containerOBLList}, {partyId},{InvoiceDate}")
                    .AsNoTracking()
                    .ToListAsync();

                response.Data = results;
                response.Status = true;
                response.TotalCount = results.Count;
            }
            catch (Exception ex)
            {
                response.Data = new List<ResponseImportInsuaranceCharges>();
                response.Status = false;
                response.Message = $"Error: {ex.Message}";
                response.TotalCount = 0;
            }

            return response;
        }

        public async Task<Response<ResponsehandlingCharges>> GetHandlingChargesCalc(string containerLoadConReqList, int partyId)
        {
            var response = new Response<ResponsehandlingCharges>();

            try
            {
                var results = await _db
                    .Set<ResponsehandlingCharges>()
                    .FromSqlInterpolated($"EXEC dbo.HandlingChargesCalc {containerLoadConReqList}, {partyId}")
                    .AsNoTracking()
                    .ToListAsync();

                var result = results.FirstOrDefault() ?? new ResponsehandlingCharges();

                response.Data = result;
                response.Status = true;
                response.TotalCount = 1;
            }
            catch (Exception ex)
            {
                response.Data = new ResponsehandlingCharges();
                response.Status = false;
                response.Message = $"Error: {ex.Message}";
                response.TotalCount = 0;
            }

            return response;
        }


		public async Task<Response<RegisterOfOutwardSupplyReportResponse>> GetRegisterOfOutwardSupplyReport(DateTime? FromDate, DateTime? ToDate, string InvoiceType)
		{
			var response = new Response<RegisterOfOutwardSupplyReportResponse>();

			try
			{
				var flatRows = await _db
					.Set<RegisterOfOutwardSupplyReportResponse>()
					.FromSqlInterpolated($"EXEC dbo.RegisterOfOutwardSupplyReport {FromDate},{ToDate},{InvoiceType ?? (object)DBNull.Value}")
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

				

				response.Data = first;
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

	
		public async Task<Response<List<ResponseChargeSummaryByInvoice>>> GetChargeSummaryByInvoiceResponse()
		{
			var response = new Response<List<ResponseChargeSummaryByInvoice>>();

			try
			{
				var results = await _db.ResponseChargeSummaryByInvoice
					.FromSqlInterpolated($"EXEC dbo.SP_GetChargeSummaryByInvoice")
					.AsNoTracking()
					.ToListAsync();

				response.Data = results;
				response.Status = true;
				response.TotalCount = results.Count;
			}
			catch (Exception ex)
			{
				response.Data = new List<ResponseChargeSummaryByInvoice>();
				response.Status = false;
				response.Message = $"Error: {ex.Message}";
				response.TotalCount = 0;
			}

			return response;
		}

        public async Task<Response<RegisterOfOutwardSupplyReportResponse>> GetRegisterOfOutwardSupplyReportInvoice(DateTime? FromDate, DateTime? ToDate, string InvoiceType)
        {
            var response = new Response<RegisterOfOutwardSupplyReportResponse>();

            try
            {
                var flatRows = await _db
                    .Set<RegisterOfOutwardSupplyReportResponse>()
                    .FromSqlInterpolated($"EXEC dbo.RegisterOfOutwardSupplyReport {FromDate},{ToDate},{InvoiceType ?? (object)DBNull.Value}")
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



                response.Data = first;
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


        public async Task<Response<RegisterOfOutwardSupplyReportResponse>> GetRegisterOfOutwardSupplyReportCancel(DateTime? FromDate, DateTime? ToDate, string InvoiceType)
        {
            var response = new Response<RegisterOfOutwardSupplyReportResponse>();

            try
            {
                var flatRows = await _db
                    .Set<RegisterOfOutwardSupplyReportResponse>()
                    .FromSqlInterpolated($"EXEC dbo.RegisterOfOutwardSupplyReport {FromDate},{ToDate},{InvoiceType ?? (object)DBNull.Value}")
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



                response.Data = first;
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
		public async Task<Response<List<ResponseGetContainerlistByGetEntry>>> GetContainerlistByGetEntry()
		{
			var response = new Response<List<ResponseGetContainerlistByGetEntry>>();

			try
			{
				var results = await _db.ResponseGetContainerlistByGetEntry
					.FromSqlInterpolated($"EXEC dbo.GetContainerlistByGetEntryOnly")
					.AsNoTracking()
					.ToListAsync();

				response.Data = results;
				response.Status = true;
				response.TotalCount = results.Count;
			}
			catch (Exception ex)
			{
				response.Data = new List<ResponseGetContainerlistByGetEntry>();
				response.Status = false;
				response.Message = $"Error: {ex.Message}";
				response.TotalCount = 0;
			}

			return response;
		}

		public async Task<Response<List<ResponseGetContainerlistForLoadedContainerRequest>>> GetContainerlistForLoadedContainerRequest()
        {
			var response = new Response<List<ResponseGetContainerlistForLoadedContainerRequest>>();

			try
			{
				var results = await _db.ResponseGetContainerlistForLoadedContainerRequest
					.FromSqlInterpolated($"EXEC dbo.GetContainerlistForLoadedContainerRequest")
					.AsNoTracking()
					.ToListAsync();

				response.Data = results;
				response.Status = true;
				response.TotalCount = results.Count;
			}
			catch (Exception ex)
			{
				response.Data = new List<ResponseGetContainerlistForLoadedContainerRequest>();
				response.Status = false;
				response.Message = $"Error: {ex.Message}";
				response.TotalCount = 0;
			}

			return response;
		}

		public async Task<Response<List<ResponseGetCLandRno>>> GetCLandRNoForLoadContainerInvoice(string? RequestNo)
        {
			var response = new Response<List<ResponseGetCLandRno>>();

			try
			{
				var query = from Lchdr in _db.LoadContainerRtHeader 
							join LcD in _db.LoadContainerRDetails
								on Lchdr.LoadContReqId equals LcD.LoadContReqId
                            where (string.IsNullOrEmpty(RequestNo) || Lchdr.LoadContReqNo == RequestNo)
							 && !_db.GetYardInvoiceList.Any(y => y.IsLoadContainerInvoice == true && y.ApplicationId == LcD.LoadContReqId)
							select new ResponseGetCLandRno
							{
								LoadContReqId = LcD.LoadContReqId,
								LoadContReqDetlId = LcD.LoadContReqDetlId,
								LoadContReqNo = Lchdr.LoadContReqNo,
								ContainerNo = LcD.ContainerNo,
							};

				var totalRecords = await query.CountAsync();
				var data = await query.ToListAsync();

				response.Data = data;
				response.Status = true;
				response.TotalCount = totalRecords;
			}
			catch (Exception ex)
			{
				response.Data = new List<ResponseGetCLandRno>();
				response.Status = false;
				response.TotalCount = 0;
				response.Message = $"Error: {ex.Message}";
			}

			return response;
		}
	}
}
