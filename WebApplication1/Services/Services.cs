
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using SezApi.Data;
using SezApi.Model.DBModels;
using SezApi.Model.Request;
using SezApi.Model.Response;
using System.Drawing;
using System.Linq;
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
   @UpdatedBy = {request.UpdatedBy}
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

        public async Task<Response<List<GateEntry>>> GetAllEntries(int? page, int? size)
        {
            var response = new Response<List<GateEntry>>();

            try
            {
                var query = _db.GetEntryList.AsQueryable();

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
                @Charge = {request.Charge},
                @EffectiveDate = {request.EffectiveDate},
                @BranchId = {request.BranchId},
                @SacCode = {request.SacCode},
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

        public async Task<Response<List<MstInsurance>>> GetMstInsurance()
        {
            var response = new Response<List<MstInsurance>>();

            try
            {
                var result = await _db.GetMstInsurance.ToListAsync();
                response.Data = result;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Data = new List<MstInsurance>();
                response.Status = false;
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

        public async Task<Response<List<MstParty>>> GetMstParty(int? page, int? size)
        {
            var response = new Response<List<MstParty>>();

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

        public async Task<AddEditResponse> AddEditYardInvoice(RequestYardInvocie  request)
        {
            try
            {
                var result = await _db.AddEditResponse
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
                 @PayeeName = {request.PayeeName}
                                 ")
                    .AsNoTracking()
                    .ToListAsync();

                var response = result.FirstOrDefault();
                return response ?? new AddEditResponse { Response = "No response from procedure." };
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to execute AddEditYardInvoice", ex);
            }

        }

        public async Task<Response<List<InvoiceYard>>> GetYardInvoice(int? page, int? size)
        {
            var response = new Response<List<InvoiceYard>>();

            try
            {
                var query = _db.GetYardInvoiceList.AsQueryable();

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

    }
}
